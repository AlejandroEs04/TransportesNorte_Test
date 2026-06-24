using System;
using System.Collections.Generic;
using System.Text;

namespace ADN_Test.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre_Completo { get; set; } = "";
        public string No_Empleado { get; set; } = "";
        public int Posicion_Id { get; set; }
    }
}
