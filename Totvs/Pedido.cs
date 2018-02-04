using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1.Totvs
{
    public class SC5200H
    {

        [Key]
        public string C5_NUM { get; set; }
        public string C5_FILIAL { get; set; }
        public string C5_TIPO { get; set; }
        public string C5_CLIENTE { get; set; }
        public string C5_CONDPAG { get; set; }
        public string C5_VEND1 { get; set; }
        public string C5_EMISSAO { get; set; }
        public string C5_PEDSITE { get; set; }
        public string C5_XSTATUS { get; set; }

    }
}
