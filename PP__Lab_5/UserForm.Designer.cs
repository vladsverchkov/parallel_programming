﻿namespace NetWork.User.Window
{
    partial class UserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserForm));
            this.ipLabel = new System.Windows.Forms.Label();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.chatRichTextBox = new System.Windows.Forms.RichTextBox();
            this.contextMenuStripChat = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messageTextBox = new System.Windows.Forms.RichTextBox();
            this.clientListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sendButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dlg_open_file = new System.Windows.Forms.OpenFileDialog();
            this.bt_send = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.fileListBox = new System.Windows.Forms.ListBox();
            this.buttonGetFile = new System.Windows.Forms.Button();
            this.contextMenuStripChat.SuspendLayout();
            this.SuspendLayout();
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.Location = new System.Drawing.Point(172, 11);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(60, 13);
            this.ipLabel.TabIndex = 0;
            this.ipLabel.Text = "IP Address";
            // 
            // ipTextBox
            // 
            this.ipTextBox.AccessibleRole = System.Windows.Forms.AccessibleRole.IpAddress;
            this.ipTextBox.Location = new System.Drawing.Point(175, 27);
            this.ipTextBox.MaximumSize = new System.Drawing.Size(87, 20);
            this.ipTextBox.MaxLength = 15;
            this.ipTextBox.MinimumSize = new System.Drawing.Size(87, 20);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(87, 22);
            this.ipTextBox.TabIndex = 0;
            this.ipTextBox.TextChanged += new System.EventHandler(this.IPTextBox_TextChanged);
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(265, 11);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(28, 13);
            this.portLabel.TabIndex = 1;
            this.portLabel.Text = "Port";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(268, 27);
            this.portTextBox.MaximumSize = new System.Drawing.Size(36, 20);
            this.portTextBox.MaxLength = 5;
            this.portTextBox.MinimumSize = new System.Drawing.Size(36, 20);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(36, 22);
            this.portTextBox.TabIndex = 1;
            this.portTextBox.TextChanged += new System.EventHandler(this.PortTextBox_TextChanged);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(307, 11);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(36, 13);
            this.nameLabel.TabIndex = 2;
            this.nameLabel.Text = "Name";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(310, 27);
            this.nameTextBox.MaxLength = 20;
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(125, 22);
            this.nameTextBox.TabIndex = 2;
            this.nameTextBox.TextChanged += new System.EventHandler(this.NameTextBox_TextChanged);
            // 
            // connectButton
            // 
            this.connectButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectButton.Location = new System.Drawing.Point(441, 12);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(148, 53);
            this.connectButton.TabIndex = 3;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // chatRichTextBox
            // 
            this.chatRichTextBox.ContextMenuStrip = this.contextMenuStripChat;
            this.chatRichTextBox.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chatRichTextBox.Location = new System.Drawing.Point(216, 75);
            this.chatRichTextBox.Name = "chatRichTextBox";
            this.chatRichTextBox.Size = new System.Drawing.Size(373, 174);
            this.chatRichTextBox.TabIndex = 5;
            this.chatRichTextBox.Text = "";
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
            this.clearChatToolStripMenuItem.Click += new System.EventHandler(this.ClearChatToolStripMenuItem_Click);
            // 
            // messageTextBox
            // 
            this.messageTextBox.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.messageTextBox.Location = new System.Drawing.Point(216, 426);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(373, 96);
            this.messageTextBox.TabIndex = 3;
            this.messageTextBox.Text = "";
            this.messageTextBox.TextChanged += new System.EventHandler(this.MessageTextBox_TextChanged);
            // 
            // clientListBox
            // 
            this.clientListBox.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clientListBox.FormattingEnabled = true;
            this.clientListBox.ItemHeight = 17;
            this.clientListBox.Location = new System.Drawing.Point(18, 75);
            this.clientListBox.Name = "clientListBox";
            this.clientListBox.Size = new System.Drawing.Size(180, 174);
            this.clientListBox.TabIndex = 4;
            this.clientListBox.SelectedIndexChanged += new System.EventHandler(this.ClientListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(213, 401);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Type your message here:";
            // 
            // sendButton
            // 
            this.sendButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sendButton.Location = new System.Drawing.Point(18, 426);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(180, 40);
            this.sendButton.TabIndex = 6;
            this.sendButton.Text = "Send Message";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(18, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Users:";
            // 
            // dlg_open_file
            // 
            this.dlg_open_file.FileName = "openFileDialog1";
            // 
            // bt_send
            // 
            this.bt_send.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bt_send.Location = new System.Drawing.Point(18, 472);
            this.bt_send.Name = "bt_send";
            this.bt_send.Size = new System.Drawing.Size(180, 50);
            this.bt_send.TabIndex = 8;
            this.bt_send.Text = "Send Data";
            this.bt_send.UseVisualStyleBackColor = true;
            this.bt_send.Click += new System.EventHandler(this.bt_send_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Firebrick;
            this.label3.Location = new System.Drawing.Point(15, 252);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Files:";
            // 
            // fileListBox
            // 
            this.fileListBox.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fileListBox.FormattingEnabled = true;
            this.fileListBox.HorizontalScrollbar = true;
            this.fileListBox.ItemHeight = 17;
            this.fileListBox.Location = new System.Drawing.Point(18, 279);
            this.fileListBox.Name = "fileListBox";
            this.fileListBox.Size = new System.Drawing.Size(571, 89);
            this.fileListBox.TabIndex = 10;
            this.fileListBox.SelectedIndexChanged += new System.EventHandler(this.fileListBox_SelectedIndexChanged);
            // 
            // buttonGetFile
            // 
            this.buttonGetFile.BackColor = System.Drawing.Color.BurlyWood;
            this.buttonGetFile.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonGetFile.Location = new System.Drawing.Point(18, 374);
            this.buttonGetFile.Name = "buttonGetFile";
            this.buttonGetFile.Size = new System.Drawing.Size(180, 40);
            this.buttonGetFile.TabIndex = 12;
            this.buttonGetFile.Text = "Receive File";
            this.buttonGetFile.UseVisualStyleBackColor = false;
            this.buttonGetFile.Click += new System.EventHandler(this.buttonGetFile_Click);
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 534);
            this.Controls.Add(this.buttonGetFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.fileListBox);
            this.Controls.Add(this.bt_send);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ipLabel);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.ipTextBox);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.clientListBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.chatRichTextBox);
            this.Controls.Add(this.connectButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserForm";
            this.Text = "User Chat";
            this.contextMenuStripChat.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.RichTextBox chatRichTextBox;
        private System.Windows.Forms.RichTextBox messageTextBox;
        private System.Windows.Forms.ListBox clientListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripChat;
        private System.Windows.Forms.ToolStripMenuItem clearChatToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog dlg_open_file;
        private System.Windows.Forms.Button bt_send;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox fileListBox;
        private System.Windows.Forms.Button buttonGetFile;
    }
}

