using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1
{ 
    public class Cliente
    {
        public Cliente()
        {
            Pedido = new HashSet<Pedido>();
        }

        public Guid id { get; set; }
        public string cpfCnpj { get; set; }
        public int? tipo { get; set; }
        public string nome { get; set; }
        public string nomeFantasia { get; set; }
        public string razaoSocial { get; set; }
        public string inscricaoEstadual { get; set; }
        public string cro { get; set; }
        public string croEstado { get; set; }
        public string email { get; set; }

        public DateTime dataCriacao { get; set; }

        //endereco
        public string endereco { get; set; }
        public string enderecoNumero { get; set; }
        public string enderecoComplemento { get; set; }
        public string enderecoBairro { get; set; }
        public string enderecoCep { get; set; }
        public string enderecoCidade { get; set; }
        public string enderecoEstado { get; set; }
        public string enderecoIbge { get; set; }
        public string enderecoReferencia { get; set; }

        public string telefone { get; set; }
        public string telefone2 { get; set; }
        public string telefone3 { get; set; }

        //endereco entrega
        public string enderecoEntrega { get; set; }
        public string enderecoEntregaNumero { get; set; }
        public string enderecoEntregaComplemento { get; set; }
        public string enderecoEntregaBairro { get; set; }
        public string enderecoEntregaCep { get; set; }
        public string enderecoEntregaCidade { get; set; }
        public string enderecoEntregaEstado { get; set; }
        public string enderecoEntregaIbge { get; set; }
        public string enderecoEntregaReferencia { get; set; }

        public string especialidade { get; set; }
        public string profissao { get; set; }

        public ICollection<Pedido> Pedido { get; set; }

    }
}
