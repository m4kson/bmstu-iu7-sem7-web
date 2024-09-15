using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.GUI.Interfaces;
using System;

namespace ProdMonitor.GUI.Presenters
{
    public class StartPresenter
    {
        private IStartView _view;
        private IAuthView _authView;
        private IRegView _regView;

        public StartPresenter()
        {
        }

        public void SetView(IStartView view, IAuthView authView, IRegView regView)
        {
            _view = view;
            _authView = authView;
            _regView = regView;

            _view.LoginClicked += OnLoginClicked;
            _view.RegisterClicked += OnRegisterClicked;
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            _view.Close();
            _authView.Show();
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            _view.Close();
            _regView.Show();
        }
    }
}
