using ADN_Test.Dtos;
using ADN_Test.Models;
using ADN_Test.Service;
using System;
using System.Diagnostics;
using System.Windows;

namespace ADN_Test.ViewModels
{
    public class EmbarqueDetailViewModel : ViewModelBase
    {
        private readonly EmbarqueResponseDto _embarque;
        private readonly EmbarqueService _embarqueService;

        public string Placa_Tracto => _embarque.Placa_Tracto;
        public string Nombre_Conductor => _embarque.Nombre_Conductor;
        public decimal Peso_Teorico_ERP => _embarque.Peso_Teorico_ERP;

        // The variable is a string because I have some issues to accept "." on decimal number in the TextBox
        private string _pesoBasculaSalida = "";

        private bool _requireJustification;
        private bool _enableSaveButton = true;
        private string? _errorMessage;
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }
        public string? ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public string Peso_Bascula_Salida
        {
            get => _pesoBasculaSalida;
            set
            {
                if(SetProperty(ref _pesoBasculaSalida, value))
                {
                    // On any change will call to recalculate the Peso_Neto_Real and check if requires Justificacion_Diferencia
                    Recalculate();
                }
            }
        }

        // Value to enable or disable the Justiciacion_Diferencia field
        public bool RequireJustification
        {
            get => _requireJustification;
            set => SetProperty(ref _requireJustification, value);
        }

        // In case already authorize the cargo, "Authorizar Salida" will be disabled
        public bool EnableSaveButton
        {
            get => _enableSaveButton;
            set => SetProperty(ref _enableSaveButton, value);
        }

        private decimal? _pesoNetoReal;
        public decimal? Peso_Neto_Real
        {
            get => _pesoNetoReal;
            set => SetProperty(ref _pesoNetoReal, value);
        }

        private string _justificacionDiferencia = string.Empty;
        public string Justificacion_Diferencia
        {
            get => _justificacionDiferencia;
            set => SetProperty(ref _justificacionDiferencia, value);
        }

        public AsyncRelayCommand GuardarCommand { get; }
        public RelayCommand CerrarCommand { get; }

        public event EventHandler? RequestClose;

        public EmbarqueDetailViewModel(EmbarqueResponseDto embarque)
        {
            _embarque = embarque;
            _embarqueService = new EmbarqueService();

            _pesoBasculaSalida = embarque.Peso_Bascula_Salida.ToString();
            _pesoNetoReal = embarque.Peso_Neto_Real;
            _justificacionDiferencia = embarque.Justificacion_Diferencia;

            // Disable Button of "Autorizar Salida"
            if (_embarque.Fecha_Salida.HasValue)
            {
                EnableSaveButton = false;
            }

            GuardarCommand = new AsyncRelayCommand(Guardar, _ => !IsLoading);
            CerrarCommand = new RelayCommand(() => RequestClose?.Invoke(this, EventArgs.Empty));
        }

        private void Recalculate()
        {
            // Converting to decimal to use in oprations
            if (!decimal.TryParse(Peso_Bascula_Salida, out decimal pesoBascula))
            {
                Peso_Neto_Real = null;
                RequireJustification = false;
                return;
            }

            Peso_Neto_Real = pesoBascula - _embarque.Peso_Tara;

            decimal differencePercent =
                Math.Abs((Peso_Neto_Real.Value - Peso_Teorico_ERP) /
                         Peso_Teorico_ERP) * 100m;

            // If the difference is greater or equal to 3% enable Justiciacion_Diferencia TextBox
            RequireJustification = differencePercent >= 3m;
        }

        private async Task Guardar(object? parameter)
        {
            // Converting to decimal to confirm is a number
            if (!decimal.TryParse(Peso_Bascula_Salida, out decimal peso))
            {
                MessageBox.Show("Peso inválido.");
                return;
            }

            _embarque.Peso_Bascula_Salida = peso;
            _embarque.Peso_Neto_Real = Peso_Neto_Real;
            _embarque.Justificacion_Diferencia = Justificacion_Diferencia;

            IsLoading = true;
            try
            {
                await _embarqueService.UpdateSalida(
                    new EmbarqueUpdateSalida()
                    {
                        Id = _embarque.Id,
                        Justificacion_Diferencia = _embarque.Justificacion_Diferencia,
                        Peso_Bascula_Salida = _embarque.Peso_Bascula_Salida.Value,
                        Peso_Neto_Real = _embarque.Peso_Neto_Real.Value
                    });

                MessageBox.Show("Salida de embarque autorizada", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                RequestClose?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception) 
            {
                ErrorMessage = "Hubo un error, por favor verifica tu conexión";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
