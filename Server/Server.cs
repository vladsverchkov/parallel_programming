using NetWork.Data;
using NetWork.NetworkLib;
using NetWork.Server.Window;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace NetWork.Server
{
    [System.Serializable]
    public class Server 
    {
        static ServerForm serverForm; //UI серверу
        static Socket listenerSocket;
        static IPEndPoint serverIP;
        static Thread listenThread; //потік для прослуховування бажаючих підєднатись
        public static Thread removeUserThread;
        public static Thread mainThread;
        public static XmlDocument managedUsers;
        public static bool isDataTableUpdating;

        public static List<UserData> users; 
        public static string port; 
        public static bool started;

        public static Color colorSystem = Color.Green;
        public static Color colorAlert = Color.Red;
        public static Color colorConnect = Color.DarkGreen;
        public static Color colorDisconnect = Color.Orange;
        public static Color colorChat = Color.DarkBlue;
        public static Color colorSpecificChat = Color.Purple;
        public static XDocument managedClientsXML; 

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(serverForm = new ServerForm());
        }

        public static void StartServer() //функція запуску серверу
        {
            CheckXML();
            mainThread = Thread.CurrentThread;

            if (started)
                StopServer();

            try
            {
                listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverIP = new IPEndPoint(IPAddress.Parse(NetworkFunctions.GetIP4Address()), int.Parse(port));
                listenerSocket.Bind(serverIP);
            }
            catch (SocketException ex)
            {
                serverForm.ChatWindow("Can't start server " + ex, colorAlert); 
                StopServer();
            }

            users = new List<UserData>(); 
            serverForm.ChatWindow("Start server on: " + NetworkFunctions.GetIP4Address() + ":" + port, colorSystem); 
            serverForm.Started(true);
            started = true; 
            listenThread = new Thread(ListenThread); 
            listenThread.Start(); 
        }

        public static void StopServer() //зупинка серверу
        {
            started = false; 

           
            foreach (UserData user in users)
            {
                user.clientSocket.Close();
            }

            listenerSocket.Close(); 
            listenThread.Abort();
            serverIP = null; 
            users.Clear(); 
            serverForm.Started(false);
            serverForm.ChatWindow("Stopped server", colorAlert);
        }

        public class UserData // клас, що містить дані про юзера 
        {
            public Socket clientSocket; 
            public Thread clientThread; 
            public bool registerd; 
            public string name; 
            public string id; 

            public UserData(Socket clientSocket) 
            {
                this.clientSocket = clientSocket; 
                id = Guid.NewGuid().ToString();
            }

            public void SendRegistrationPacket(Socket clientSocket) //надсилаємо registration packet юзеру якому потрібен сокет для відправки
            {
                if (clientSocket != null)
                {
                    if (Server.users.Count > 1)
                    {
                        var content = new string[Server.users.Count - 1];

                        int a = 0;
                        for (int i = 0; i < Server.users.Count; i++)
                        {
                            if (Server.users[i].name != null)
                            {
                                content[a] = Server.users[i].name;
                                a++;
                            }
                        }
                        Server.DataOut(clientSocket, 2, content);
                    }
                    else
                    {
                        var content = new string[1];
                        Server.DataOut(clientSocket, 2, content);
                    }
                }
            }

        }

        static void ListenThread()
        {
            Thread.CurrentThread.IsBackground = true;

            while (started)
            {
                listenerSocket.Listen(0); 
                Socket clientSocket = listenerSocket.Accept();
                UserData user = new UserData(clientSocket);
                users.Add(user); 
                user.clientThread = new Thread(() => Server.DataIn(user));
                user.clientThread.IsBackground = true;
                user.clientThread.Start(); 
                user.SendRegistrationPacket(user.clientSocket); 
            }
        }

        static void DataIn(UserData user) 
        {
            while (started) 
            {
                try
                {
                    byte[] buffer; 
                    int readBytes;

                    buffer = new byte[user.clientSocket.SendBufferSize];  
                    readBytes = user.clientSocket.Receive(buffer);

                    if (readBytes == 0) 
                    {
                        serverForm.ChatWindow("Client socket closed, Disconnecting " + user.name, colorAlert);
                        break;
                    }
                    DataManager(user, Packet.UnPack(buffer)); 
                }
                catch
                {
                    break;
                }
            }

            DisconnectClient(user);
            Thread.CurrentThread.Abort();
        }

        public static void DataOut(int PacketType, string[] sendData)
        {
            var content = Tuple.Create(PacketType, sendData);
            byte[] packet = Packet.Pack(content);

            try
            {
                if (started && users.Count > 0)
                {
                    foreach (UserData user in users)
                        user.clientSocket.Send(packet);
                }
                else
                    serverForm.ChatWindow("No Clients to send to", colorAlert);
            }
            catch (SocketException sx)
            {
                serverForm.ChatWindow("SocketException: " + sx, colorAlert);
            }
        }

        public static void DataOut(Socket socket, int PacketType, string[] sendData)
        {
            try
            {
                if (started && users.Count > 0)
                {
                    var content = Tuple.Create(PacketType, sendData); 
                    socket.Send(Packet.Pack(content)); 
                }
                else
                    serverForm.ChatWindow("No Clients to send to", colorAlert);
            }
            catch (SocketException sx)
            {
                serverForm.ChatWindow("SocketException: " + sx, colorAlert);
            }
        }

        public static void DataOutExcludeSender(Socket socket, int PacketType, string[] sendData)
        {
            var content = Tuple.Create(PacketType, sendData);
            byte[] pakcet = Packet.Pack(content);

            try
            {
                if (started && users.Count > 0)
                {
                    foreach (UserData user in users)
                    {
                        if (user.clientSocket != socket)
                            user.clientSocket.Send(pakcet); 
                    }
                }
                else
                    serverForm.ChatWindow("No Clients to send to", colorAlert);
            }
            catch (SocketException sx)
            {
                serverForm.ChatWindow("SocketException: " + sx, colorAlert);
            }
        }

        static void DataManager(UserData user, Tuple<int, string[]> content) 
        {
            if (content.Item1 >= 0 && content.Item1 <= 2)
            {
                switch (content.Item1)
                {
                    case 0: //чат
                        if (user.registerd && content.Item2.Length == 1)
                        {
                            serverForm.ChatWindow(user.name + ": " + content.Item2[0], colorChat);

                            var newContent = new string[2];
                            newContent[0] = user.name;
                            newContent[1] = content.Item2[0];

                            DataOutExcludeSender(user.clientSocket, 0, newContent); //sends
                        }
                        break;

                    case 1: //конкретний чат
                        if (user.registerd && content.Item2.Length == 2)
                        {
                            if (user.name != content.Item2[0])
                            {
                                serverForm.ChatWindow(user.name + " -> " + content.Item2[0] + ": " + content.Item2[1], colorSpecificChat);

                                var specificChat = new string[2];
                                specificChat[0] = user.name;
                                specificChat[1] = content.Item2[1];

                                Socket toClient = GetClientSocket(content.Item2[0]);

                                if (toClient != null)
                                    DataOut(toClient, 1, specificChat); //sends
                            }
                            else
                            {
                                DisconnectClient(client, "incorrect packet sent");
                            }
                        }
                        break;

                    case 2: //реєстрація
                        if (IsBand(((IPEndPoint)user.clientSocket.RemoteEndPoint).Address))
                        {
                            DisconnectClient(user, "you are band");
                        }
                        else
                        if (CheckIfNamesTaken(content.Item2[0]))
                        {
                            DisconnectClient(user, "name already taken");
                        }
                        else
                        {
                            user.name = content.Item2[0];
                            user.registerd = true;
                            serverForm.UserList(user.name, false);
                            serverForm.ChatWindow(user.name + " connected", colorConnect);

                            var userName = new string[1];
                            userName[0] = user.name;
                            DataOutExcludeSender(user.clientSocket, 3, userName);
                        }
                        break;
                }
            }
            else
            {
                DisconnectClient(user, "disconnected for not being registerd");
            }
        }

        public static UserData GetClient(string name)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].name == name)
                    return users[i];
            }
            return null;
        }

    }
}
