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
    public partial class AdminPannel : Form, IAdminPannelView
    {
        public AdminPannel()
        {
            InitializeComponent();
            GetUsersBtn.Click += (s, e) => GetUsersClicked?.Invoke(s, e);
            ChangeRoleBtn.Click += (s, e) => ChangeRoleClicked?.Invoke(s, e);
            AddTractorBtn.Click += (s, e) => AddTracktorClicked?.Invoke(s, e);
            AddLineBtn.Click += (s, e) => AddLineClicked?.Invoke(s, e);
            AddDetailBtn.Click += (s, e) => AddDetailClicked?.Invoke(s, e);
            GetTractorsBtn.Click += (s, e) => GetTracktorsClicked?.Invoke(s, e);
            GetLinesBtn.Click += (s, e) => GetLinesClicked?.Invoke(s, e);
            GetReportsBtn.Click += (s, e) => GetReportsClicked?.Invoke(s, e);
            GetDetailsBtn.Click += (s, e) => GetDetailsClicked?.Invoke(s, e);
            GetDetailBtn.Click += (s, e) => GetDetailClicked?.Invoke(s, e);
            GetLineBtn.Click += (s, e) => GetLineClicked?.Invoke(s, e);
            GetTractorBtn.Click += (s, e) => GetTractorCLicked?.Invoke(s, e);
            AddSupplyBtn.Click += (s, e) => AddSupplyBtnClicked?.Invoke(s, e);
        }

        public event EventHandler AddSupplyBtnClicked;
        public event EventHandler GetUsersClicked;
        public event EventHandler ChangeRoleClicked;
        public event EventHandler AddTracktorClicked;
        public event EventHandler AddLineClicked;
        public event EventHandler AddDetailClicked;
        public event EventHandler GetTracktorsClicked;
        public event EventHandler GetLinesClicked;
        public event EventHandler GetReportsClicked;
        public event EventHandler GetDetailsClicked;
        public event EventHandler GetDetailClicked;
        public event EventHandler GetLineClicked;
        public event EventHandler GetTractorCLicked;

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
