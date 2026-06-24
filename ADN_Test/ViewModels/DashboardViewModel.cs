using ADN_Test.Dtos;
using ADN_Test.Models;
using ADN_Test.Service;
using ADN_Test.Views;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ADN_Test.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IEmbarqueService _embarqueService;

        public ObservableCollection<EmbarqueResponseDto> Embarques { get; } = [];

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

        public RelayCommand NuevoEmbarqueCommand { get; }

        public AsyncRelayCommand ImportarExcelCommand { get; }

        public DashboardViewModel(IEmbarqueService embarqueService)
        {
            _embarqueService = embarqueService;
            RecargarCommand = new AsyncRelayCommand(CargarEmbarquesAsync);
            AbrirDetalleCommand = new RelayCommand(AbrirDetalle);
            NuevoEmbarqueCommand = new RelayCommand(NuevoEmbarque);
            ImportarExcelCommand = new AsyncRelayCommand(ImportarExcelAsync);
            _ = CargarEmbarquesAsync();
        }

        private void AbrirDetalle(object? parameter)
        {
            if (parameter is not EmbarqueResponseDto embarque)
                return;

            var detailVm = new EmbarqueDetailViewModel(embarque);
            var window = new EmbarqueDetailWindow(detailVm);
            window.ShowDialog();
        }

        private void NuevoEmbarque()
        {
            var createVm = new CreateEmbarqueViewModel();
            var window = new CreateEmbarqueWindow(createVm);
            window.ShowDialog();
            _ = CargarEmbarquesAsync();
        }

        private async Task ImportarExcelAsync()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Archivos Excel (*.xlsx)|*.xlsx|Todos los archivos (*.*)|*.*",
                Title = "Seleccionar archivo Excel de embarques"
            };

            if (dialog.ShowDialog() != true)
                return;

            IsLoading = true;
            ErrorMessage = null;

            try
            {
                var result = await _embarqueService.ImportFromExcelAsync(dialog.FileName);

                var msg = $"Importación completada.\n\n" +
                          $"Total de filas: {result.TotalRows}\n" +
                          $"Procesadas correctamente: {result.SuccessCount}\n" +
                          $"Errores: {result.ErrorCount}";

                if (result.Errors.Count > 0)
                {
                    var detalles = string.Join("\n", result.Errors.Take(10));
                    if (result.Errors.Count > 10)
                        detalles += $"\n... y {result.Errors.Count - 10} error(es) más";
                    msg += $"\n\nDetalles:\n{detalles}";
                }

                System.Windows.MessageBox.Show(msg, "Resultado de Importación",
                    System.Windows.MessageBoxButton.OK,
                    result.ErrorCount > 0
                        ? System.Windows.MessageBoxImage.Warning
                        : System.Windows.MessageBoxImage.Information);

                await CargarEmbarquesAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al importar: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task CargarEmbarquesAsync()
        {
            IsLoading = true;
            ErrorMessage = null;
            try
            {
                var registros = await _embarqueService.GetAll();
                Embarques.Clear();
                foreach (var registro in registros.OrderBy(registro => registro.Fecha_Salida))
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
