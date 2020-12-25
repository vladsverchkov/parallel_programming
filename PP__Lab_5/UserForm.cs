using System;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using NetWork.NetworkLib;

namespace NetWork.User.Window

{
    public partial class UserForm : Form
    {
        static User user = new User();
        static string selectedUser;
        static string selectedFile;
        delegate void ConnectedDelegate(bool connected);
        delegate void ChatDelegate(string message, Color color);
        delegate void UserListDelegate(string userName, bool remove);
        delegate void FileListDelegate(string userName, string fileName);

        public UserForm()
        {
            InitializeComponent();
        }

        //функція під`єднання до серверу
        public void Connected(bool connected)
        {
            if (connectButton.InvokeRequired && ipTextBox.InvokeRequired && portTextBox.InvokeRequired && nameTextBox.InvokeRequired && sendButton.InvokeRequired)
            {
                var threadSafe = new ConnectedDelegate(LocalConnected);
                Invoke(threadSafe, new object[] { connected });
            }
            else
            {
                LocalConnected(connected);
            }
        }

        //виведення повідомлень до чату
        public void ChatWindow(string message, Color color)
        {
            if (chatRichTextBox.InvokeRequired)
            {
                var threadSafe = new ChatDelegate(LocalChatWindow);
                Invoke(threadSafe, new object[] { message, color });
            }
            else
            {
                LocalChatWindow(message, color);
            }
        }

        //виведення надійшовших файлів до відповідного боксу
        public void FileList(string user, string fileName)
        {
            if (fileListBox.InvokeRequired)
            {
                var threadSafe = new FileListDelegate(LocalFileList);
                Invoke(threadSafe, new object[] { user, fileName });
            }
            else
            {
                LocalFileList(user, fileName);
            }
        }

        void LocalFileList(string userName, string fileName)
        {
            fileListBox.Items.Add("User " + userName + " has sent file: " + fileName);
        }

        //список користувачів чату
        public void UserList(string userName, bool remove)
        {
            if (clientListBox.InvokeRequired)
            {
                var threadSafe = new UserListDelegate(LocalUserList); //делегат необхідний для передачі даних між потоками
                Invoke(threadSafe, new object[] { userName, remove });
            }
            else
            {
                LocalUserList(userName, remove);
            }
        }

        //що відбувається у UI конкретного юзера після запиту на з`єднання з сервером
        void LocalConnected(bool connected)
        {
            if (connected)
            {
                User.name = nameTextBox.Text;
                ipTextBox.Enabled = false;
                portTextBox.Enabled = false;
                nameTextBox.Enabled = false;
                connectButton.Text = "Disconnect";
                sendButton.Enabled = true;
            }
            else
            {
                User.name = string.Empty;
                sendButton.Enabled = false;
                ipTextBox.Enabled = true;
                portTextBox.Enabled = true;
                nameTextBox.Enabled = true;
                connectButton.Text = "Connect";
                clientListBox.Items.Clear();
                sendButton.Enabled = false;
            }
        }

        //що відбувається у чаті конкретного юзера після запиту на з`єднання з сервером
        void LocalChatWindow(string message, Color color)
        {
            chatRichTextBox.SelectionColor = color;

            if (chatRichTextBox.Text.Length > 0)
                chatRichTextBox.AppendText("\n" + message);
            else
                chatRichTextBox.AppendText(message);

            chatRichTextBox.SelectionColor = Color.Black;
        }

        //що відбувається у списку юзерів конкретного клієнта після запиту на з`єднання з сервером
        void LocalUserList(string userName, bool remove)
        {
            if (userName != null)
            {
                if (remove)
                {
                    clientListBox.Items.Remove(userName);

                    if (clientListBox.Items.Count <= 0)
                        sendButton.Enabled = false;

                }
                else
                {
                    clientListBox.Items.Add(userName);

                    if (!sendButton.Enabled)
                        sendButton.Enabled = true;
                }
            }
        }


        void ClientWindow_Load(object sender, EventArgs e)
        {
            sendButton.Enabled = false;
        }

        //опрацювання кнопки з`єднання із сервером
        void ConnectButton_Click(object sender, EventArgs e)
        {
            if (User.connected)
            {
                Connected(false);
                User.Disconnect();
            }
            else
            {
                bool ip = false;
                bool port = false;
                bool name = false;

                if (NetworkLib.NetworkFunctions.CheckIPv4Valid(ipTextBox.Text))
                {
                    User.ipAddress = ipTextBox.Text;
                    ip = true;
                }
                else
                {
                    ipTextBox.BackColor = Color.Red;
                    if (ip)
                        ip = false;
                }

                if (NetworkLib.NetworkFunctions.CheckPortValid(portTextBox.Text))
                {
                    User.port = portTextBox.Text;
                    port = true;
                }
                else
                {
                    portTextBox.BackColor = Color.Red;
                    if (port)
                        port = false;
                }

                if (nameTextBox.Text.Length > 0)
                {
                    User.name = nameTextBox.Text;
                    name = true;
                }
                else
                {
                    nameTextBox.BackColor = Color.Red;
                    if (name)
                        name = false;
                }

                if (ip && port && name)
                {
                    User.Connect();
                }
            }
        }


        void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (nameTextBox.BackColor == Color.Red)
                nameTextBox.BackColor = Color.White;
        }

        void IPTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ipTextBox.BackColor == Color.Red)
                ipTextBox.BackColor = Color.White;
        }

        void PortTextBox_TextChanged(object sender, EventArgs e)
        {
            if (portTextBox.BackColor == Color.Red)
                portTextBox.BackColor = Color.White;
        }

        //опрацювання кнопки, що відповідає за відправлення повідмолення
        void SendButton_Click(object sender, EventArgs e)
        {
            if (User.connected && messageTextBox.TextLength > 0)
            {
                if (selectedUser != string.Empty && selectedUser != null)
                {
                    string answerTime = DateTime.Now.ToLongTimeString();
                    var message = new string[3];
                    message[0] = selectedUser;
                    message[1] = messageTextBox.Text;
                    message[2] = answerTime;

                    LocalChatWindow("<" + answerTime + "> " + User.name + " -> " + selectedUser + ": " + messageTextBox.Text, User.colorSpecificChat);
                    User.DataOut(1, message);

                    messageTextBox.Clear();
                    clientListBox.ClearSelected();
                }
                else
                {
                    string answerTime = DateTime.Now.ToLongTimeString();
                    var message = new string[2];
                    message[0] = messageTextBox.Text;
                    message[1] = answerTime;

                    LocalChatWindow("<" + answerTime + "> " + User.name + ": " + messageTextBox.Text, User.colorChat);
                    User.DataOut(0, message);
                    messageTextBox.Clear();
                    clientListBox.ClearSelected();
                }
            }
        }

        void ClientListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedUser == clientListBox.GetItemText(clientListBox.SelectedItem))
            {
                clientListBox.ClearSelected();
                selectedUser = string.Empty;

                string temp = messageTextBox.Text;
                messageTextBox.Clear();
                messageTextBox.SelectionColor = User.colorChat;
                messageTextBox.AppendText(temp);
            }
            else
            {
                selectedUser = clientListBox.GetItemText(clientListBox.SelectedItem);

                string temp = messageTextBox.Text;
                messageTextBox.Clear();
                messageTextBox.SelectionColor = User.colorSpecificChat;
                messageTextBox.AppendText(temp);
            }
        }

        void MessageTextBox_TextChanged(object sender, EventArgs e)
        {
            if (selectedUser == string.Empty || selectedUser == null)
                messageTextBox.SelectionColor = User.colorChat;
            else if (selectedUser != string.Empty || selectedUser != null)
                messageTextBox.SelectionColor = User.colorSpecificChat;
        }

        void ClearChatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chatRichTextBox.Clear();
        }

        private void bt_send_Click(object sender, EventArgs e)
        {
            if (User.connected == true)
            {
                if (dlg_open_file.ShowDialog() == DialogResult.OK)
                {


                    IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(User.ipAddress), 89);
                    Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);


                    string selected_file = dlg_open_file.FileName; //повний шлях до обраного файлу
                    string fileName = Path.GetFileName(selected_file); //шлях до самого файлу
                    string user_name = User.name;

                    byte[] fileNameByte = Encoding.ASCII.GetBytes(user_name + "_--_" + fileName);

                    byte[] fileData = File.ReadAllBytes(selected_file);
                    byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
                    byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

                    fileNameLen.CopyTo(clientData, 0);
                    fileNameByte.CopyTo(clientData, 4);
                    fileData.CopyTo(clientData, 4 + fileNameByte.Length);

                    clientSock.Connect(ipEnd);
                    clientSock.Send(clientData);

                    LocalChatWindow("File " + fileName + " has been succsessfuly sent!", User.colorChat);

                    clientSock.Close();
                }
            }
            else
            {
                DialogResult dr2 = MessageBox.Show("User is not connected to the Server!");
            }
        }

        private void fileListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonGetFile_Click(object sender, EventArgs e)
        {
            try
            {

                selectedFile = fileListBox.GetItemText(fileListBox.SelectedItem);

                if (selectedFile != "")
                {

                    Thread thread = new Thread(() =>
                    {

                        string[] separator = new string[] { ": " };
                        string[] fileName_words = selectedFile.Split(separator, StringSplitOptions.None);

                        //fileListBox.Items.Add(fileName_words[1]); отображает имя желаемого файла, все хорошо

                        IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(User.ipAddress), 111);
                        Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

                        string user_name = User.name;

                        byte[] fileNameByte = Encoding.ASCII.GetBytes(user_name + "_--_" + fileName_words[1]);


                        byte[] clientData = new byte[4 + fileNameByte.Length];
                        byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

                        fileNameLen.CopyTo(clientData, 0);
                        fileNameByte.CopyTo(clientData, 4);

                        clientSock.Connect(ipEnd);
                        clientSock.Send(clientData);

                        //LocalChatWindow("You have been asking for file " + fileName_words[1], User.colorChat);

                        clientSock.Disconnect(true);


                    }
                    );
                    thread.Start();

                    thread.Join();

                    string[] res = ConnectionTransferData();
                    LocalChatWindow("---File " + res[1] + " has been received from server!---", Color.CadetBlue);

                }

            }
            catch (Exception ex)
            {
                DialogResult dr = MessageBox.Show("Hey! Some error occured: " + ex);
            }


        }

        public static string[] ConnectionTransferData()
        {

            string[] messageToServer = new string[2];

            try
            {
                //Код для прийому даних від користувачів
                IPEndPoint ipEnd2 = new IPEndPoint(IPAddress.Parse(NetworkFunctions.GetIP4Address()), 1112);
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                sock.Bind(ipEnd2);
                sock.Listen(50);
                Socket clientSock = sock.Accept();

                byte[] clientData = new byte[1024 * 5000];
                string pathToSaveFile = @"I:\C_Sharp_Saves\PP__Lab_5\PP__Lab_5\UserFiles\";

                int receivedBytesLen = clientSock.Receive(clientData);
                int fileNameLen = BitConverter.ToInt32(clientData, 0);
                string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);
                string[] separator = new string[] { "_--_" };

                string[] fileName_words = fileName.Split(separator, StringSplitOptions.None);

                BinaryWriter bWrite = new BinaryWriter(File.Open(pathToSaveFile + fileName_words[1], FileMode.Append));
                //BinaryWriter bWrite = new BinaryWriter(File.Open(pathToSaveFile + fileName, FileMode.Append)); 
                bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);



                messageToServer[0] = fileName_words[0];
                messageToServer[1] = fileName_words[1];

                bWrite.Close();
                clientSock.Close();


            }
            catch (Exception ex)
            {
                DialogResult dr = MessageBox.Show("Hey! Some error occured: " + ex);
            }
            return messageToServer;
        }
    }
}

