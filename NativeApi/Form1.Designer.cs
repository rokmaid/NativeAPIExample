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
            this.SuspendLayout();
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(53, 71);
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedHorizontal;
            this.txtResult.Size = new System.Drawing.Size(278, 333);
            this.txtResult.TabIndex = 0;
            this.txtResult.Text = "";
            // 
            // btnSendToHost
            // 
            this.btnSendToHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendToHost.Location = new System.Drawing.Point(372, 31);
            this.btnSendToHost.Name = "btnSendToHost";
            this.btnSendToHost.Size = new System.Drawing.Size(164, 49);
            this.btnSendToHost.TabIndex = 1;
            this.btnSendToHost.Text = "Send to Host";
            this.btnSendToHost.UseVisualStyleBackColor = true;
            this.btnSendToHost.Click += new System.EventHandler(this.btnSendToHost_Click);
            // 
            // btnSendToEmulator
            // 
            this.btnSendToEmulator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendToEmulator.Location = new System.Drawing.Point(372, 104);
            this.btnSendToEmulator.Name = "btnSendToEmulator";
            this.btnSendToEmulator.Size = new System.Drawing.Size(164, 48);
            this.btnSendToEmulator.TabIndex = 2;
            this.btnSendToEmulator.Text = "Send to Emulator";
            this.btnSendToEmulator.UseVisualStyleBackColor = true;
            this.btnSendToEmulator.Click += new System.EventHandler(this.btnSendToEmulator_Click);
            // 
            // btnGetToken
            // 
            this.btnGetToken.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetToken.Location = new System.Drawing.Point(372, 176);
            this.btnGetToken.Name = "btnGetToken";
            this.btnGetToken.Size = new System.Drawing.Size(164, 51);
            this.btnGetToken.TabIndex = 3;
            this.btnGetToken.Text = "Get Token";
            this.btnGetToken.UseVisualStyleBackColor = true;
            this.btnGetToken.Click += new System.EventHandler(this.btnGetToken_Click);
            // 
            // txtCommand
            // 
            this.txtCommand.Location = new System.Drawing.Point(53, 31);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(278, 20);
            this.txtCommand.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 465);
            this.Controls.Add(this.txtCommand);
            this.Controls.Add(this.btnGetToken);
            this.Controls.Add(this.btnSendToEmulator);
            this.Controls.Add(this.btnSendToHost);
            this.Controls.Add(this.txtResult);
            this.Name = "Form1";
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
    }
}

