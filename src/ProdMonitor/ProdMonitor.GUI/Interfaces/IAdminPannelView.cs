using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.GUI.Interfaces
{
    public interface IAdminPannelView
    {
        event EventHandler GetUsersClicked;
        event EventHandler ChangeRoleClicked;
        event EventHandler AddTracktorClicked;
        event EventHandler AddLineClicked;
        event EventHandler AddDetailClicked;
        event EventHandler GetTracktorsClicked;
        event EventHandler GetLinesClicked;
        event EventHandler GetReportsClicked;
        event EventHandler GetDetailsClicked;
        event EventHandler GetDetailClicked;
        event EventHandler GetLineClicked;
        event EventHandler GetTractorCLicked;
        event EventHandler AddSupplyBtnClicked;

        void Show();
        void Close();
    }
}
