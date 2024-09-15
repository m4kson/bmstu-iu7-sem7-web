namespace ProdMonitor.GUI.View
{
    partial class StartForm
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
            HelloLable = new Label();
            LoginButton = new Button();
            RegisterButton = new Button();
            SuspendLayout();
            // 
            // HelloLable
            // 
            HelloLable.AutoSize = true;
            HelloLable.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            HelloLable.Location = new Point(92, 57);
            HelloLable.Name = "HelloLable";
            HelloLable.Size = new Size(190, 37);
            HelloLable.TabIndex = 0;
            HelloLable.Text = "Здравствуйте!";
            // 
            // LoginButton
            // 
            LoginButton.FlatStyle = FlatStyle.Flat;
            LoginButton.Font = new Font("Segoe UI", 14.25F);
            LoginButton.Location = new Point(82, 147);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(211, 42);
            LoginButton.TabIndex = 1;
            LoginButton.Text = "Войти";
            LoginButton.UseVisualStyleBackColor = true;
            // 
            // RegisterButton
            // 
            RegisterButton.FlatStyle = FlatStyle.Flat;
            RegisterButton.Font = new Font("Segoe UI", 14.25F);
            RegisterButton.Location = new Point(82, 195);
            RegisterButton.Name = "RegisterButton";
            RegisterButton.Size = new Size(211, 42);
            RegisterButton.TabIndex = 2;
            RegisterButton.Text = "Зарегистрироваться";
            RegisterButton.UseVisualStyleBackColor = true;
            // 
            // StartForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(373, 319);
            Controls.Add(RegisterButton);
            Controls.Add(LoginButton);
            Controls.Add(HelloLable);
            Name = "StartForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "StartForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label HelloLable;
        private Button LoginButton;
        private Button RegisterButton;
    }
}