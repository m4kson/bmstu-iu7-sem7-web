using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.GUI.Interfaces;
using ProdMonitor.View.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ProdMonitor.GUI.View
{
    public partial class RegistrationForm : Form, IRegView
    {
        public RegistrationForm()
        {
            InitializeComponent();
            RegButton.Click += (s, e) => RegisterClicked?.Invoke(s, e);
        }

        public string UserName => NameTxt.Text;
        public string Surname => SurnameTxt.Text;
        public string Patronymic => PatronymicTxt.Text;
        public string Department => DepartmentTxt.Text;
        public string Email => MailTxt.Text;
        public string Password => PasswordTxt.Text;

        public DateOnly BirthDay => DateOnly.FromDateTime(BirthDayPicker.Value);

        public SexTypeView Sex => (SexTypeView)SexComboBox.SelectedIndex;

        public event EventHandler RegisterClicked;

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void Close()
        {
            this.Hide();
        }
    }
}
