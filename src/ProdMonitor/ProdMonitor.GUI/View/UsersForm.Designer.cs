namespace ProdMonitor.GUI.View
{
    partial class UsersForm
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
            dataGridView1 = new DataGridView();
            DepartmentTxt = new TextBox();
            SkipTxt = new TextBox();
            LimitTxt = new TextBox();
            GetUsersBtn = new Button();
            label1 = new Label();
            SexComboBox = new ComboBox();
            RoleBox = new ComboBox();
            BackBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(28, 68);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1150, 443);
            dataGridView1.TabIndex = 0;
            // 
            // DepartmentTxt
            // 
            DepartmentTxt.Font = new Font("Segoe UI", 14.25F);
            DepartmentTxt.Location = new Point(432, 562);
            DepartmentTxt.Name = "DepartmentTxt";
            DepartmentTxt.PlaceholderText = "Департамент";
            DepartmentTxt.Size = new Size(171, 33);
            DepartmentTxt.TabIndex = 1;
            // 
            // SkipTxt
            // 
            SkipTxt.Font = new Font("Segoe UI", 14.25F);
            SkipTxt.Location = new Point(432, 601);
            SkipTxt.Name = "SkipTxt";
            SkipTxt.PlaceholderText = "Skip";
            SkipTxt.Size = new Size(171, 33);
            SkipTxt.TabIndex = 5;
            // 
            // LimitTxt
            // 
            LimitTxt.Font = new Font("Segoe UI", 14.25F);
            LimitTxt.Location = new Point(610, 601);
            LimitTxt.Name = "LimitTxt";
            LimitTxt.PlaceholderText = "Limit";
            LimitTxt.Size = new Size(171, 33);
            LimitTxt.TabIndex = 6;
            // 
            // GetUsersBtn
            // 
            GetUsersBtn.FlatStyle = FlatStyle.Flat;
            GetUsersBtn.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            GetUsersBtn.Location = new Point(537, 694);
            GetUsersBtn.Name = "GetUsersBtn";
            GetUsersBtn.Size = new Size(134, 53);
            GetUsersBtn.TabIndex = 7;
            GetUsersBtn.Text = "Получить";
            GetUsersBtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(291, 514);
            label1.Name = "label1";
            label1.Size = new Size(632, 37);
            label1.TabIndex = 8;
            label1.Text = "При необходимости заполните поля фильтрации";
            // 
            // SexComboBox
            // 
            SexComboBox.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            SexComboBox.ForeColor = SystemColors.WindowFrame;
            SexComboBox.FormattingEnabled = true;
            SexComboBox.Items.AddRange(new object[] { "Male", "Female" });
            SexComboBox.Location = new Point(432, 641);
            SexComboBox.Name = "SexComboBox";
            SexComboBox.Size = new Size(171, 33);
            SexComboBox.TabIndex = 10;
            SexComboBox.Text = "Sex";
            // 
            // RoleBox
            // 
            RoleBox.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            RoleBox.ForeColor = SystemColors.WindowFrame;
            RoleBox.FormattingEnabled = true;
            RoleBox.Items.AddRange(new object[] { "Admin", "Operator", "Specialist", "Verification", "Role" });
            RoleBox.Location = new Point(610, 562);
            RoleBox.Name = "RoleBox";
            RoleBox.Size = new Size(171, 33);
            RoleBox.TabIndex = 11;
            RoleBox.Text = "Role";
            // 
            // BackBtn
            // 
            BackBtn.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            BackBtn.Location = new Point(12, 12);
            BackBtn.Name = "BackBtn";
            BackBtn.Size = new Size(38, 23);
            BackBtn.TabIndex = 12;
            BackBtn.Text = "<--";
            BackBtn.UseVisualStyleBackColor = true;
            // 
            // UsersForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1205, 778);
            Controls.Add(BackBtn);
            Controls.Add(RoleBox);
            Controls.Add(SexComboBox);
            Controls.Add(label1);
            Controls.Add(GetUsersBtn);
            Controls.Add(LimitTxt);
            Controls.Add(SkipTxt);
            Controls.Add(DepartmentTxt);
            Controls.Add(dataGridView1);
            Name = "UsersForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "UsersForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private TextBox DepartmentTxt;
        private TextBox SkipTxt;
        private TextBox LimitTxt;
        private Button GetUsersBtn;
        private Label label1;
        private ComboBox SexComboBox;
        private ComboBox RoleBox;
        private Button BackBtn;
    }
}