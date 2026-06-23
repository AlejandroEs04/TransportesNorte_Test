using ADN_Test.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADN_Test.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IEmbarqueService _embarqueService;

        private object _currentViewModel = null!;
        public object CurrentViewModel
        {
            get => _currentViewModel;
            private set => SetProperty(ref _currentViewModel, value);
        }

        public MainViewModel(IEmbarqueService embarqueService)
        {
            _embarqueService = embarqueService;
            NavigateToDashboard();
        }

        public void NavigateToDashboard()
        {
            var dashboardVm = new DashboardViewModel(_embarqueService);
            CurrentViewModel = dashboardVm;
        }
    }
}
