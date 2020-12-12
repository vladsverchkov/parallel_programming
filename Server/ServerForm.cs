using System;
using System.Drawing;
using System.Net.Sockets;
using System.Windows.Forms;

namespace NetWork.Server.Window
{
    public partial class ServerForm : Form
    {
        static Server server = new Server();
        static string selectedUser;
        delegate void StartedDelegate(bool started);
        delegate void ChatDelegate(string message, Color color);
        delegate void UserListDelegate(string userName, bool remove);

        public void Started(bool started)
        {
            if (startButton.InvokeRequired)
            {
                var threadSafe = new StartedDelegate(LocalStarted);
                Invoke(threadSafe, new bool[] { started });
            }
            else
            {
                LocalStarted(started);
            }
        }

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

        public void UserList(string userName, bool remove)
        {
            if (clientListBox.InvokeRequired && chatRichTextBox.InvokeRequired)
            {
                var threadSafe = new UserListDelegate(LocalUserList);
                Invoke(threadSafe, new object[] { userName, remove });
            }
            else
            {
                LocalUserList(userName, remove);
            }
        }

        void LocalStarted(bool started)
        {
            if (started)
            {
                portTextBox.Enabled = false;
                startButton.Text = "Stop";
            }
            else
            {
                sendButton.Enabled = false;
                portTextBox.Enabled = true;
                startButton.Text = "Start";
                clientListBox.Items.Clear();
            }
        }

        void LocalChatWindow(string message, Color color)
        {
            chatRichTextBox.SelectionColor = color;

            if (chatRichTextBox.Text.Length > 0)
                chatRichTextBox.AppendText("\n" + message);
            else
                chatRichTextBox.AppendText(message);

            chatRichTextBox.SelectionColor = Color.Black;
        }

        void LocalUserList(string userName, bool remove)
        {
            if (userName != null)
            {
                if (remove)
                {
                    clientListBox.Items.Remove(userName);

                    if (clientListBox.Items.Count < 0)
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

        public ServerForm()
        {
            InitializeComponent();
        }

        private void PortTextBox_TextChanged(object sender, EventArgs e)
        {
            if (portTextBox.BackColor == Color.Red)
                portTextBox.BackColor = Color.White;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (Server.started)
            {
                Started(false);
                Server.StopServer();
            }
            else
            {
                if (NetworkLib.NetworkFunctions.CheckPortValid(portTextBox.Text))
                {
                    if (Server.started)
                        Started(false);
                    else
                        Started(true);

                    Server.port = portTextBox.Text;
                    Server.StartServer();
                }
                else
                {
                    portTextBox.BackColor = Color.Red;
                }
            }
        }

        private void clientManagerButton_Click(object sender, EventArgs e)
        {
            var clientManagerWindow = new UserManagerForm();
            clientManagerWindow.Show();
        }

        private void ClientListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedUser == clientListBox.GetItemText(clientListBox.SelectedItem))
            {
                clientListBox.ClearSelected();
                selectedUser = string.Empty;

                string temp = messageTextBox.Text;
                messageTextBox.Clear();
                messageTextBox.SelectionColor = Server.colorChat;
                messageTextBox.AppendText(temp);
            }
            else
            {
                selectedUser = clientListBox.GetItemText(clientListBox.SelectedItem);

                messageTextBox.Clear();
                messageTextBox.SelectionColor = Server.colorSpecificChat;
                messageTextBox.AppendText(messageTextBox.Text);
            }
        }

        private void MessageTextBox_TextChanged(object sender, EventArgs e)
        {
            if (selectedUser == string.Empty || selectedUser == null)
                messageTextBox.SelectionColor = Server.colorChat;
            else if (selectedUser != string.Empty || selectedUser != null)
                messageTextBox.SelectionColor = Server.colorSpecificChat;
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            if (Server.started && messageTextBox.TextLength > 0 && Server.users.Count > 0)
            {

                string answerTime = DateTime.Now.ToLongTimeString();

                if (selectedUser != string.Empty && selectedUser != null)
                {
                    var message = new string[3];
                    message[0] = "Server";
                    message[1] = messageTextBox.Text;
                    message[2] = answerTime;

                    Socket clientSocket = Server.GetClientSocket(selectedUser);

                    if (clientSocket != null)
                    {
                        LocalChatWindow("**" + answerTime + "** " + "Server" + " -> " + selectedUser + ": " + messageTextBox.Text, Color.Purple);
                        Server.DataOut(clientSocket, 1, message);
                        messageTextBox.Clear();
                        clientListBox.ClearSelected();
                    }
                }
                else
                {
                    var message = new string[3];
                    message[0] = "Server";
                    message[1] = messageTextBox.Text;
                    message[2] = answerTime;

                    LocalChatWindow("**" + answerTime + "** " + "Server: " + messageTextBox.Text, Color.DarkBlue);
                    Server.DataOut(0, message);
                    messageTextBox.Clear();
                    clientListBox.ClearSelected();
                }
            }
        }

        private void ServerWindow_Load(object sender, EventArgs e)
        {
            sendButton.Enabled = false;
        }

        private void clearChatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chatRichTextBox.Clear();
        }

        private void AddToManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string clientName = clientListBox.GetItemText(clientListBox.SelectedItem);
            if (clientName != null && clientName != string.Empty && selectedUser != null && selectedUser != string.Empty)
            {
                Server.AddToManagedClients(clientName);
                UserManagerForm.UpdateDataTable();
            }
        }

        private void KickClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Server.UserData user = Server.GetClient((string)clientListBox.SelectedItem);

            string clientName = clientListBox.GetItemText(clientListBox.SelectedItem);
            if (clientName != null && clientName != string.Empty && selectedUser != null && selectedUser != string.Empty)
            {
                Server.DisconnectClient(clientName, "Disconnected by server");
            }
        }

        private void BanClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string clientName = clientListBox.GetItemText(clientListBox.SelectedItem);
            if (clientName != null && clientName != string.Empty && selectedUser != null && selectedUser != string.Empty)
            {
                Server.AddToManagedClients(clientName, true);
                UserManagerForm.UpdateDataTable();
                Server.DisconnectClient(clientName, "Disconnected by server");
            }
        }
    }
}
