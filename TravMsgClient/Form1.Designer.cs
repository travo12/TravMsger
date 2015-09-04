namespace TravMsgClient
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
            this.Text_Box_Message = new System.Windows.Forms.RichTextBox();
            this.Login_Button = new System.Windows.Forms.Button();
            this.Chat_Window = new System.Windows.Forms.ListBox();
            this.Send_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Text_Box_Message
            // 
            this.Text_Box_Message.Location = new System.Drawing.Point(17, 347);
            this.Text_Box_Message.Name = "Text_Box_Message";
            this.Text_Box_Message.Size = new System.Drawing.Size(401, 72);
            this.Text_Box_Message.TabIndex = 0;
            this.Text_Box_Message.Text = "";
            // 
            // Login_Button
            // 
            this.Login_Button.Location = new System.Drawing.Point(277, 431);
            this.Login_Button.Name = "Login_Button";
            this.Login_Button.Size = new System.Drawing.Size(140, 22);
            this.Login_Button.TabIndex = 1;
            this.Login_Button.Text = "Login";
            this.Login_Button.UseVisualStyleBackColor = true;
            this.Login_Button.Click += new System.EventHandler(this.Login_Button_Click);
            // 
            // Chat_Window
            // 
            this.Chat_Window.FormattingEnabled = true;
            this.Chat_Window.Location = new System.Drawing.Point(17, 10);
            this.Chat_Window.Name = "Chat_Window";
            this.Chat_Window.Size = new System.Drawing.Size(399, 329);
            this.Chat_Window.TabIndex = 2;
            // 
            // Send_Button
            // 
            this.Send_Button.Location = new System.Drawing.Point(22, 430);
            this.Send_Button.Name = "Send_Button";
            this.Send_Button.Size = new System.Drawing.Size(145, 22);
            this.Send_Button.TabIndex = 3;
            this.Send_Button.Text = "Send";
            this.Send_Button.UseVisualStyleBackColor = true;
            this.Send_Button.Click += new System.EventHandler(this.Send_Button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 458);
            this.Controls.Add(this.Send_Button);
            this.Controls.Add(this.Chat_Window);
            this.Controls.Add(this.Login_Button);
            this.Controls.Add(this.Text_Box_Message);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox Text_Box_Message;
        private System.Windows.Forms.Button Login_Button;
        private System.Windows.Forms.ListBox Chat_Window;
        private System.Windows.Forms.Button Send_Button;
    }
}

