using System;
using System.Collections.Generic;
using System.Text;

namespace ADN_Test.Dtos
{
    public class EmbarqueUpdateSalida
    {
        public int Id { get; set; }
        public decimal Peso_Bascula_Salida { get; set; }
        public decimal Peso_Neto_Real { get; set; }
        public string Justificacion_Diferencia { get; set; } = string.Empty;
    }
}
