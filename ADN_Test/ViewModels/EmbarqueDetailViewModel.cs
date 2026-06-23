using ADN_Test.Models;
using ADN_Test.Service;
using System;
using System.Diagnostics;
using System.Windows;

namespace ADN_Test.ViewModels
{
    public class EmbarqueDetailViewModel : ViewModelBase
    {
        private readonly Embarque _embarque;
        private readonly EmbarqueService _embarqueService;

        public string Placa_Tracto => _embarque.Placa_Tracto;
        public string Nombre_Conductor => _embarque.Nombre_Conductor;
        public decimal Peso_Teorico_ERP => _embarque.Peso_Teorico_ERP;
        private string _pesoBasculaSalida = "";
        private bool _requireJustification;

        public string Peso_Bascula_Salida
        {
            get => _pesoBasculaSalida;
            set
            {
                if(SetProperty(ref _pesoBasculaSalida, value))
                {
                    Recalculate();
                }
            }
        }

        public bool RequireJustification
        {
            get => _requireJustification;
            set => SetProperty(ref _requireJustification, value);
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

        public RelayCommand GuardarCommand { get; }
        public RelayCommand CerrarCommand { get; }

        public event EventHandler? RequestClose;

        public EmbarqueDetailViewModel(Embarque embarque)
        {
            _embarque = embarque;
            _embarqueService = new EmbarqueService();

            _pesoBasculaSalida = embarque.Peso_Bascula_Salida.ToString();
            _pesoNetoReal = embarque.Peso_Neto_Real;
            _justificacionDiferencia = embarque.Justificacion_Diferencia;

            GuardarCommand = new RelayCommand(Guardar);
            CerrarCommand = new RelayCommand(() => RequestClose?.Invoke(this, EventArgs.Empty));
        }

        private void Recalculate()
        {
            if (!decimal.TryParse(Peso_Bascula_Salida, out decimal pesoBascula))
            {
                Peso_Neto_Real = null;
                RequireJustification = false;
                return;
            }

            // Aquí va tu fórmula real
            Peso_Neto_Real = pesoBascula - _embarque.Peso_Tara;

            decimal differencePercent =
                Math.Abs((Peso_Neto_Real.Value - Peso_Teorico_ERP) /
                         Peso_Teorico_ERP) * 100m;

            RequireJustification = differencePercent >= 3m;
        }

        private async void Guardar()
        {
            if (!decimal.TryParse(Peso_Bascula_Salida, out decimal peso))
            {
                MessageBox.Show("Peso inválido.");
                return;
            }

            _embarque.Peso_Bascula_Salida = peso;
            _embarque.Peso_Neto_Real = Peso_Neto_Real;
            _embarque.Justificacion_Diferencia = Justificacion_Diferencia;

            try
            {
                await _embarqueService.UpdateSalida(_embarque);
                RequestClose?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex) { }

        }
    }
}
