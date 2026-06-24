using ADN_Test.Dtos;
using ADN_Test.Models;
using ADN_Test.Service;
using ADN_Test.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ADN_Test.ViewModels
{
    public class CreateEmbarqueViewModel : ViewModelBase
    {
        private readonly IEmbarqueService _embarqueService;
        private readonly ICentroOperativoService _centroOperativoService;
        private readonly ITractoService _tractoService;
        private readonly IEmpleadoService _empleadoService;

        public ObservableCollection<CentroOperativo> CentrosOperativos { get; } = new();
        public ObservableCollection<Tracto> Tractos { get; } = new();
        public ObservableCollection<EmpleadoWithNombreDto> Conductores { get; } = new();

        private CentroOperativo? _selectedCentroOperativo;
        public CentroOperativo? SelectedCentroOperativo
        {
            get => _selectedCentroOperativo;
            set => SetProperty(ref _selectedCentroOperativo, value);
        }

        private Tracto? _selectedTracto;
        public Tracto? SelectedTracto
        {
            get => _selectedTracto;
            set => SetProperty(ref _selectedTracto, value);
        }

        private EmpleadoWithNombreDto? _selectedConductor;
        public EmpleadoWithNombreDto? SelectedConductor
        {
            get => _selectedConductor;
            set => SetProperty(ref _selectedConductor, value);
        }

        private string _pesoTeoricoERP = "";
        public string PesoTeoricoERP
        {
            get => _pesoTeoricoERP;
            set => SetProperty(ref _pesoTeoricoERP, value);
        }

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

        public AsyncRelayCommand GuardarCommand { get; }
        public RelayCommand CerrarCommand { get; }

        public event EventHandler? RequestClose;

        public CreateEmbarqueViewModel()
        {
            _embarqueService = new EmbarqueService();
            _centroOperativoService = new CentroOperativoService();
            _tractoService = new TractoService();
            _empleadoService = new EmpleadoService();

            GuardarCommand = new AsyncRelayCommand(GuardarAsync, _ => !IsLoading);
            CerrarCommand = new RelayCommand(() => RequestClose?.Invoke(this, EventArgs.Empty));

            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            IsLoading = true;
            ErrorMessage = null;
            try
            {
                var centrosTask = _centroOperativoService.GetAll();
                var tractosTask = _tractoService.GetAllTractos();
                var conductoresTask = _empleadoService.GetEmpleadosWithNombre();

                await Task.WhenAll(centrosTask, tractosTask, conductoresTask);

                // Filling combo box to complete the form
                foreach (var c in centrosTask.Result)
                    CentrosOperativos.Add(c);
                foreach (var t in tractosTask.Result)
                    Tractos.Add(t);
                foreach (var c in conductoresTask.Result)
                    Conductores.Add(c);
            }
            catch (Exception ex)
            {
                ErrorMessage = "No se pudieron cargar los datos. Verifica tu conexión.";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task GuardarAsync(object? parameter)
        {
            // If some input is not selected any value, shows an error
            if (SelectedCentroOperativo is null)
            {
                MessageBox.Show("Selecciona un centro operativo.");
                return;
            }
            if (SelectedTracto is null)
            {
                MessageBox.Show("Selecciona un tracto.");
                return;
            }
            if (SelectedConductor is null)
            {
                MessageBox.Show("Selecciona un conductor.");
                return;
            }
            if (!decimal.TryParse(PesoTeoricoERP, out decimal peso) || peso <= 0)
            {
                MessageBox.Show("Ingresa un peso teórico ERP válido.");
                return;
            }

            var dto = new EmbarqueInsertDto
            {
                Centro_Operativo_Id = SelectedCentroOperativo.Id,
                Tracto_Id = SelectedTracto.Id,
                Conductor_Id = SelectedConductor.Id,
                Peso_Teorico_ERP = peso
            };

            IsLoading = true;
            try
            {
                await _embarqueService.CreateEmbarque(dto);
                MessageBox.Show("Embarque registrado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                RequestClose?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el embarque: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
