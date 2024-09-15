using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.GUI.Interfaces
{
    public interface IAddSupplyView
    {
        event EventHandler AddSupplyBtnClick;
        event EventHandler BackBtnClick;

        Guid DetailId { get; }
        int Amaunt {  get; }


        public void ClearForm();
        void ShowMessage(string message);
        void Show();
        void Close();
    }
}
