using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1.Totvs
{
    public class SB2200H
    {

        [Key]
        public string B2_COD { get; set; }
        public string B2_FILIAL { get; set; }
        public string B2_LOCAL { get; set; }
        public double B2_QFIM { get; set; }
        public double B2_QATU { get; set; }
        public double B2_RESERVA { get; set; }
        public double B2_QPEDVEN { get; set; }

    }
}
