using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Domain.Models;
using ProdMonitor.GUI.Helpers;
using ProdMonitor.GUI.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProdMonitor.GUI.Presenters
{
    public class RegisterPresenter
    {
        private IRegView _registerView;
        private readonly IAuthenticationService _authService;
        private IStartView _startView;

        public RegisterPresenter(IAuthenticationService authService)
        {
            _authService = authService;
        }

        public void SetView(IRegView registerView, IStartView startView)
        {
            _registerView = registerView;
            _registerView.RegisterClicked += OnRegisterClicked;
            _startView = startView;
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            var name = _registerView.UserName;
            var surname = _registerView.Surname;
            var patronymic = _registerView.Patronymic;
            var department = _registerView.Department;
            var email = _registerView.Email;
            var password = _registerView.Password;
            var birthDay = _registerView.BirthDay;
            var sex = _registerView.Sex;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                _registerView.ShowMessage("Заполните все обязательные поля.");
                return;
            }

            try
            {
                var result = await _authService.RegisterAsync(new RegisterModel
                (
                    email: email,
                    password: password,
                    name: name,
                    surname: surname,
                    patronymic: patronymic,
                    sex: SexTypeConverter.ToDomain(sex),
                    department: department,
                    birthDay: birthDay
                ));

                _registerView.ShowMessage("Регистрация успешна!");
                _registerView.Close(); 

                _startView.Show(); 
            }
            catch (UserAlreadyExistException)
            {
                _registerView.ShowMessage("Пользователь с таким логином уже существует");
            }
            catch (RegisterException)
            {
                _registerView.ShowMessage("Что-то пошло не так");
            }
        }
    }
}
