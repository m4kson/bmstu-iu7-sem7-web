using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Domain.Models;
using ProdMonitor.GUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.GUI.Presenters
{
    public class AddDetailPresenter
    {
        private  IAddDetailView _view;
        private readonly IDetailService _detailService;
        private  IAdminPannelView _adminPannelView;

        public AddDetailPresenter(IDetailService detailService)
        {
            _detailService = detailService;
        }

        public void SetView(IAddDetailView view,
            IAdminPannelView adminPannelView)
        {
            _view = view;
            _adminPannelView = adminPannelView;

            _view.BackBtnClick += OnBackBtnClick;
            _view.CreateDetailBtnClick += OnCreateDetailBtnClick;
        }

        private async void OnBackBtnClick(object sender, EventArgs e)
        {
            _view.Close();
            _adminPannelView.Show();
        }

        private async void OnCreateDetailBtnClick(object sender, EventArgs e)
        {
            var detailCreate = new DetailCreate
            (
                name: _view.DetailName,
                country: _view.Country,
                price: _view.Price,
                amount: _view.Amount,
                length: _view.Length,
                height: _view.Height,
                width: _view.Width
            );

            try
            {
                var createdDetail = await _detailService.CreateDetailAsync(detailCreate);
                _view.ShowMessage("Деталь успешно создана!");
                _view.ClearForm();
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка при создании детали: {ex.Message}");
            }
        }
    }
}
