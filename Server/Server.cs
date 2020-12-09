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

    }
}
