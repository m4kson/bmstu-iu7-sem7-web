using ProdMonitor.Domain.Models;
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
    public partial class UsersForm : Form, IUsersView
    {
        public UsersForm()
        {
            InitializeComponent();
            dataGridView1.DataError += dataGridView1_DataError;
            GetUsersBtn.Click += (sender, e) => GetUsersBtnClick?.Invoke(sender, e);

            BackBtn.Click += (sender, e) => BackBtnClick?.Invoke(sender, e);
        }

        public event EventHandler GetUsersBtnClick;
        public event EventHandler BackBtnClick;

        public string DepartmentText => DepartmentTxt.Text;
        public string Sex => SexComboBox.SelectedItem?.ToString();
        public string Role => RoleBox.SelectedItem?.ToString();
        public string SkipText => SkipTxt.Text;
        public string LimitText => LimitTxt.Text;

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            
            e.ThrowException = false; 
        }

        public void DisplayUsers(List<User> users)
        {
            dataGridView1.DataSource = users;
        }

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
