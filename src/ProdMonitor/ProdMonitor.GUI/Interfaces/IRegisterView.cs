using ProdMonitor.View.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.GUI.Interfaces
{
    public interface IRegView
    {
        string UserName { get; }
        string Surname { get; }
        string Patronymic { get; }
        string Department { get; }
        string Email { get; }
        string Password { get; }
        DateOnly BirthDay { get; }
        SexTypeView Sex { get; }

        event EventHandler RegisterClicked;

        void ShowMessage(string message);
        void Show();
        void Close();
    }
}
