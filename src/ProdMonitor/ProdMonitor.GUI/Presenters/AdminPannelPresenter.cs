using ProdMonitor.GUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.GUI.Presenters
{
    public class AdminPannelPresenter
    {
        private IAdminPannelView _view;
        private IUsersView _usersView;
        private IChangeRoleView _changeRoleView;
        private IAddTractorView _addTractorView;
        private IAddLineView _addLineView;
        private IAddDetailView _addDetailView;
        private ITractorsView _tractorsView;
        private ILinesView _linesView;
        private IReportsView _reportsView;
        private IDetailsView _detailsView;
        private IGetDetailView _detailView;
        private IGetLineView _lineView;
        private IGetTractorView _tractorView;
        private IAddSupplyView _supplyView;


        public AdminPannelPresenter()
        {
            
        }

        public void SetView(IAdminPannelView view,
            IUsersView usersView,
            IChangeRoleView changeRoleView,
            IAddTractorView addTractorView,
            IAddLineView addLineView,
            IAddDetailView addDetailView,
            ITractorsView tractorsView,
            ILinesView linesView,
            IReportsView reportsView,
            IDetailsView detailsView,
            IGetDetailView detailView,
            IGetLineView lineView,
            IGetTractorView tractorView,
            IAddSupplyView supplyView)
        {
            _view = view;
            _usersView = usersView;
            _changeRoleView = changeRoleView;
            _addTractorView = addTractorView;
            _addLineView = addLineView;
            _addDetailView = addDetailView;
            _tractorsView = tractorsView;
            _linesView = linesView;
            _reportsView = reportsView;
            _detailsView = detailsView;
            _detailView = detailView;
            _lineView = lineView;
            _tractorView = tractorView;
            _supplyView = supplyView;

            
            _view.GetUsersClicked += OnGetUsersClicked;
            _view.ChangeRoleClicked += OnChangeRoleClicked;
            _view.AddTracktorClicked += OnAddTractorClicked;
            _view.AddLineClicked += OnAddLineClicked;
            _view.AddDetailClicked += OnAddDetailClicked;
            _view.GetTracktorsClicked += OnGetTractorsClicked;
            _view.GetLinesClicked += OnGetLinesClicked;
            _view.GetReportsClicked += OnGetReportsClicked;
            _view.GetDetailsClicked += OnGetDetailsClicked;
            _view.GetDetailClicked += OnGetDetailClicked;
            _view.GetLineClicked += OnGetLineClicked;
            _view.GetTractorCLicked += OnGetTractorClicked;
            _view.AddSupplyBtnClicked += OnAddSupplyBtnClicked;
        }

        private void OnAddSupplyBtnClicked(object sender, EventArgs e)
        {
            _view.Close();
            _supplyView.Show();
        }

        private void OnGetUsersClicked(object sender, EventArgs e)
        {
            _view.Close();
            _usersView.Show();
        }

        private void OnChangeRoleClicked(object sender, EventArgs e)
        {
            _view.Close();
            _changeRoleView.Show();
        }

        private void OnAddTractorClicked(object sender, EventArgs e)
        {
            _view.Close();
            _addTractorView.Show();
        }

        private void OnAddLineClicked(object sender, EventArgs e)
        {
            _view.Close();
            _addLineView.Show();
        }

        private void OnAddDetailClicked(object sender, EventArgs e)
        {
            _view.Close();
            _addDetailView.Show();
        }

        private void OnGetTractorsClicked(object sender, EventArgs e)
        {
            _view.Close();
            _tractorsView.Show();
        }

        private void OnGetLinesClicked(object sender, EventArgs e)
        {
            _view.Close();
            _linesView.Show();
        }

        private void OnGetReportsClicked(object sender, EventArgs e)
        {
            _view.Close();
            _reportsView.Show();
        }

        private void OnGetDetailsClicked(object sender, EventArgs e)
        {
            _view.Close();
            _detailsView.Show();
        }

        private void OnGetDetailClicked(object sender, EventArgs e)
        {
            _view.Close();
            _detailView.Show();
        }

        private void OnGetLineClicked(object sender, EventArgs e)
        {
            _view.Close();
            _lineView.Show();
        }

        private void OnGetTractorClicked(object sender, EventArgs e)
        {
            _view.Close();
            _tractorView.Show();
        }
    }
}
