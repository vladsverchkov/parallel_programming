namespace PP_Lab_3
{
    partial class FormMain
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
            this.refreshButton = new System.Windows.Forms.Button();
            this.numberField = new System.Windows.Forms.NumericUpDown();
            this.temperatureInput = new System.Windows.Forms.TrackBar();
            this.drawBox = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.numberField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.temperatureInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).BeginInit();
            this.SuspendLayout();
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(482, 15);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 0;
            this.refreshButton.Text = "Go";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // numberField
            // 
            this.numberField.Location = new System.Drawing.Point(353, 15);
            this.numberField.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numberField.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberField.Name = "numberField";
            this.numberField.Size = new System.Drawing.Size(114, 22);
            this.numberField.TabIndex = 1;
            this.numberField.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // temperatureInput
            // 
            this.temperatureInput.Location = new System.Drawing.Point(12, 12);
            this.temperatureInput.Name = "temperatureInput";
            this.temperatureInput.Size = new System.Drawing.Size(335, 45);
            this.temperatureInput.TabIndex = 2;
            this.temperatureInput.ValueChanged += new System.EventHandler(this.temperatureInput_ValueChanged);
            // 
            // drawBox
            // 
            this.drawBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.drawBox.Location = new System.Drawing.Point(12, 48);
            this.drawBox.Name = "drawBox";
            this.drawBox.Size = new System.Drawing.Size(547, 323);
            this.drawBox.TabIndex = 3;
            this.drawBox.TabStop = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 383);
            this.Controls.Add(this.drawBox);
            this.Controls.Add(this.temperatureInput);
            this.Controls.Add(this.numberField);
            this.Controls.Add(this.refreshButton);
            this.Name = "FormMain";
            this.Text = "Brownian motion";
            ((System.ComponentModel.ISupportInitialize)(this.numberField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.temperatureInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.NumericUpDown numberField;
        private System.Windows.Forms.TrackBar temperatureInput;
        private System.Windows.Forms.PictureBox drawBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

