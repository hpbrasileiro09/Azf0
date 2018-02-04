using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FunctionApp1
{ 

    public class Produto
    {
        public Guid id { get; set; }
        public string idTotvs { get; set; }
        public string nome { get; set; }
        public string categoriaId { get; set; }
        public string subcategoriaId { get; set; }
        public int ativo { get; set; }
        public string marcaId { get; set; }
        public string marca { get; set; }
        public decimal peso { get; set; }
        public decimal altura { get; set; }
        public decimal largura { get; set; }
        public decimal profundidade { get; set; }
        public int possuiVariacao { get; set; }
        public string gradeId { get; set; }
        public string grade { get; set; }
        public decimal compraCusto { get; set; }
        public decimal compraIcms { get; set; }
        public decimal compraIpi { get; set; }
        public decimal compraSt { get; set; }
        public decimal compraPreco { get; set; }
        public decimal precoSugerido { get; set; }
        public decimal precoCustoFinanceiro { get; set; }
        public decimal precoOutrosCustos { get; set; }
        public decimal precoAtaFrete { get; set; }
        public decimal precoAtaComissao { get; set; }
        public decimal precoAtaMarkup { get; set; }
        public decimal precoVarFrete { get; set; }
        public decimal precoVarComissao { get; set; }
        public decimal precoVarMarkup { get; set; }
        public decimal minimoAtaMarkup { get; set; }
        public decimal minimoVarMarkup { get; set; }
        public int bloqueado { get; set; }
        public int grupoId { get; set; }
        public decimal precoVenda { get; set; }
        public decimal precoCompra { get; set; }
        public decimal precoPromocional { get; set; }
        public int estoque { get; set; }
        public int lastUpdaterId { get; set; }

        //datas
        public DateTime dataCriacao { get; set; }
    }

}
