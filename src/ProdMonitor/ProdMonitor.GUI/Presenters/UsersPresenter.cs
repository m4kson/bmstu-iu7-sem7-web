using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.GUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.GUI.Presenters
{
    public class UsersPresenter
    {
        private  IUsersView _view;
        private readonly IUserService _userService;
        private IAdminPannelView _adminPannelView;

        public UsersPresenter(IUserService userService)
        {
            _userService = userService;
        }

        public void SetView(IUsersView view, IAdminPannelView adminPannelView)
        {
            _view = view;
            _view.GetUsersBtnClick += OnGetUsersBtnClick;
            _adminPannelView = adminPannelView;
            _view.BackBtnClick += OnBackBtnClick;
        }

        private async void OnBackBtnClick(object sender, EventArgs e)
        {
            _view.Close(); 
            _adminPannelView.Show(); 
        }

        private async void OnGetUsersBtnClick(object sender, EventArgs e)
        {
            // Преобразование строки в перечисление SexType
            SexType? sex = null;
            if (Enum.TryParse(_view.Sex, out SexType parsedSex))
            {
                sex = parsedSex;
            }

            // Преобразование строки в перечисление RoleType
            RoleType? role = null;
            if (Enum.TryParse(_view.Role, out RoleType parsedRole))
            {
                role = parsedRole;
            }
            

            var filter = new UserFilter
            (
                department: _view.DepartmentText,
                //birthDay: birthDay,
                sex: sex,
                role: role,
                skip: int.TryParse(_view.SkipText, out var skip) ? skip : 0,
                limit: int.TryParse(_view.LimitText, out var limit) ? limit : int.MaxValue
            );

            try
            {
                var users = await _userService.GetAllUsersAsync(filter);
                _view.DisplayUsers(users);
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show("Failed to retrieve users.");
            }
        }
    }
}
