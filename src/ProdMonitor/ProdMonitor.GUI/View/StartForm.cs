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
    public partial class StartForm : Form, IStartView
    {
        public StartForm()
        {
            InitializeComponent();
            LoginButton.Click += (s, e) => LoginClicked?.Invoke(s, e);
            RegisterButton.Click += (s, e) => RegisterClicked?.Invoke(s, e);
        }

        public event EventHandler LoginClicked;
        public event EventHandler RegisterClicked;

        public void Close()
        {
            this.Hide();
        }
    }
}
