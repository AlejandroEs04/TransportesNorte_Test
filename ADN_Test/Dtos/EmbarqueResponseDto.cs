using System;
using System.Collections.Generic;
using System.Text;

namespace ADN_Test.Dtos
{
    public class EmbarqueResponseDto
    {
        public int Id { get; set; }
        public string Folio { get; set; } = "";
        public string Centro_Operativo { get; set; } = "";
        public string Placa_Tracto { get; set; } = "";
        public string Nombre_Conductor { get; set; } = "";
        public decimal Peso_Teorico_ERP { get; set; }
        public decimal Peso_Tara { get; set; }
        public decimal? Peso_Bascula_Salida { get; set; }
        public decimal? Peso_Neto_Real { get; set; }
        public string Justificacion_Diferencia { get; set; } = string.Empty;
        public DateTime Fecha_Registro { get; set; }
        public DateTime? Fecha_Salida { get; set; }
    }
}
