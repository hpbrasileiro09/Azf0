using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1.Hub
{
    public class PedidosC
    {
        public PedidosC()
        {
            this.pedidos = new List<PedidoC>();
        }
        public ICollection<PedidoC> pedidos { get; set; }
    }

    public class PedidoC
    {
        public PedidoC()
        {
            this.itens = new List<PedidoItemC>();
        }
        public string pedido { get; set; }
        public string data { get; set; }
        public string hora { get; set; }
        public string cliente { get; set; }

        public string vendedor { get; set; }
        public string transportadora { get; set; }
        public string tipo_frete { get; set; }
        public decimal total_peso { get; set; }
        public decimal total_frete { get; set; }
        public decimal total_produtos { get; set; }
        public decimal total_pedido { get; set; }
        public decimal total_comissao { get; set; }
        public int pagamento_cod { get; set; }
        public string pagamento_forma { get; set; }
        public int pagamento_parcelas { get; set; }
        public string pagamento_operadora { get; set; }
        public string pagamento_cartao { get; set; }
        public decimal parcela01 { get; set; }
        public decimal parcela02 { get; set; }
        public decimal parcela03 { get; set; }
        public decimal parcela04 { get; set; }
        public decimal parcela05 { get; set; }
        public decimal parcela06 { get; set; }
        public decimal parcela07 { get; set; }
        public decimal parcela08 { get; set; }
        public decimal parcela09 { get; set; }
        public decimal parcela10 { get; set; }
        public decimal parcela11 { get; set; }
        public decimal parcela12 { get; set; }
        public string vencimento01 { get; set; }
        public string vencimento02 { get; set; }
        public string vencimento03 { get; set; }
        public string vencimento04 { get; set; }
        public string vencimento05 { get; set; }
        public string vencimento06 { get; set; }
        public string vencimento07 { get; set; }
        public string vencimento08 { get; set; }
        public string vencimento09 { get; set; }
        public string vencimento10 { get; set; }
        public string vencimento11 { get; set; }
        public string vencimento12 { get; set; }
        public string obs { get; set; }

        public ClienteC cadastro_cliente { get; set; }

        public ICollection<PedidoItemC> itens { get; set; }

    }

    public class PedidoItemC
    {
        public int pedido { get; set; }
        public string item { get; set; }
        public string produto { get; set; }
        public string armazem { get; set; }
        public string bonificar { get; set; }
        public int qtde { get; set; }
        public decimal preco_tabela { get; set; }
        public decimal preco_unitario { get; set; }
        public decimal total_produto { get; set; }
        public decimal total_comissao { get; set; }
        public decimal total_peso { get; set; }
        public string obs { get; set; }
        public int promocao_codigo { get; set; }

    }

    public class ClienteC
    {
        public string empresa { get; set; }
        public string cliente { get; set; }
        public string nome { get; set; }
        public string endereco { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string codibge { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string cep { get; set; }
        public string ddd { get; set; }
        public string fone { get; set; }
        public string fones { get; set; }
        public string email { get; set; }
        public string tipopessoa { get; set; }
        public string cpfcnpj { get; set; }
        public string rgie { get; set; }
        public string crotpd { get; set; }
        public string cro_Estado { get; set; }
        public string vendedor { get; set; }
        public string vendedor_anterior { get; set; }
        public string risco { get; set; }
        public int limite { get; set; }
        public string limite_vencimento { get; set; }
        public string grupo { get; set; }
        public string serasa { get; set; }
        public string especialidade { get; set; }
        public string bloqueado { get; set; }

    }

    public class EstoqueC
    {
        public EstoqueC()
        {
            this.produtos = new List<EstoqueItemC>();
        }
        public ICollection<EstoqueItemC> produtos { get; set; }
    }

    public class EstoqueItemC
    {
        public EstoqueItemC()
        {
            empresa = "";
            produto = "";
            armazem = "";
            estoque = 0;
        }

        public EstoqueItemC(
            string _empresa,
            string _produto,
            string _armazem,
            int _estoque)
        {
            empresa = _empresa;
            produto = _produto;
            armazem = _armazem;
            estoque = _estoque;
        }

        public string empresa { get; set; }
        public string produto { get; set; }
        public string armazem { get; set; }
        public int estoque { get; set; }
    }

    public class ProdutosC
    {
        public ProdutosC()
        {
            this.produtos = new List<ProdutoC>();
        }
        public ICollection<ProdutoC> produtos { get; set; }
    }

    public class ProdutoC
    {
        public string empresa { get; set; }
        public string produto { get; set; }
        public string descricao { get; set; }
        public string grupo { get; set; }
        public decimal pesobruto { get; set; }
        public decimal prazoentrega { get; set; }
        public string fornecedor { get; set; }
        public string fabricante { get; set; }
        public string compra_data { get; set; }
        public decimal compra_custo { get; set; }
        public decimal compra_icms { get; set; }
        public decimal compra_ipi { get; set; }
        public decimal compra_st { get; set; }
        public decimal compra_preco { get; set; }
        public string avulso { get; set; }
        public string avulso_pai { get; set; }
        public decimal avulso_fator { get; set; }
        public decimal similar { get; set; }
        public string descontinuado { get; set; }
        public decimal preco_sugerido { get; set; }
        public decimal preco_custofinanceiro { get; set; }
        public decimal preco_outroscustos { get; set; }
        public decimal preco_ata_frete { get; set; }
        public decimal preco_ata_comissao { get; set; }
        public decimal preco_ata_markup { get; set; }
        public decimal preco_var_frete { get; set; }
        public decimal preco_var_comissao { get; set; }
        public decimal preco_var_markup { get; set; }
        public decimal minimo_ata_markup { get; set; }
        public decimal minimo_var_markup { get; set; }
        public string bloqueado { get; set; }

    }

    public class StatusC
    {
        public StatusC()
        {
            this.status = new List<StatusItemC>();
        }
        public ICollection<StatusItemC> status { get; set; }
    }

    public class StatusItemC
    {
        public StatusItemC(
            string _pedido,
            string _aPI_Status,
            string _aPI_Codigo,
            string _aPI_NotaFiscal
        )
        {
            pedido = _pedido;
            aPI_Status = _aPI_Status;
            aPI_Codigo = _aPI_Codigo;
            aPI_NotaFiscal = _aPI_NotaFiscal;
        }
        public string pedido { get; set; }
        public string aPI_Status { get; set; }
        public string aPI_Codigo { get; set; }
        public string aPI_NotaFiscal { get; set; }
    }
}
