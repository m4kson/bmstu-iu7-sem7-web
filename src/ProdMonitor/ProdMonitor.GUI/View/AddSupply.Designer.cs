namespace ProdMonitor.GUI.View
{
    partial class AddSupply
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
            AddSupplyBtn = new Button();
            label1 = new Label();
            DetailIdTxt = new TextBox();
            AmauntTxt = new TextBox();
            BackBtn = new Button();
            SuspendLayout();
            // 
            // AddSupplyBtn
            // 
            AddSupplyBtn.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            AddSupplyBtn.Location = new Point(73, 238);
            AddSupplyBtn.Name = "AddSupplyBtn";
            AddSupplyBtn.Size = new Size(164, 42);
            AddSupplyBtn.TabIndex = 0;
            AddSupplyBtn.Text = "Добавить";
            AddSupplyBtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(38, 29);
            label1.Name = "label1";
            label1.Size = new Size(260, 74);
            label1.TabIndex = 1;
            label1.Text = "Добавить поставку \r\n         деталей";
            // 
            // DetailIdTxt
            // 
            DetailIdTxt.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            DetailIdTxt.Location = new Point(59, 148);
            DetailIdTxt.Name = "DetailIdTxt";
            DetailIdTxt.PlaceholderText = "Detail ID";
            DetailIdTxt.Size = new Size(193, 33);
            DetailIdTxt.TabIndex = 2;
            // 
            // AmauntTxt
            // 
            AmauntTxt.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            AmauntTxt.Location = new Point(59, 187);
            AmauntTxt.Name = "AmauntTxt";
            AmauntTxt.PlaceholderText = "Количество деталей";
            AmauntTxt.Size = new Size(193, 33);
            AmauntTxt.TabIndex = 3;
            // 
            // BackBtn
            // 
            BackBtn.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            BackBtn.Location = new Point(12, 3);
            BackBtn.Name = "BackBtn";
            BackBtn.Size = new Size(38, 23);
            BackBtn.TabIndex = 13;
            BackBtn.Text = "<--";
            BackBtn.UseVisualStyleBackColor = true;
            // 
            // AddSupply
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(330, 352);
            Controls.Add(BackBtn);
            Controls.Add(AmauntTxt);
            Controls.Add(DetailIdTxt);
            Controls.Add(label1);
            Controls.Add(AddSupplyBtn);
            Name = "AddSupply";
            Text = "AddSupply";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button AddSupplyBtn;
        private Label label1;
        private TextBox DetailIdTxt;
        private TextBox AmauntTxt;
        private Button BackBtn;
    }
}