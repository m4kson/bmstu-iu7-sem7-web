namespace ProdMonitor.GUI.View
{
    partial class RegistrationForm
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
            label1 = new Label();
            MailTxt = new TextBox();
            PasswordTxt = new TextBox();
            NameTxt = new TextBox();
            SurnameTxt = new TextBox();
            SexComboBox = new ComboBox();
            BirthDayPicker = new DateTimePicker();
            PatronymicTxt = new TextBox();
            DepartmentTxt = new TextBox();
            RegButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(78, 36);
            label1.Name = "label1";
            label1.Size = new Size(171, 37);
            label1.TabIndex = 0;
            label1.Text = "Регистрация";
            // 
            // MailTxt
            // 
            MailTxt.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            MailTxt.Location = new Point(78, 101);
            MailTxt.Name = "MailTxt";
            MailTxt.PlaceholderText = "e-mail";
            MailTxt.Size = new Size(171, 33);
            MailTxt.TabIndex = 1;
            // 
            // PasswordTxt
            // 
            PasswordTxt.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PasswordTxt.Location = new Point(78, 140);
            PasswordTxt.Name = "PasswordTxt";
            PasswordTxt.PlaceholderText = "Password";
            PasswordTxt.Size = new Size(171, 33);
            PasswordTxt.TabIndex = 2;
            PasswordTxt.UseSystemPasswordChar = true;
            // 
            // NameTxt
            // 
            NameTxt.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            NameTxt.Location = new Point(78, 179);
            NameTxt.Name = "NameTxt";
            NameTxt.PlaceholderText = "Name";
            NameTxt.Size = new Size(171, 33);
            NameTxt.TabIndex = 3;
            // 
            // SurnameTxt
            // 
            SurnameTxt.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            SurnameTxt.Location = new Point(78, 218);
            SurnameTxt.Name = "SurnameTxt";
            SurnameTxt.PlaceholderText = "Surname";
            SurnameTxt.Size = new Size(171, 33);
            SurnameTxt.TabIndex = 4;
            // 
            // SexComboBox
            // 
            SexComboBox.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            SexComboBox.ForeColor = SystemColors.WindowFrame;
            SexComboBox.FormattingEnabled = true;
            SexComboBox.Items.AddRange(new object[] { "Male", "Female" });
            SexComboBox.Location = new Point(78, 296);
            SexComboBox.Name = "SexComboBox";
            SexComboBox.Size = new Size(171, 33);
            SexComboBox.TabIndex = 6;
            SexComboBox.Text = "Sex";
            // 
            // BirthDayPicker
            // 
            BirthDayPicker.AccessibleName = "Birth Day";
            BirthDayPicker.CalendarFont = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            BirthDayPicker.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            BirthDayPicker.ImeMode = ImeMode.NoControl;
            BirthDayPicker.Location = new Point(78, 374);
            BirthDayPicker.Name = "BirthDayPicker";
            BirthDayPicker.Size = new Size(171, 33);
            BirthDayPicker.TabIndex = 7;
            // 
            // PatronymicTxt
            // 
            PatronymicTxt.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PatronymicTxt.Location = new Point(78, 257);
            PatronymicTxt.Name = "PatronymicTxt";
            PatronymicTxt.PlaceholderText = "Patronymic";
            PatronymicTxt.Size = new Size(171, 33);
            PatronymicTxt.TabIndex = 5;
            // 
            // DepartmentTxt
            // 
            DepartmentTxt.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            DepartmentTxt.Location = new Point(78, 335);
            DepartmentTxt.Name = "DepartmentTxt";
            DepartmentTxt.PlaceholderText = "Department";
            DepartmentTxt.Size = new Size(171, 33);
            DepartmentTxt.TabIndex = 8;
            // 
            // RegButton
            // 
            RegButton.FlatStyle = FlatStyle.Flat;
            RegButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            RegButton.Location = new Point(59, 428);
            RegButton.Name = "RegButton";
            RegButton.Size = new Size(209, 44);
            RegButton.TabIndex = 9;
            RegButton.Text = "Зарегистрироваться";
            RegButton.UseVisualStyleBackColor = true;
            // 
            // RegistrationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(328, 500);
            Controls.Add(RegButton);
            Controls.Add(DepartmentTxt);
            Controls.Add(BirthDayPicker);
            Controls.Add(SexComboBox);
            Controls.Add(PatronymicTxt);
            Controls.Add(SurnameTxt);
            Controls.Add(NameTxt);
            Controls.Add(PasswordTxt);
            Controls.Add(MailTxt);
            Controls.Add(label1);
            MaximizeBox = false;
            Name = "RegistrationForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "RegistrationForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox MailTxt;
        private TextBox PasswordTxt;
        private TextBox NameTxt;
        private TextBox SurnameTxt;
        private ComboBox SexComboBox;
        private DateTimePicker BirthDayPicker;
        private TextBox PatronymicTxt;
        private TextBox DepartmentTxt;
        private Button RegButton;
    }
}