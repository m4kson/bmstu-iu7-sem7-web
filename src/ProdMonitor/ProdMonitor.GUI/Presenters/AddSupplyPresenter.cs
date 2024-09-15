using ProdMonitor.Application.Services;
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
    public class AddSupplyPresenter
    {
        private IAddSupplyView _view;
        private readonly ISupplyService _supplyService;
        private IAdminPannelView _adminPannelView;

        public AddSupplyPresenter(ISupplyService supplyService)
        {
            _supplyService = supplyService;
        }

        public void SetView(IAddSupplyView view, IAdminPannelView adminPannelView)
        {
            _view = view;
            _view.AddSupplyBtnClick += OnAddSupplyBtnClick;
            _adminPannelView = adminPannelView;
            _view.BackBtnClick += OnBackBtnClick;
        }

        private async void OnBackBtnClick(object sender, EventArgs e)
        {
            _view.Close();
            _adminPannelView.Show();
        }

        private async void OnAddSupplyBtnClick(object sender, EventArgs e)
        {
            var supplyCreate = new SupplyCreate(detailId: _view.DetailId,
                quantity: _view.Amaunt);

            try
            {
                var createdSupply = await _supplyService.CreateSupplyAsync(supplyCreate);
                _view.ShowMessage("Деталь успешно создана!");
                _view.ClearForm();
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка при создании поставки: {ex.Message}");
            }
        }
    }
}
