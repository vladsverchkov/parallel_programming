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

        }

        private void StartButton_Click(object sender, EventArgs e)
        {

        }

        private void clientManagerButton_Click(object sender, EventArgs e)
        {

        }

        private void ClientListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MessageTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SendButton_Click(object sender, EventArgs e)
        {

        }
    }
}
