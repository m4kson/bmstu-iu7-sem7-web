using ProdMonitor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.GUI.Interfaces
{
    public interface IUsersView
    {
        event EventHandler GetUsersBtnClick;

        event EventHandler BackBtnClick;

        string DepartmentText { get; }
        string Sex { get; }
        string Role { get; }
        string SkipText { get; }
        string LimitText { get; }

        void DisplayUsers(List<User> users);

        void Show();
        void Close();
    }
}
