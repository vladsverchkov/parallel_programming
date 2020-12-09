namespace Server
{
    partial class ServerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerForm));
            this.clientManagerButton = new System.Windows.Forms.Button();
            this.portLabel = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.messageTextBox = new System.Windows.Forms.RichTextBox();
            this.chatRichTextBox = new System.Windows.Forms.RichTextBox();
            this.clientListBox = new System.Windows.Forms.ListBox();
            this.contextMenuStripChat = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripClient = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kickClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.banClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStripChat.SuspendLayout();
            this.contextMenuStripClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // clientManagerButton
            // 
            this.clientManagerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clientManagerButton.Location = new System.Drawing.Point(12, 21);
            this.clientManagerButton.Name = "clientManagerButton";
            this.clientManagerButton.Size = new System.Drawing.Size(171, 36);
            this.clientManagerButton.TabIndex = 1;
            this.clientManagerButton.Text = "Client Manager";
            this.clientManagerButton.UseVisualStyleBackColor = true;
            this.clientManagerButton.Click += new System.EventHandler(this.clientManagerButton_Click);
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(309, 14);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(28, 13);
            this.portLabel.TabIndex = 0;
            this.portLabel.Text = "Port";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(312, 30);
            this.portTextBox.MaximumSize = new System.Drawing.Size(36, 20);
            this.portTextBox.MaxLength = 5;
            this.portTextBox.MinimumSize = new System.Drawing.Size(36, 20);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(36, 20);
            this.portTextBox.TabIndex = 0;
            this.portTextBox.TextChanged += new System.EventHandler(this.PortTextBox_TextChanged);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(365, 21);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(103, 36);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start Server";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(210, 385);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(398, 76);
            this.messageTextBox.TabIndex = 5;
            this.messageTextBox.Text = "";
            this.messageTextBox.TextChanged += new System.EventHandler(this.MessageTextBox_TextChanged);
            // 
            // chatRichTextBox
            // 
            this.chatRichTextBox.Location = new System.Drawing.Point(210, 96);
            this.chatRichTextBox.Name = "chatRichTextBox";
            this.chatRichTextBox.Size = new System.Drawing.Size(398, 251);
            this.chatRichTextBox.TabIndex = 6;
            this.chatRichTextBox.Text = "";
            this.chatRichTextBox.TextChanged += new System.EventHandler(this.PortTextBox_TextChanged);
            // 
            // clientListBox
            // 
            this.clientListBox.FormattingEnabled = true;
            this.clientListBox.Location = new System.Drawing.Point(12, 96);
            this.clientListBox.Name = "clientListBox";
            this.clientListBox.Size = new System.Drawing.Size(171, 251);
            this.clientListBox.TabIndex = 7;
            this.clientListBox.SelectedIndexChanged += new System.EventHandler(this.ClientListBox_SelectedIndexChanged);
            // 
            // contextMenuStripChat
            // 
            this.contextMenuStripChat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearChatToolStripMenuItem});
            this.contextMenuStripChat.Name = "contextMenuStripChat";
            this.contextMenuStripChat.Size = new System.Drawing.Size(128, 26);
            // 
            // clearChatToolStripMenuItem
            // 
            this.clearChatToolStripMenuItem.Name = "clearChatToolStripMenuItem";
            this.clearChatToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.clearChatToolStripMenuItem.Text = "Clear Chat";
            // 
            // contextMenuStripClient
            // 
            this.contextMenuStripClient.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToManagerToolStripMenuItem,
            this.kickClientToolStripMenuItem,
            this.banClientToolStripMenuItem});
            this.contextMenuStripClient.Name = "contextMenuStripClient";
            this.contextMenuStripClient.Size = new System.Drawing.Size(159, 70);
            // 
            // addToManagerToolStripMenuItem
            // 
            this.addToManagerToolStripMenuItem.Name = "addToManagerToolStripMenuItem";
            this.addToManagerToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.addToManagerToolStripMenuItem.Text = "Add to Manager";
            // 
            // kickClientToolStripMenuItem
            // 
            this.kickClientToolStripMenuItem.Name = "kickClientToolStripMenuItem";
            this.kickClientToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.kickClientToolStripMenuItem.Text = "Kick Client";
            // 
            // banClientToolStripMenuItem
            // 
            this.banClientToolStripMenuItem.Name = "banClientToolStripMenuItem";
            this.banClientToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.banClientToolStripMenuItem.Text = "Ban Client";
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(12, 385);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(171, 76);
            this.sendButton.TabIndex = 10;
            this.sendButton.Text = "Send Message";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(207, 360);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Type your message here:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(12, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Users:";
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 476);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.clientManagerButton);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.clientListBox);
            this.Controls.Add(this.chatRichTextBox);
            this.Controls.Add(this.messageTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServerForm";
            this.Text = "Server";
            this.contextMenuStripChat.ResumeLayout(false);
            this.contextMenuStripClient.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button clientManagerButton;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.RichTextBox messageTextBox;
        private System.Windows.Forms.RichTextBox chatRichTextBox;
        private System.Windows.Forms.ListBox clientListBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripChat;
        private System.Windows.Forms.ToolStripMenuItem clearChatToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripClient;
        private System.Windows.Forms.ToolStripMenuItem addToManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kickClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem banClientToolStripMenuItem;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

