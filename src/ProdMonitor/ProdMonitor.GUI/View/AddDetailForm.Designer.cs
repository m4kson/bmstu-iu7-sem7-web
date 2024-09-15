namespace ProdMonitor.GUI.View
{
    partial class AddDetailForm
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
            NameTxt = new TextBox();
            CountryTxt = new TextBox();
            PriceTxt = new TextBox();
            AmauntTxt = new TextBox();
            LengthTxt = new TextBox();
            HeightTxt = new TextBox();
            WidthTxt = new TextBox();
            DetailCreateBtn = new Button();
            label1 = new Label();
            BackBtn = new Button();
            SuspendLayout();
            // 
            // NameTxt
            // 
            NameTxt.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            NameTxt.Location = new Point(93, 135);
            NameTxt.Name = "NameTxt";
            NameTxt.PlaceholderText = "Имя";
            NameTxt.Size = new Size(158, 33);
            NameTxt.TabIndex = 0;
            // 
            // CountryTxt
            // 
            CountryTxt.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            CountryTxt.Location = new Point(93, 174);
            CountryTxt.Name = "CountryTxt";
            CountryTxt.PlaceholderText = "Страна";
            CountryTxt.Size = new Size(158, 33);
            CountryTxt.TabIndex = 1;
            // 
            // PriceTxt
            // 
            PriceTxt.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PriceTxt.Location = new Point(93, 252);
            PriceTxt.Name = "PriceTxt";
            PriceTxt.PlaceholderText = "Цена";
            PriceTxt.Size = new Size(158, 33);
            PriceTxt.TabIndex = 2;
            // 
            // AmauntTxt
            // 
            AmauntTxt.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            AmauntTxt.Location = new Point(93, 213);
            AmauntTxt.Name = "AmauntTxt";
            AmauntTxt.PlaceholderText = "Количество";
            AmauntTxt.Size = new Size(158, 33);
            AmauntTxt.TabIndex = 3;
            // 
            // LengthTxt
            // 
            LengthTxt.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            LengthTxt.Location = new Point(93, 291);
            LengthTxt.Name = "LengthTxt";
            LengthTxt.PlaceholderText = "Длина";
            LengthTxt.Size = new Size(158, 33);
            LengthTxt.TabIndex = 4;
            // 
            // HeightTxt
            // 
            HeightTxt.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            HeightTxt.Location = new Point(93, 330);
            HeightTxt.Name = "HeightTxt";
            HeightTxt.PlaceholderText = "Высота";
            HeightTxt.Size = new Size(158, 33);
            HeightTxt.TabIndex = 5;
            // 
            // WidthTxt
            // 
            WidthTxt.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            WidthTxt.Location = new Point(93, 369);
            WidthTxt.Name = "WidthTxt";
            WidthTxt.PlaceholderText = "Ширина";
            WidthTxt.Size = new Size(158, 33);
            WidthTxt.TabIndex = 6;
            // 
            // DetailCreateBtn
            // 
            DetailCreateBtn.FlatStyle = FlatStyle.Flat;
            DetailCreateBtn.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            DetailCreateBtn.Location = new Point(93, 420);
            DetailCreateBtn.Name = "DetailCreateBtn";
            DetailCreateBtn.Size = new Size(158, 38);
            DetailCreateBtn.TabIndex = 7;
            DetailCreateBtn.Text = "Создать";
            DetailCreateBtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(56, 72);
            label1.Name = "label1";
            label1.Size = new Size(228, 37);
            label1.TabIndex = 8;
            label1.Text = "Создание детали";
            // 
            // BackBtn
            // 
            BackBtn.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            BackBtn.Location = new Point(12, 12);
            BackBtn.Name = "BackBtn";
            BackBtn.Size = new Size(33, 20);
            BackBtn.TabIndex = 9;
            BackBtn.Text = "<--";
            BackBtn.UseVisualStyleBackColor = true;
            // 
            // AddDetailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(335, 497);
            Controls.Add(BackBtn);
            Controls.Add(label1);
            Controls.Add(DetailCreateBtn);
            Controls.Add(WidthTxt);
            Controls.Add(HeightTxt);
            Controls.Add(LengthTxt);
            Controls.Add(AmauntTxt);
            Controls.Add(PriceTxt);
            Controls.Add(CountryTxt);
            Controls.Add(NameTxt);
            Name = "AddDetailForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AddDetailForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox NameTxt;
        private TextBox CountryTxt;
        private TextBox PriceTxt;
        private TextBox AmauntTxt;
        private TextBox LengthTxt;
        private TextBox HeightTxt;
        private TextBox WidthTxt;
        private Button DetailCreateBtn;
        private Label label1;
        private Button BackBtn;
    }
}