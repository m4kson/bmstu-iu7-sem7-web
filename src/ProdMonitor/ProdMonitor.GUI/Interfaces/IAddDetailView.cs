using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.GUI.Interfaces
{
    public interface IAddDetailView
    {
        string DetailName { get; }
        string Country { get; }
        float Price { get; }
        int Amount { get; }
        int Length { get; }
        int Height { get; }
        int Width { get; }

        event EventHandler CreateDetailBtnClick;
        event EventHandler BackBtnClick;

        void ClearForm();
        void ShowMessage(string message);
        void Show();
        void Close();
    }
}
