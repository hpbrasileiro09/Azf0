using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1.Hub
{
    public class Token
    {

        public string access_token { get; set; }

    }

    public class Parametro
    {
        public int pagina { get; set; }
        public int totalRegistrosPorPagina { get; set; }
        public string dataInicio { get; set; }
        public string dataFim { get; set; }
        public string pedidoStatus { get; set; }
    }

    public class Resumo
    {
        public Resumo()
        {
            this.ordersNotFound = new List<string>();
        }
        public int range { get; set; }
        public string stateDate { get; set; }
        public string endDate { get; set; }
        public int processed { get; set; }
        public int notfound { get; set; }
        public int error { get; set; }
        public ICollection<string> ordersNotFound { get; set; }
    }

    public class PedidoL
    {
        public string id { get; set; }
        public string numero { get; set; }
        public string numeroNotaFiscal { get; set; }
        public string status { get; set; }
        public string subtotal { get; set; }
        public string desconto { get; set; }
        public string frete { get; set; }
        public string total { get; set; }
        public string criadoEm { get; set; }
        public string cliente_Tipo { get; set; }
        public string cliente_Documento { get; set; }
        public string cliente_Email { get; set; }
        public string cliente_Nome { get; set; }
        public string cliente_DataNascimento { get; set; }
        public string cliente_RazaoSocial { get; set; }
        public string cliente_NomeFantasia { get; set; }
        public string cliente_InscricaoEstadual { get; set; }
        public string cliente_Endereco_Nome { get; set; }
        public string cliente_Endereco_Descricao { get; set; }
        public string cliente_Endereco_CodigoPostal { get; set; }
        public string cliente_Endereco_Logradouro { get; set; }
        public string cliente_Endereco_Numero { get; set; }
        public string cliente_Endereco_Complemento { get; set; }
        public string cliente_Endereco_Bairro { get; set; }
        public string cliente_Endereco_Cidade { get; set; }
        public string cliente_Endereco_UF { get; set; }
        public string cliente_Endereco_Referencia { get; set; }
        public string cliente_Endereco_IBGE { get; set; }
        public string cliente_Endereco_Telefone1 { get; set; }
        public string cliente_Endereco_Telefone2 { get; set; }
        public ICollection<Informacoes_AdicionaisL> cliente_InformacoesAdicionais { get; set; }
        public string entrega_Nome { get; set; }
        public string entrega_Servico { get; set; }
        public string entrega_CodigoPostal { get; set; }
        public string entrega_Logradouro { get; set; }
        public string entrega_Numero { get; set; }
        public string entrega_Bairro { get; set; }
        public string entrega_Cidade { get; set; }
        public string entrega_UF { get; set; }
        public string entrega_Complemento { get; set; }
        public string entrega_Telefone { get; set; }
        public string entrega_IBGE { get; set; }
        public string entrega_CodigoRastreio { get; set; }
        public string entrega_LinkRastreio { get; set; }
        public string entrega_DataEntregaPrevista { get; set; }
        public string entrega_Agendamento_Data { get; set; }
        public string entrega_Agendamento_Periodo { get; set; }
        public string entrega_Agendamento_Valor { get; set; }
        public string pagamento_Tipo { get; set; }
        public string pagamento_Nome { get; set; }
        public string pagamento_Servico { get; set; }
        public string pagamento_Descricao { get; set; }
        public string pagamento_NumeroParcelas { get; set; }
        public string pagamento_ValorParcela { get; set; }
        public string transacao { get; set; }
        public ICollection<Pedido_ItemL> itens { get; set; }
    }

    public class Pedido_ItemL
    {
        public string id { get; set; }
        public string pedidoid { get; set; }
        public string sku { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string gtin { get; set; }
        public string quantidade { get; set; }
        public string precoUnitario { get; set; }
        public string desconto { get; set; }
        public string precoTotal { get; set; }
    }

    public class Informacoes_AdicionaisL
    {
        public string nome { get; set; }
        public string valor { get; set; }
    }

    public class CategoriaL
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public int categoriaPaiId { get; set; }
        public bool ativo { get; set; }
        public ICollection<int> filhos { get; set; }
    }

    public class ProdutoCategoriaL
    {
        public int categoriaId { get; set; }
        public string categoriaNome { get; set; }
    }

    public class GradeL
    {
        public int gradeId { get; set; }
        public string gradeNome { get; set; }
    }

    public class VariacaoL
    {
        public string ativo { get; set; }
        public string sku { get; set; }
        public string estoque { get; set; }
        public string gtin { get; set; }
        public string mpn { get; set; }
        public string ncm { get; set; }
        public string precoVenda { get; set; }
        public string precoCusto { get; set; }
        public string precoPromocional { get; set; }
        public ICollection<int> gradeValores { get; set; }

    }

    public class ImagemL
    {
        public string imagemId { get; set; }
        public string urlImagemMini { get; set; }
        public string urlImagemPequena { get; set; }
        public string urlImagemMedia { get; set; }
        public string urlImagemGrande { get; set; }
        public string principal { get; set; }
        public string ordem { get; set; }
    }

    public class ProdutoBaseL
    {
        public ProdutoBaseL()
        {
            this.variacoes = new List<VariacaoL>();
            this.imagens = new List<ImagemL>();
        }

        public string id { get; set; }
        public string ativo { get; set; }
        public string possuiVariacao { get; set; }
        public string nome { get; set; }
        public string slug { get; set; }
        public string complemento { get; set; }
        public string descricaoGeral { get; set; }
        public string descricaoTecnica { get; set; }
        public string descricaoAplicacao { get; set; }
        public string marca { get; set; }
        public string peso { get; set; }
        public string altura { get; set; }
        public string largura { get; set; }
        public string profundidade { get; set; }
        public string seoTitulo { get; set; }
        public string seoDescricao { get; set; }
        public string seoPalavrasChaves { get; set; }
        public string categoriaId { get; set; }
        public ICollection<VariacaoL> variacoes { get; set; }
        public ICollection<ImagemL> imagens { get; set; }

    }

    public class ProdutoL : ProdutoBaseL
    {
        public ProdutoL()
        {
            this.categorias = new List<ProdutoCategoriaL>();
            this.grades = new List<GradeL>();
        }

        public ICollection<ProdutoCategoriaL> categorias { get; set; }
        public ICollection<GradeL> grades { get; set; }

    }

    public class ProdutoAddL : ProdutoBaseL
    {
        public ProdutoAddL()
        {
            this.categorias = new List<int>();
            this.grades = new List<int>();
        }

        public ICollection<int> categorias { get; set; }
        public ICollection<int> grades { get; set; }

    }

    public class TrackingL
    {
        public string pedidoStatus { get; set; }
        public string notaFiscal { get; set; }
        public string codigoRastreio { get; set; }
        public string linkRastreio { get; set; }
    }

    public class RetornoL
    {
        public string sucesso { get; set; }
        public ICollection<string> erros { get; set; }
        public ICollection<string> avisos { get; set; }
    }

    public class EstoqueL
    {
        public EstoqueL()
        {
            this.estoques = new List<EstoqueItemL>();
        }
        public ICollection<EstoqueItemL> estoques { get; set; }
    }

    public class EstoqueItemL
    {
        public EstoqueItemL()
        {
            sku = "";
            quantidade = 0;
        }
        public EstoqueItemL(string _sku, int _quantidade)
        {
            sku = _sku;
            quantidade = _quantidade;
        }
        public string sku { get; set; }
        public int quantidade { get; set; }
    }

    public class PrecoL
    {
        public PrecoL()
        {
            this.precos = new List<PrecoItemL>();
        }
        public ICollection<PrecoItemL> precos { get; set; }
    }

    public class PrecoItemL
    {
        public PrecoItemL()
        {
            sku = "";
            precoVenda = 0.0m;
            precoCusto = 0.0m;
            precoPromocional = 0.0m;
        }
        public PrecoItemL(
            string _sku,
            decimal _precoVenda,
            decimal _precoCusto,
            decimal _precoPromocional)
        {
            sku = _sku;
            precoVenda = _precoVenda;
            precoCusto = _precoCusto;
            precoPromocional = _precoPromocional;
        }
        public string sku { get; set; }
        public decimal precoVenda { get; set; }
        public decimal precoCusto { get; set; }
        public decimal precoPromocional { get; set; }
    }
}
