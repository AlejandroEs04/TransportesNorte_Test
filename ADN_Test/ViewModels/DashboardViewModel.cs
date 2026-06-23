using ADN_Test.Models;
using ADN_Test.Service;
using ADN_Test.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ADN_Test.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IEmbarqueService _embarqueService;

        public ObservableCollection<Embarque> Embarques { get; } = new();

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private string? _errorMessage;
        public string? ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public AsyncRelayCommand RecargarCommand { get; }

        public RelayCommand AbrirDetalleCommand { get; }

        public DashboardViewModel(IEmbarqueService embarqueService)
        {
            _embarqueService = embarqueService;
            RecargarCommand = new AsyncRelayCommand(CargarEmbarquesAsync);
            AbrirDetalleCommand = new RelayCommand(AbrirDetalle);
            _ = CargarEmbarquesAsync();
        }

        private void AbrirDetalle(object? parameter)
        {
            if (parameter is not Embarque embarque)
                return;

            var detailVm = new EmbarqueDetailViewModel(embarque);
            var window = new EmbarqueDetailWindow(detailVm);
            window.ShowDialog();
        }

        private async Task CargarEmbarquesAsync()
        {
            IsLoading = true;
            ErrorMessage = null;
            try
            {
                var registros = await _embarqueService.GetAll();
                Embarques.Clear();
                foreach (var registro in registros)
                    Embarques.Add(registro);
            }
            catch (Exception)
            {
                ErrorMessage = "No se pudo cargar la lista de embarques. Verifica tu conexión.";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
