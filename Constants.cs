
namespace FunctionApp1
{

    static class Constants
    {
        public const int STOCK_RESET = 1;

        public const string DEV01 = "hub01";
        public const string DEV01_HOST = "insity01.database.windows.net";
        public const string DEV01_LOGIN = "insity";
        public const string DEV01_PASSWD = "hub#141215";

        public const string DEV02 = "insityportaldev03";
        public const string DEV02_HOST = "52.170.19.41";
        public const string DEV02_LOGIN = "db";
        public const string DEV02_PASSWD = "insity#db";

        public const int LEAN_DATE_RANGE = -7;
        public const int LEAN_START_PAGE = 0;
        public const int LEAN_REGS_PER_PAGE = 25;
        public const string TOTVS_FTP_HOST = "ftp://66.147.238.79";
        public const string TOTVS_FTP_LGN = "portal";
        public const string TOVS_FTP_PWD = "SH6651@!nR";
        public const string TOTVS_SELLER = "009";
        public const int TOTVS_SELLER_LEN = 3;
    }

    static class LeanOrderStatus
    {
        public const string REGISTRADO = "REGISTRADO"; // Pedidos registrados e pendentes de aprovação (pagamento).
        public const string APROVADO = "APROVADO"; // Pedidos com pagamento aprovado.
        public const string SEPARACAO = "SEPARACAO"; // Pedidos em separação para entrega.
        public const string TRANSPORTE = "TRANSPORTE"; // Pedidos em transporte.
        public const string ENTREGUE = "ENTREGUE"; // Pedidos entregues.
        public const string CANCELADO = "CANCELADO"; // Pedidos cencelados.
    }

    static class OrderStatus
    {
        public const int INTEGRADO_A_APLICACAO = 0;
        public const int ARQUIVO_INTEGRACAO_GERADO = 1;
        public const int INTEGRADO = 2;
        public const int NAO_INTEGRADO = 3;
        public const int RESEND = 4;
        public const int PAGAMENTO_EM_ANALISE = 5;
        public const int PAGAMENTO_APROVADO = 6;
        public const int SEPARACAO = 7;
        public const int FATURAMENTO = 8;
    }

    static class HubOrderStatus
    {
        public const int INTEGRADO_A_APP = 0; // Integrado a Aplicação
        public const int ARQ_GERADO = 1; // Arquivo Integração Gerado
        public const int INTEGRADO = 2; // Integrado
        public const int NAO_INTEGRADO = 3; // Arquivo Não Integrado
        public const int REENVIO = 4; // Reenvio Arquivo Integração
        public const int EM_ANALISE = 5; // Pedido em Análise
        public const int APROVADO = 6; // Pedido Aprovado
        public const int SEPARACAO = 7; // Pedido em Separação
        public const int FATURADO = 8; // Pedido Faturado
        public const int TRANSPORTE = 9; // Pedido em Transporte
        public const int ENTREGUE = 10; // Pedido Entregue
        public const int CANCELADO = 11; // Pedido Cancelado
    }

    static class TotvsOrderStatus
    {
        public const string IMPORTADO = "00"; // Pedido Importado 
        public const string EM_ANALISE = "01"; // Pedido em Análise 
        public const string APROVADO = "02"; // Pedido Aprovado 
        public const string SEPARACAO = "03"; // Pedido em Separação 
        public const string FATURADO = "04"; // Pedido Faturado - NF 
        public const string TRANSPORTE = "05"; // Pedido em Transporte 
        public const string ENTREGUE = "06"; // Pedido Entregue 
        public const string CANCELADO = "07"; // Pedido Cancelado 
    }

    static class Updater
    {
        public const int PW = 0;
        public const string PW_DS = "PW";
        public const int CS = 1;
        public const string CS_DS = "CS";
    }

    static class TotvsStatus
    {
        public const string SUCESSO = "00";  // Pedido importado com Sucesso | Será informado o Numero do Pedido do Protheus
        public const string FALTA_CNPJ = "01"; // CPF / CNPJ não Informado        
        public const string FALTA_RAZAO_SOCIAL = "02"; // Razão Social não Informada        
        public const string FALTA_NOME_FANTASIA = "03"; // Nome Fanatasia não Informado        
        public const string FALTA_ENDERECO = "04"; // Endereço não Informado | Mesmo errro para todos os campos do Endereço
        public const string COD_MUNICIPIO_INVALIDO = "05"; // Código do Municipio Inválido | Tanto para Endereço do Cliente quanto para endereço de Entrega
        public const string FALTA_ENDERECO_ENTREGA = "06"; // Endereço Entrega não Informado | Mesmo errro para todos os campos do Endereço
        public const string PAGAMENTO_INVALIDO = "07"; // Tipo de Pagamenbto Inválido       
        public const string PRODUTO_INVALIDO = "08"; // Produto Inválido        
        public const string QUANTIDADE_ZERADA = "09"; // Quantidade Zerado       
        public const string UNITARIO_INVALIDO = "10"; // Valor Unitário Inválido       
        public const string DESCONTO_INVALIDO = "11"; // Valor do Desconto Inválido | Caso o desconto seja Maior ou Igual ao Valor total do Item
        public const string TOTAL_INVALIDO = "12"; // Total do Pedido Inválido        
        public const string PEDIDO_JAH_IMPORTADO = "99"; // Pedido Já Importado no Protheus | Será informado o Numero do Pedido do Protheus e Data da Emissão
    }

    static class TotvsPaymentTypes
    {
        public const string A_VISTA = "001"; // A VISTA
        public const string BOL_PRAZO_1X = "002"; // BOL PRAZO 1X
        public const string BOLETO_2X = "003"; // BOLETO 2X
        public const string BOLETO_3X = "004"; // BOLETO 3X
        public const string BOLETO_4X = "005"; // BOLETO 4X
        public const string BOLETO_5X = "006"; // BOLETO 5X
        public const string BOLETO_6X = "007"; // BOLETO 6X
        public const string C_CRED_PRAZO_1X = "008"; // C CRED PRAZO 1X
        public const string CART_CREDITO_2X = "009"; // CART CREDITO 2X
        public const string CART_CREDITO_3X = "010"; // CART CREDITO 3X
        public const string CART_CREDITO_4X = "011"; // CART CREDITO 4X
        public const string CART_CREDITO_5X = "012"; // CART CREDITO 5X
        public const string CART_CREDITO_6X = "013"; // CART CREDITO 6X
    }

}