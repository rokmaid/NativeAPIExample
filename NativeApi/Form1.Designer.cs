namespace NativeApi
{
    partial class Form1
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
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.btnSendToHost = new System.Windows.Forms.Button();
            this.btnSendToEmulator = new System.Windows.Forms.Button();
            this.btnGetToken = new System.Windows.Forms.Button();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.btnShowInEmu = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(51, 131);
            this.txtResult.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedHorizontal;
            this.txtResult.Size = new System.Drawing.Size(534, 844);
            this.txtResult.TabIndex = 0;
            this.txtResult.Text = "";
            // 
            // btnSendToHost
            // 
            this.btnSendToHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendToHost.Location = new System.Drawing.Point(620, 58);
            this.btnSendToHost.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSendToHost.Name = "btnSendToHost";
            this.btnSendToHost.Size = new System.Drawing.Size(272, 90);
            this.btnSendToHost.TabIndex = 1;
            this.btnSendToHost.Text = "Send to Host";
            this.btnSendToHost.UseVisualStyleBackColor = true;
            this.btnSendToHost.Click += new System.EventHandler(this.btnSendToHost_Click);
            // 
            // btnSendToEmulator
            // 
            this.btnSendToEmulator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendToEmulator.Location = new System.Drawing.Point(620, 192);
            this.btnSendToEmulator.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSendToEmulator.Name = "btnSendToEmulator";
            this.btnSendToEmulator.Size = new System.Drawing.Size(272, 88);
            this.btnSendToEmulator.TabIndex = 2;
            this.btnSendToEmulator.Text = "Send to Emulator";
            this.btnSendToEmulator.UseVisualStyleBackColor = true;
            this.btnSendToEmulator.Click += new System.EventHandler(this.btnSendToEmulator_Click);
            // 
            // btnGetToken
            // 
            this.btnGetToken.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetToken.Location = new System.Drawing.Point(620, 460);
            this.btnGetToken.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnGetToken.Name = "btnGetToken";
            this.btnGetToken.Size = new System.Drawing.Size(272, 93);
            this.btnGetToken.TabIndex = 3;
            this.btnGetToken.Text = "Get PNR";
            this.btnGetToken.UseVisualStyleBackColor = true;
            this.btnGetToken.Click += new System.EventHandler(this.btnGetToken_Click);
            // 
            // txtCommand
            // 
            this.txtCommand.Location = new System.Drawing.Point(51, 58);
            this.txtCommand.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(534, 32);
            this.txtCommand.TabIndex = 4;
            // 
            // btnShowInEmu
            // 
            this.btnShowInEmu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowInEmu.Location = new System.Drawing.Point(620, 324);
            this.btnShowInEmu.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnShowInEmu.Name = "btnShowInEmu";
            this.btnShowInEmu.Size = new System.Drawing.Size(272, 88);
            this.btnShowInEmu.TabIndex = 5;
            this.btnShowInEmu.Text = "Show in Emulator";
            this.btnShowInEmu.UseVisualStyleBackColor = true;
            this.btnShowInEmu.Click += new System.EventHandler(this.btnShowInEmu_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(620, 612);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(272, 93);
            this.button1.TabIndex = 6;
            this.button1.Text = "Sales Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(986, 732);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnShowInEmu);
            this.Controls.Add(this.txtCommand);
            this.Controls.Add(this.btnGetToken);
            this.Controls.Add(this.btnSendToEmulator);
            this.Controls.Add(this.btnSendToHost);
            this.Controls.Add(this.txtResult);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "Form1";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Native API Example";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtResult;
        private System.Windows.Forms.Button btnSendToHost;
        private System.Windows.Forms.Button btnSendToEmulator;
        private System.Windows.Forms.Button btnGetToken;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.Button btnShowInEmu;
        private System.Windows.Forms.Button button1;
    }
}

