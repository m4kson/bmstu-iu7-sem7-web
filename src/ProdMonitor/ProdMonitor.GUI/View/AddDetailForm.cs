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
    public partial class AddDetailForm : Form, IAddDetailView
    {
        public AddDetailForm()
        {
            InitializeComponent();

            DetailCreateBtn.Click += (sender, e) => CreateDetailBtnClick?.Invoke(sender, e);
            BackBtn.Click += (sender, e) => BackBtnClick?.Invoke(sender, e);
        }

        public string DetailName => NameTxt.Text;
        public string Country => CountryTxt.Text;
        public float Price => float.TryParse(PriceTxt.Text, out var price) ? price : 0;
        public int Amount => int.TryParse(AmauntTxt.Text, out var amount) ? amount : 0;
        public int Length => int.TryParse(LengthTxt.Text, out var length) ? length : 0;
        public int Height => int.TryParse(HeightTxt.Text, out var height) ? height : 0;
        public int Width => int.TryParse(WidthTxt.Text, out var width) ? width : 0;

        
        public event EventHandler CreateDetailBtnClick;
        public event EventHandler BackBtnClick;

        public void ClearForm()
        {
            NameTxt.Clear();
            CountryTxt.Clear();
            PriceTxt.Clear();
            AmauntTxt.Clear();
            LengthTxt.Clear();
            HeightTxt.Clear();
            WidthTxt.Clear();
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
