using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1.Totvs
{
    public class SB1200H
    {

        [Key]
        public string B1_COD { get; set; }
        public string B1_FILIAL { get; set; }
        public string B1_DESC { get; set; }
        public string B1_TIPO { get; set; }
        public string B1_UM { get; set; }
        public string B1_MSBLQL { get; set; }
        public double B1_UPRC { get; set; }

    }
}
