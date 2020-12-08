using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using NetWork.Data;
using NetWork.User.Window;

namespace NetWork.User // створюємо свій простір імен (імпровізована мережа) задля простішого доступу до інших наших виконавчих файлів 
{
    [System.Serializable]
    public class User
    {
        static Socket masterSocket; // для взаємодії за протоколами ТСР/UDP
        static UserForm userWindow;
        public static List<string> users; 
        public static string port; 
        public static string ipAddress; 
        public static string name = string.Empty; 
        public static bool connected; //перевірка підклюеності юзерів

        public static Color colorSystem = Color.Green;
        public static Color colorAlert = Color.Red;
        public static Color colorConnect = Color.DarkGreen;
        public static Color colorDisconnect = Color.Orange;
        public static Color colorChat = Color.DarkBlue;
        public static Color colorSpecificChat = Color.Purple;

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(userWindow = new UserForm());
        }

        public static void Connect() //запит на під`єднання до серверу
        {
            users = new List<string>(); 
            connected = true;

            try 
            {
                masterSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ipAddress), int.Parse(port)); //creats an IPEndPoint and sets it with ipAddress and port
                masterSocket.Connect(ipe); //connects to the server
            }
            catch 
            {
                userWindow.ChatWindow("Could not connect to host!", colorAlert); //prints text to screen
                Disconnect();
            }

            Thread dataInThread = new Thread(DataIn); //створення потоку, в якому виконуватиметься DataIn функція
            dataInThread.Start(); 
        }

        public static void Disconnect() //процедура від`єднання від серверу
        {
            users.Clear();
            connected = false;
            userWindow.Connected(false);
            masterSocket.Close(); 
        }

    }
}
