namespace ProdMonitor.GUI.View
{
    partial class AuthForm
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
            LoginTextBox = new TextBox();
            PasswordTextBox = new TextBox();
            LoginButton = new Button();
            LoginPic = new PictureBox();
            PasswordPic = new PictureBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)LoginPic).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PasswordPic).BeginInit();
            SuspendLayout();
            // 
            // LoginTextBox
            // 
            LoginTextBox.Font = new Font("Segoe UI", 14.25F);
            LoginTextBox.Location = new Point(78, 92);
            LoginTextBox.Name = "LoginTextBox";
            LoginTextBox.Size = new Size(163, 33);
            LoginTextBox.TabIndex = 0;
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Font = new Font("Segoe UI", 14.25F);
            PasswordTextBox.Location = new Point(78, 134);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.Size = new Size(163, 33);
            PasswordTextBox.TabIndex = 1;
            PasswordTextBox.UseSystemPasswordChar = true;
            // 
            // LoginButton
            // 
            LoginButton.FlatStyle = FlatStyle.Flat;
            LoginButton.Location = new Point(78, 193);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(137, 40);
            LoginButton.TabIndex = 2;
            LoginButton.Text = "Войти";
            LoginButton.UseVisualStyleBackColor = true;
            // 
            // LoginPic
            // 
            LoginPic.Image = Properties.Resources._2674096_object_email_web_essential_icon;
            LoginPic.Location = new Point(41, 92);
            LoginPic.Name = "LoginPic";
            LoginPic.Size = new Size(31, 33);
            LoginPic.SizeMode = PictureBoxSizeMode.StretchImage;
            LoginPic.TabIndex = 3;
            LoginPic.TabStop = false;
            // 
            // PasswordPic
            // 
            PasswordPic.Image = Properties.Resources._8680028_lock_password_line_icon;
            PasswordPic.Location = new Point(41, 134);
            PasswordPic.Name = "PasswordPic";
            PasswordPic.Size = new Size(31, 33);
            PasswordPic.SizeMode = PictureBoxSizeMode.StretchImage;
            PasswordPic.TabIndex = 4;
            PasswordPic.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(62, 29);
            label1.Name = "label1";
            label1.Size = new Size(179, 37);
            label1.TabIndex = 5;
            label1.Text = "Авторизация";
            // 
            // AuthForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(288, 274);
            Controls.Add(label1);
            Controls.Add(PasswordPic);
            Controls.Add(LoginPic);
            Controls.Add(LoginButton);
            Controls.Add(PasswordTextBox);
            Controls.Add(LoginTextBox);
            Name = "AuthForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AuthForm";
            ((System.ComponentModel.ISupportInitialize)LoginPic).EndInit();
            ((System.ComponentModel.ISupportInitialize)PasswordPic).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox LoginTextBox;
        private TextBox PasswordTextBox;
        private Button LoginButton;
        private PictureBox LoginPic;
        private PictureBox PasswordPic;
        private Label label1;
    }
}