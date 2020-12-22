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
using System.Text;
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

        //для передачі даних
        public static Thread fileTransferThread;
        static Socket fileTransferSocket;

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

            //var thread = new List<Thread>();
            //thread.Add(new Thread(() => ConnectionTransferData(clientSock)));
            //foreach (Thread t in thread)
            //    t.Start();



            //var thread = new Thread(() => );
            //thread.Start();


            //foreach (Thread t in thread)
            //    t.Start();


        }

        public static string[] ConnectionTransferData()
        {
            //Код для прийому даних від користувачів
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(NetworkFunctions.GetIP4Address()), 89);
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            sock.Bind(ipEnd);
            sock.Listen(50);
            Socket clientSock = sock.Accept();

            byte[] clientData = new byte[1024 * 5000];
            string pathToSaveFile = @"I:\C_Sharp_Saves\PP__Lab_5\Server\UserFiles\";

            int receivedBytesLen = clientSock.Receive(clientData);
            int fileNameLen = BitConverter.ToInt32(clientData, 0);
            string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);
            string[] separator = new string[] { "_--_" };

            string[] fileName_words = fileName.Split(separator, StringSplitOptions.None);

            BinaryWriter bWrite = new BinaryWriter(File.Open(pathToSaveFile + fileName_words[1], FileMode.Append));
            //BinaryWriter bWrite = new BinaryWriter(File.Open(pathToSaveFile + fileName, FileMode.Append)); 
            bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);

            string[] messageToServer = new string[2];

            messageToServer[0] = fileName_words[0];
            messageToServer[1] = fileName_words[1];

            bWrite.Close();
            clientSock.Close();

            return messageToServer;
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

                //string result = ConnectionTransferData();
                Thread thread = new Thread(() =>
                {
                    string[] res = ConnectionTransferData();
                    serverForm.ChatWindow("File " + res[1] + " has been received from user " + res[0], Color.CadetBlue);
                }
                );
                thread.Start();
                //thread.Join();

                //var thread = new List<Thread>();
                
                //thread.Add(new Thread(() => ConnectionTransferData()));
                //foreach (Thread t in thread)
                //    t.Start();

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
            if (content.Item1 >= 0 && content.Item1 <= 3)
            {
                switch (content.Item1)
                {
                    case 0: //чат
                        if (user.registerd && content.Item2.Length == 2)
                        {
                            serverForm.ChatWindow("**" + content.Item2[1] + "** " + user.name + ": " + content.Item2[0], colorChat);

                            var newContent = new string[3];
                            newContent[0] = user.name;
                            newContent[1] = content.Item2[0];
                            newContent[2] = content.Item2[1];

                            DataOutExcludeSender(user.clientSocket, 0, newContent); 
                        }
                        break;

                    case 1: //конкретний чат
                        if (user.registerd && content.Item2.Length == 3)
                        {
                            if (user.name != content.Item2[0])
                            {
                                serverForm.ChatWindow("*-*" + content.Item2[2] + "*-* " + user.name + " -> " + content.Item2[0] + ": " + content.Item2[1], colorSpecificChat);

                                var specificChat = new string[3];
                                specificChat[0] = user.name;
                                specificChat[1] = content.Item2[1];
                                specificChat[2] = content.Item2[2];

                                Socket toClient = GetClientSocket(content.Item2[0]);

                                if (toClient != null)
                                    DataOut(toClient, 1, specificChat);
                            }
                            else
                            {
                                DisconnectClient(user, "incorrect packet sent");
                            }
                        }
                        break;

                    case 2: //реєстрація
                        if (IsBand(((IPEndPoint)user.clientSocket.RemoteEndPoint).Address))
                        {
                            DisconnectClient(user, "you are banned");
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
            else if (content.Item1 == 8)
            {
                serverForm.ChatWindow("DataTransfer:" + content.Item2[0] + " has sent " + content.Item2[1], colorSpecificChat);             
            }
            else
            {
                DisconnectClient(user, "disconnected for not being registered");
            }
        }

        //перевірка, чи забанено юзера
        static bool IsBand(IPAddress ipAddress)
        {
            CheckXML();

            var ips = managedClientsXML.Descendants().Elements("Client").AsEnumerable().ToArray();
            for (int i = 0; i < ips.Length; i++)
            {
                if (ips[i].Attribute("Address").Value == ipAddress.ToString() && (Convert.ToBoolean(ips[i].Attribute("Banned").Value)))
                {
                    return true;
                }
            }
            return false;
        }

        //перевірка взятого нікнейму
        static bool CheckIfNamesTaken(string name)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].name == name)
                    return true;
            }
            return false;
        }

        public static Socket GetClientSocket(string name)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].name == name)
                    return users[i].clientSocket;
            }
            return null;
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

        static int GetClientIndex(UserData Client) 
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i] == Client)
                    return i;
            }
            return -1;
        }

        //дісконект юзера
        public static void DisconnectClient(string userName)
        {
            UserData user = GetClient(userName);

            user.clientSocket.Close(); 
            users.Remove(user); 

            serverForm.UserList(user.name, true);
            serverForm.ChatWindow(user.name + " disconnected", colorDisconnect);

           
            if (users.Count > 1)
            {
                var clientDisconnecting = new string[1];
                clientDisconnecting[0] = user.name;

                DataOutExcludeSender(user.clientSocket, 3, clientDisconnecting);
            }
        }

        public static void DisconnectClient(string userName, string message)
        {
            UserData user = GetClient(userName);

            var clientDisconnectingMessage = new string[2];
            clientDisconnectingMessage[0] = "Server";
            clientDisconnectingMessage[1] = message;
            DataOut(user.clientSocket, 0, clientDisconnectingMessage);

            user.clientSocket.Close(); 
            users.Remove(user);

            serverForm.UserList(user.name, true);
            serverForm.ChatWindow(user.name + " disconnected: " + message, colorDisconnect);
        
            if (users.Count > 1)
            {
                var clientDisconnecting = new string[1];
                clientDisconnecting[0] = user.name;

                DataOutExcludeSender(user.clientSocket, 3, clientDisconnecting);
            }
        }

        public static void DisconnectClient(UserData user)
        {
            user.clientSocket.Close();
            users.Remove(user);

            serverForm.UserList(user.name, true);
            serverForm.ChatWindow(user.name + " disconnected", colorDisconnect);
           
            if (users.Count > 1)
            {
                var clientDisconnecting = new string[1];
                clientDisconnecting[0] = user.name;

                DataOutExcludeSender(user.clientSocket, 3, clientDisconnecting);
            }
        }

        public static void DisconnectClient(UserData user, string message)
        {
            var clientDisconnectingMessage = new string[2];
            clientDisconnectingMessage[0] = "Server";
            clientDisconnectingMessage[1] = message;
            DataOut(user.clientSocket, 0, clientDisconnectingMessage);

            user.clientSocket.Close(); 
            users.Remove(user);

            serverForm.UserList(user.name, true); 
            serverForm.ChatWindow(user.name + " disconnected: " + message, colorDisconnect);
        
            if (users.Count > 1)
            {
                var clientDisconnecting = new string[1];
                clientDisconnecting[0] = user.name;

                DataOutExcludeSender(user.clientSocket, 3, clientDisconnecting);
            }
        }

        public static void AddToManagedClients()
        {
            CheckXML();
            isDataTableUpdating = true;

            var clientInfo =
                new XElement("Client",
                    new XAttribute("Name", ""),
                    new XAttribute("Address", ""),
                    new XAttribute("Banned", false),
                    new XAttribute("Reason", "")
            );

            managedClientsXML.Element("Clients").Add(clientInfo);
            managedClientsXML.Save("ManagedClients.xml");
        }

        public static void AddToManagedClients(string name)
        {
            CheckXML();
            isDataTableUpdating = true;
            UserData user = GetClient(name);

            int a = 0;
            int c = managedClientsXML.Descendants("Client").Count();

            if (managedClientsXML.Descendants("Client").Any())
            {
                foreach (var fromManagedClientsXML in managedClientsXML.Descendants("Client"))
                {
                    a++;

                    if (user.clientSocket.RemoteEndPoint.ToString() == fromManagedClientsXML.Attribute("Address").Value)
                    {
                        break;
                    }

                    if (a == c)
                    {
                        var clientInfo =
                            new XElement("Client",
                                new XAttribute("Name", name),
                                new XAttribute("Address", ((IPEndPoint)user.clientSocket.RemoteEndPoint).Address),
                                new XAttribute("Banned", false),
                                new XAttribute("Reason", "")
                        );

                        managedClientsXML.Element("Clients").Add(clientInfo);
                        managedClientsXML.Save("ManagedClients.xml");

                        break;
                    }
                }
            }
            else
            {
                var clientInfo =
                    new XElement("Client",
                        new XAttribute("Name", name),
                        new XAttribute("Address", ((IPEndPoint)user.clientSocket.RemoteEndPoint).Address),
                        new XAttribute("Banned", false),
                        new XAttribute("Reason", "")
                );

                managedClientsXML.Element("Clients").Add(clientInfo);
                managedClientsXML.Save("ManagedClients.xml");
            }

        }

        public static void AddToManagedClients(string name, bool bandFromServer)
        {
            CheckXML();
            isDataTableUpdating = true;
            UserData user = GetClient(name);

            int a = 0;
            int c = managedClientsXML.Descendants("Client").Count();

            if (managedClientsXML.Descendants("Client").Any())
            {
                foreach (var fromManagedClientsXML in managedClientsXML.Descendants("Client"))
                {
                    a++;

                    if (user.clientSocket.RemoteEndPoint.ToString() == fromManagedClientsXML.Attribute("Address").Value)
                        break;

                    if (a == c)
                    {
                        var clientInfo =
                            new XElement("Client",
                                new XAttribute("Name", name),
                                new XAttribute("Address", ((IPEndPoint)user.clientSocket.RemoteEndPoint).Address),
                                new XAttribute("Banned", bandFromServer),
                                new XAttribute("Reason", "")
                            );

                        managedClientsXML.Element("Clients").Add(clientInfo);
                        managedClientsXML.Save("ManagedClients.xml");

                        break;
                    }
                }
            }
            else
            {
                var clientInfo =
                    new XElement("Client",
                    new XAttribute("Name", name),
                    new XAttribute("Address", ((IPEndPoint)user.clientSocket.RemoteEndPoint).Address),
                    new XAttribute("Banned", bandFromServer),
                    new XAttribute("Reason", "")
                );

                managedClientsXML.Element("Clients").Add(clientInfo);
                managedClientsXML.Save("ManagedClients.xml");
            }
        }

        public static void AddToManagedClients(string name, bool bandFromServer, string reason) //-------------МОЖЛИВО ВИДАЛИТИ ТРЕБА
        {
            CheckXML();
            isDataTableUpdating = true;
            UserData user = GetClient(name);

            int a = 0;
            int c = managedClientsXML.Descendants("Client").Count();

            if (managedClientsXML.Descendants("Client").Any())
            {
                foreach (var fromManagedClientsXML in managedClientsXML.Descendants("Client"))
                {
                    a++;

                    if (user.clientSocket.RemoteEndPoint.ToString() == fromManagedClientsXML.Attribute("Address").Value)
                        break;

                    if (a == c)
                    {
                        var clientInfo =
                            new XElement("Client",
                                new XAttribute("Name", name),
                                new XAttribute("Address", user.clientSocket.RemoteEndPoint),
                                new XAttribute("Banned", bandFromServer),
                                new XAttribute("Reason", reason)
                            );

                        managedClientsXML.Element("Clients").Add(clientInfo);
                        managedClientsXML.Save("ManagedClients.xml");

                        break;
                    }
                }
            }
            else
            {
                var clientInfo =
                    new XElement("Client",
                        new XAttribute("Name", name),
                        new XAttribute("Address", user.clientSocket.RemoteEndPoint),
                        new XAttribute("Banned", bandFromServer),
                        new XAttribute("Reason", reason)
                    );

                managedClientsXML.Element("Clients").Add(clientInfo);
                managedClientsXML.Save("ManagedClients.xml");
            }
        }

        public static void RemoveManagedClients(string endPoint)
        {
            CheckXML();
            isDataTableUpdating = true;
            if (File.Exists("ManagedClients.xml") && endPoint != null)
            {
                foreach (var user in managedClientsXML.Descendants("Client"))
                {
                    if (user.Attribute("Address").Value == endPoint)
                    {
                        user.Remove();
                        managedClientsXML.Save("ManagedClients.xml");
                        break;
                    }
                }
            }
        }

        public static void CheckXML()
        {
            if (!File.Exists("ManagedClients.xml"))
            {
                managedClientsXML = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XComment("All managed Clients are here"),
                    new XElement("Clients"));

                managedClientsXML.Save("ManagedClients.xml");

                managedClientsXML = XDocument.Load("ManagedClients.xml");
            }
            else
            if (managedClientsXML == null)
                managedClientsXML = XDocument.Load("ManagedClients.xml");
        }

    }
}
