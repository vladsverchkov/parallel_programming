namespace PP__Lab_5
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserForm));
            this.ipLabel = new System.Windows.Forms.Label();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.chatRichTextBox = new System.Windows.Forms.RichTextBox();
            this.messageTextBox = new System.Windows.Forms.RichTextBox();
            this.clientListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sendButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
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
            this.ipTextBox.Size = new System.Drawing.Size(87, 20);
            this.ipTextBox.TabIndex = 0;
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
            this.portTextBox.Size = new System.Drawing.Size(36, 20);
            this.portTextBox.TabIndex = 1;
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
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(441, 12);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(124, 53);
            this.connectButton.TabIndex = 3;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            // 
            // chatRichTextBox
            // 
            this.chatRichTextBox.Location = new System.Drawing.Point(180, 71);
            this.chatRichTextBox.Name = "chatRichTextBox";
            this.chatRichTextBox.Size = new System.Drawing.Size(385, 307);
            this.chatRichTextBox.TabIndex = 2;
            this.chatRichTextBox.Text = "";
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(180, 412);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(385, 54);
            this.messageTextBox.TabIndex = 3;
            this.messageTextBox.Text = "";
            // 
            // clientListBox
            // 
            this.clientListBox.FormattingEnabled = true;
            this.clientListBox.Location = new System.Drawing.Point(18, 49);
            this.clientListBox.Name = "clientListBox";
            this.clientListBox.Size = new System.Drawing.Size(151, 329);
            this.clientListBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(185, 390);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Type your message here:";
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(21, 412);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(148, 54);
            this.sendButton.TabIndex = 6;
            this.sendButton.Text = "Send Message";
            this.sendButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(18, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Users:";
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 488);
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
    }
}

