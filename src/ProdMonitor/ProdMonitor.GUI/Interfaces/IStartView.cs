using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.GUI.Interfaces
{
    public interface IStartView
    {
        event EventHandler LoginClicked;
        event EventHandler RegisterClicked;

        void Show();
        void Close();
    }
}
