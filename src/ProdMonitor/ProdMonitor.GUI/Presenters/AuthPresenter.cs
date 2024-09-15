using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.GUI.Interfaces;
using ProdMonitor.GUI.View;
using System;
using System.Threading.Tasks;

namespace ProdMonitor.GUI.Presenters
{
    public class AuthPresenter
    {
        private IAuthView _view;
        private readonly IAuthenticationService _authService;

        private  IAdminPannelView _adminPanelView;
        private  IOperatorPannelView _operatorPanelView;
        private  ISpecialistPannelView _specialistPanelView;
        private  IVerificationPannelView _verificationView;

        public AuthPresenter(IAuthenticationService authService)
        {
            _authService = authService;
        }

        public void SetView(IAuthView view,
            IAdminPannelView adminPanelView)
        {
            _view = view;
            _view.LoginClicked += OnLoginClicked;

            _adminPanelView = adminPanelView;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var username = _view.Username;
            var password = _view.Password;
            var loginModel = new LoginModel(username, password);

            try
            {
                var user = await _authService.LoginAsync(loginModel);

                if (_view is Form form)
                {
                    form.Invoke(new Action(() => HandleLoginSuccess(user)));
                }
            }
            catch (UserNotFoundException)
            {
                ShowErrorMessage("Пользователь не найден");
            }
            catch (WrongPasswordException)
            {
                ShowErrorMessage("Неверный пароль");
            }
            catch (LoginException)
            {
                ShowErrorMessage("Что-то пошло не так");
            }
        }

        private void HandleLoginSuccess(User user)
        {
            _view.ShowMessage("Добро пожаловать, " + user.Name + "!");

            switch (user.Role)
            {
                case RoleType.Admin:
                    _adminPanelView.Show();
                    break;
                case RoleType.Operator:
                    _operatorPanelView.Show();
                    break;
                case RoleType.Specialist:
                    _specialistPanelView.Show();
                    break;
                case RoleType.Verification:
                    _verificationView.Show();
                    break;
                default:
                    ShowErrorMessage("Неизвестная роль пользователя");
                    return;
            }

            if (_view is Form currentForm)
            {
                currentForm.Hide();
            }
        }

        private void ShowErrorMessage(string message)
        {
            if (_view is Form form)
            {
                if (form.InvokeRequired)
                {
                    form.Invoke(new Action(() => _view.ShowMessage(message)));
                }
                else
                {
                    _view.ShowMessage(message);
                }
            }
        }

        private void OpenForm(Form form)
        {
            form.Show();
        }
    }

}
