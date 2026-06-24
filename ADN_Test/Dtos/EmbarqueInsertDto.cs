using System;
using System.Collections.Generic;
using System.Text;

namespace ADN_Test.Dtos
{
    public class EmbarqueInsertDto
    {
        public int Centro_Operativo_Id { get; set; }
        public int Tracto_Id { get; set; }
        public int Conductor_Id { get; set; }
        public decimal Peso_Teorico_ERP {  get; set; }
    }
}
