using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1
{
    public class Pedido
    {
        public Pedido()
        {
            this.itens = new HashSet<PedidoItem>();
        }

        public Guid id { get; set; }
        public Guid? clienteId { get; set; }
        public string numeroPedido { get; set; }
        public string aplicacao { get; set; }
        public string numeroPedidoAplicacao { get; set; }
        public int numeroPedidoAplicacaoId { get; set; }
        public string numeroPedidoTotvs { get; set; }

        public string notaFiscal { get; set; }
        public int? status { get; set; }
        public int? hubStatus { get; set; }

        public string vendedor { get; set; }
        public string mensagem { get; set; }

        //datas
        public DateTime dataCriacao { get; set; }

        public decimal valorTotal { get; set; }
        public decimal valorDesconto { get; set; }
        public decimal valorFinal { get; set; }

        //forma de pagamento

        public string condicaoPagamento { get; set; }
        public int quantidadeParcelas { get; set; }

        //totvs
        public int integradoTotvs { get; set; }
        public DateTime dataIntegracaoTotvs { get; set; }
        public DateTime dataEnvioTotvs { get; set; }
        public string retornoTotvs { get; set; }

        //outros
        public decimal valorTotalFrete { get; set; }

        public Cliente cliente { get; set; }

        public ICollection<PedidoItem> itens { get; set; }

        //methods
        public void adicionarItem(
            string _produtoSku,
            string _produtoDescricao,
            string _brinde,
            decimal _quantidade,
            decimal _valorUnitario,
            decimal _valorDesconto)
        {

            //verificar se itens esta vazio
            if (this.itens == null)
            {
                this.itens = new List<PedidoItem>();
            }

            PedidoItem novoItem = new PedidoItem();
            novoItem.id = Guid.NewGuid();
            novoItem.quantidade = _quantidade;
            novoItem.produtoSku = _produtoSku;
            novoItem.produtoDescricao = _produtoDescricao;

            novoItem.brinde = _brinde;

            novoItem.valorUnitario = _valorUnitario;
            novoItem.valorDesconto = _valorDesconto;
            novoItem.valorTotal = novoItem.quantidade * novoItem.valorUnitario;
            novoItem.valorFinal = novoItem.valorTotal - novoItem.valorDesconto;

            //adicionar item
            this.itens.Add(novoItem);

            //atualizar pedido
            this.atualizarPedido();

        }

        public void atualizarPedido()
        {
            //atualizar valores

            //inicializa valores
            this.valorTotal = 0;
            this.valorFinal = 0;

            //percorrer itens
            if (this.itens != null)
            {
                foreach (PedidoItem item in this.itens)
                {
                    this.valorTotal = this.valorTotal + item.valorTotal;
                    this.valorDesconto = this.valorDesconto + item.valorDesconto;
                    this.valorFinal = this.valorFinal + item.valorFinal;
                }
            }
        }

    }

    public class PedidoItem
    {
        public Guid id { get; set; }
        public Guid PedidoId { get; set; }
        public decimal quantidade { get; set; }
        public string produtoSku { get; set; }
        public string produtoDescricao { get; set; }
        public decimal valorUnitario { get; set; }
        public decimal percentualDesconto { get; set; }
        public decimal valorDesconto { get; set; }
        public decimal valorTotal { get; set; }
        public decimal valorFinal { get; set; }
        public string brinde { get; set; }

        public ICollection<Pedido> Pedido { get; set; }
    }

}