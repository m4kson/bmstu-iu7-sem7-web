using ProdMonitor.GUI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProdMonitor.GUI.View
{
    public partial class AuthForm : Form, IAuthView
    {
        public AuthForm()
        {
            InitializeComponent();
            LoginButton.Click += (s, e) => LoginClicked?.Invoke(s, e);
        }

        public string Username => LoginTextBox.Text;
        public string Password => PasswordTextBox.Text;

        public event EventHandler LoginClicked;

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
