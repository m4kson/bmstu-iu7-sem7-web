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
    public partial class AddSupply : Form, IAddSupplyView
    {
        public AddSupply()
        {
            InitializeComponent();
            AddSupplyBtn.Click += (sender, e) => AddSupplyBtnClick?.Invoke(sender, e);
            BackBtn.Click += (sender, e) => BackBtnClick?.Invoke(sender, e);
        }

        public event EventHandler AddSupplyBtnClick;
        public event EventHandler BackBtnClick;

        public Guid DetailId => Guid.Parse(DetailIdTxt.Text);
        public int Amaunt => int.Parse(AmauntTxt.Text);

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void ClearForm()
        {
            DetailIdTxt.Clear();
            AmauntTxt.Clear();
        }

        public void Close()
        {
            this.Hide();
        }

    }
}
