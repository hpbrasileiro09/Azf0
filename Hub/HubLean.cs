using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace FunctionApp1.Hub
{

    public class HubLean
    {
        private static bool _loaded = false;

        private static string _message = "";

        private static string _description = "";

        public static ProdutoL _produto;

        public static RetornoL _retorno;

        public static ProdutoL[] _produtos;

        public static PedidoL _pedido;

        public static PedidoL[] _pedidos;

        public static HttpClient _client;

        public static string _base_path = "http://portalcomm-api.azurewebsites.net";

        public static List<PaymentType> condicaoPagamento;

        public static List<StatusHub> statusHub;

        public static string GetMessage()
        {
            return _message;
        }

        public static string GetDescription()
        {
            return _description;
        }

        public static void LoadClient()
        {

            _message = "";
            _description = "";

            if (_loaded == false)
            {

                _client = new HttpClient();
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add("Accept", "application/json");
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                statusHub = new List<StatusHub>();
                statusHub.Add(new StatusHub(0, "Integrado a Aplicação", "REGISTRADO", "", "0A", ""));
                statusHub.Add(new StatusHub(1, "Arquivo Integração Gerado", "REGISTRADO", "", "0B", ""));
                statusHub.Add(new StatusHub(2, "Integrado Totvs", "REGISTRADO", "", "00", "Pedido Importado"));
                statusHub.Add(new StatusHub(3, "Arquivo Não Integrado", "REGISTRADO", "", "0D", ""));
                statusHub.Add(new StatusHub(4, "Reenvio Arquivo Integração", "REGISTRADO", "", "0E", ""));
                statusHub.Add(new StatusHub(5, "Pedido em Análise", "REGISTRADO", "", "01", "Pedido em Análise"));
                statusHub.Add(new StatusHub(6, "Pedido Aprovado", "APROVADO", "", "02", "Pedido Aprovado"));
                statusHub.Add(new StatusHub(7, "Pedido em Separação", "SEPARACAO", "", "03", "Pedido em Separação"));
                statusHub.Add(new StatusHub(8, "Pedido Faturado", "", "", "04", "Pedido Faturado - NF"));
                statusHub.Add(new StatusHub(9, "Pedido em Transporte", "TRANSPORTE", "", "05", "Pedido em Transporte"));
                statusHub.Add(new StatusHub(10, "Pedido Entregue", "ENTREGUE", "", "06", "Pedido Entregue"));
                statusHub.Add(new StatusHub(11, "Pedido Cancelado", "CANCELADO", "", "07", "Pedido Cancelado"));

                condicaoPagamento = new List<PaymentType>();
                condicaoPagamento.Add(new PaymentType("001", "A VISTA", "DEPOSITO BANCARIO", 1));

                condicaoPagamento.Add(new PaymentType("002", "BOL PRAZO 1X", "BOLETO MUNDIPAGG", 1));
                condicaoPagamento.Add(new PaymentType("003", "BOLETO 2X", "BOLETO MUNDIPAGG", 2));
                condicaoPagamento.Add(new PaymentType("004", "BOLETO 3X", "BOLETO MUNDIPAGG", 3));
                condicaoPagamento.Add(new PaymentType("005", "BOLETO 4X", "BOLETO MUNDIPAGG", 4));
                condicaoPagamento.Add(new PaymentType("006", "BOLETO 5X", "BOLETO MUNDIPAGG", 5));
                condicaoPagamento.Add(new PaymentType("007", "BOLETO 6X", "BOLETO MUNDIPAGG", 6));

                condicaoPagamento.Add(new PaymentType("002", "BOL PRAZO 1X", "BOLETO PARCELADO", 1));
                condicaoPagamento.Add(new PaymentType("003", "BOLETO 2X", "BOLETO PARCELADO", 2));
                condicaoPagamento.Add(new PaymentType("004", "BOLETO 3X", "BOLETO PARCELADO", 3));
                condicaoPagamento.Add(new PaymentType("005", "BOLETO 4X", "BOLETO PARCELADO", 4));
                condicaoPagamento.Add(new PaymentType("006", "BOLETO 5X", "BOLETO PARCELADO", 5));
                condicaoPagamento.Add(new PaymentType("007", "BOLETO 6X", "BOLETO PARCELADO", 6));

                condicaoPagamento.Add(new PaymentType("008", "C CRED PRAZO 1X", "CARTAO CREDITO MUNDIPAGG", 1));
                condicaoPagamento.Add(new PaymentType("009", "CART CREDITO 2X", "CARTAO CREDITO MUNDIPAGG", 2));
                condicaoPagamento.Add(new PaymentType("010", "CART CREDITO 3X", "CARTAO CREDITO MUNDIPAGG", 3));
                condicaoPagamento.Add(new PaymentType("011", "CART CREDITO 4X", "CARTAO CREDITO MUNDIPAGG", 4));
                condicaoPagamento.Add(new PaymentType("012", "CART CREDITO 5X", "CARTAO CREDITO MUNDIPAGG", 5));
                condicaoPagamento.Add(new PaymentType("013", "CART CREDITO 6X", "CARTAO CREDITO MUNDIPAGG", 6));

                condicaoPagamento.Add(new PaymentType("008", "C CRED PRAZO 1X", "CARTAO CREDITO PAGARME", 1));
                condicaoPagamento.Add(new PaymentType("009", "CART CREDITO 2X", "CARTAO CREDITO PAGARME", 2));
                condicaoPagamento.Add(new PaymentType("010", "CART CREDITO 3X", "CARTAO CREDITO PAGARME", 3));
                condicaoPagamento.Add(new PaymentType("011", "CART CREDITO 4X", "CARTAO CREDITO PAGARME", 4));
                condicaoPagamento.Add(new PaymentType("012", "CART CREDITO 5X", "CARTAO CREDITO PAGARME", 5));
                condicaoPagamento.Add(new PaymentType("013", "CART CREDITO 6X", "CARTAO CREDITO PAGARME", 6));

                _loaded = true;
            }
        }

        public static async Task<bool> GetPedido(int _id = 0)
        {
            LoadClient();

            try
            {
                string uri = _base_path + "/api/v1/pedidos/" + _id.ToString();
                HttpResponseMessage response = await _client.GetAsync(uri);

                var property = response.GetType().GetProperty("StatusCode");
                if ((HttpStatusCode)property.GetValue(response) == HttpStatusCode.OK)
                {
                    response.EnsureSuccessStatusCode();
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(PedidoL));
                    _pedido = (PedidoL)jsonSerializer.ReadObject(await response.Content.ReadAsStreamAsync());
                    return true;
                }
            }
            catch (Exception e)
            {
                _message = e.Message;
            }
            return false;
        }

        public static string GetJsonPedido(PedidoL _item)
        {
            return JsonConvert.SerializeObject(_item);
        }

        public static string GetJsonEstoque(EstoqueL _item)
        {
            return JsonConvert.SerializeObject(_item);
        }

        public static string GetJsonPrecos(PrecoL _item)
        {
            return JsonConvert.SerializeObject(_item);
        }

        public static string GetJsonRetorno()
        {
            return JsonConvert.SerializeObject(_retorno);
        }

        public static async Task<bool> GetPedidos(Parametro _param)
        {
            LoadClient();

            try
            {
                string uri = _base_path + "/api/v1/pedidos";
                var json = JsonConvert.SerializeObject(_param);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(new Uri(uri), stringContent);

                var property = response.GetType().GetProperty("StatusCode");
                if ((HttpStatusCode)property.GetValue(response) == HttpStatusCode.OK)
                {
                    response.EnsureSuccessStatusCode();
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(PedidoL[]));
                    _pedidos = (PedidoL[])jsonSerializer.ReadObject(await response.Content.ReadAsStreamAsync());
                    return true;
                }
            }
            catch (Exception e)
            {
                _message = e.Message;
            }
            return false;
        }

        public static async Task<bool> AddProduto(ProdutoAddL _produto)
        {
            LoadClient();

            _retorno = new RetornoL();

            try
            {
                string uri = _base_path + "/api/v1/catalogo/produtos";
                var json = JsonConvert.SerializeObject(_produto);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(new Uri(uri), stringContent);

                var property = response.GetType().GetProperty("StatusCode");

                if ((HttpStatusCode)property.GetValue(response) == HttpStatusCode.OK)
                {
                    response.EnsureSuccessStatusCode();
                    _message = "HttpStatus.200";
                    return true;
                }

                if ((HttpStatusCode)property.GetValue(response) == HttpStatusCode.BadRequest)
                {
                    var _json = new DataContractJsonSerializer(typeof(RetornoL));
                    _retorno = (RetornoL)_json.ReadObject(await response.Content.ReadAsStreamAsync());
                    _message = "HttpStatus.400";
                    return false;
                }

                if ((HttpStatusCode)property.GetValue(response) == HttpStatusCode.InternalServerError)
                {
                    _message = "HttpStatus.500";
                    return false;
                }

            }
            catch (Exception e)
            {
                _message = e.Message;
            }
            return false;
        }

        public static async Task<bool> UpdEstoque(EstoqueL _estoque)
        {
            LoadClient();

            try
            {
                string uri = _base_path + "/api/v1/estoque";
                var json = JsonConvert.SerializeObject(_estoque);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(new Uri(uri), stringContent);

                var property = response.GetType().GetProperty("StatusCode");

                if ((HttpStatusCode)property.GetValue(response) == HttpStatusCode.OK)
                {
                    response.EnsureSuccessStatusCode();
                    _message = "HttpStatus.200";
                    return true;
                }

                if ((HttpStatusCode)property.GetValue(response) == HttpStatusCode.BadRequest)
                {
                    _message = "HttpStatus.400";
                    return false;
                }

                if ((HttpStatusCode)property.GetValue(response) == HttpStatusCode.InternalServerError)
                {
                    _message = "HttpStatus.500";
                    return false;
                }

            }
            catch (Exception e)
            {
                _message = e.Message;
            }
            return false;
        }

        public static async Task<bool> UpdPreco(PrecoL _preco)
        {
            LoadClient();

            try
            {
                string uri = _base_path + "/api/v1/precos";
                var json = JsonConvert.SerializeObject(_preco);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(new Uri(uri), stringContent);

                var property = response.GetType().GetProperty("StatusCode");

                if ((HttpStatusCode)property.GetValue(response) == HttpStatusCode.OK)
                {
                    response.EnsureSuccessStatusCode();
                    _message = "HttpStatus.200";
                    return true;
                }

                if ((HttpStatusCode)property.GetValue(response) == HttpStatusCode.BadRequest)
                {
                    _message = "HttpStatus.400";
                    return false;
                }

                if ((HttpStatusCode)property.GetValue(response) == HttpStatusCode.InternalServerError)
                {
                    _message = "HttpStatus.500";
                    return false;
                }

            }
            catch (Exception e)
            {
                _message = e.Message;
            }
            return false;
        }

        public static async Task<bool> GetProduto(int _id = 0)
        {
            LoadClient();

            try
            {
                string uri = _base_path + "/api/v1/catalogo/produtos/" + _id.ToString();
                HttpResponseMessage response = await _client.GetAsync(uri);

                var property = response.GetType().GetProperty("StatusCode");
                if ((HttpStatusCode)property.GetValue(response) == HttpStatusCode.OK)
                {
                    response.EnsureSuccessStatusCode();
                    var json = new DataContractJsonSerializer(typeof(ProdutoL));
                    _produto = (ProdutoL)json.ReadObject(await response.Content.ReadAsStreamAsync());
                    return true;
                }
            }
            catch (Exception e)
            {
                _message = e.Message;
            }
            return false;
        }

        public static string GetJsonProduto(ProdutoL _item)
        {
            return JsonConvert.SerializeObject(_item);
        }

        public static string GetJsonProdutoAdd(ProdutoAddL _item)
        {
            return JsonConvert.SerializeObject(_item);
        }

        public static async Task<bool> GetProdutos()
        {
            LoadClient();

            try
            {
                string uri = _base_path + "/api/v1/catalogo/produtos?Page=1&PageSize=25";
                HttpResponseMessage response = await _client.GetAsync(uri);

                var property = response.GetType().GetProperty("StatusCode");
                if ((HttpStatusCode)property.GetValue(response) == HttpStatusCode.OK)
                {
                    response.EnsureSuccessStatusCode();
                    var json = new DataContractJsonSerializer(typeof(ProdutoL[]));
                    _produtos = (ProdutoL[])json.ReadObject(await response.Content.ReadAsStreamAsync());
                    return true;
                }
            }
            catch (Exception e)
            {
                _message = e.Message;
            }
            return false;
        }

        public static string GetSequence(SMContext db, int _nextVal = 0)
        {
            var nextVal = "0";
            if (_nextVal != 0)
            {
                nextVal = _nextVal.ToString();
            }
            else
            {
                using (var command = db.Database.Connection.CreateCommand())
                {
                    command.CommandText = "SELECT NEXT VALUE FOR numeroPedido";
                    db.Database.Connection.Open();
                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            nextVal = result[0].ToString();
                        }
                    }
                }
            }
            return nextVal;
        }

        public static string GetNumeroPedido(string _numeroPedido, string _abrev = "", int _tamanho = 6)
        {
            var _resp = "";
            var _caracter = '0';
            if (_numeroPedido.Length < _tamanho)
            {
                string tabs = new String(_caracter, _tamanho - _numeroPedido.Length);
                _resp = _abrev + tabs.ToString() + _numeroPedido;
            }
            else
            {
                _resp = _abrev + _numeroPedido.Substring(0, _tamanho);
            }
            return _resp;
        }

        public static string GetCondicaoPagamento(string _source = "DEPOSITO BANCARIO", int _times = 1)
        {
            LoadClient();

            string _target = "";
            try
            {
                PaymentType _find = condicaoPagamento.Find(p => p.source == _source && p.times == _times);
                _target = _find.target;
                _description = _find.description;
            }
            catch (Exception e)
            {
                _message = e.Message;
                _target = "000";
            }
            return _target;
        }

        public static int GetStatusHub(string _source = "00")
        {
            LoadClient();

            int _target = 0;
            try
            {
                StatusHub _find = statusHub.Find(p => p.source == _source);
                _target = _find.target;
                _description = _find.target_ds;
            }
            catch (Exception e)
            {
                _message = e.Message;
                _target = 0;
            }
            return _target;
        }

        public static string GetStatusPw(string _source = "00")
        {
            LoadClient();

            string _target = "";
            try
            {
                StatusHub _find = statusHub.Find(p => p.source == _source);
                _target = _find.target_pw;
                _description = _find.target_ds;
            }
            catch (Exception e)
            {
                _message = e.Message;
                _target = "REGISTRADO";
            }
            return _target;
        }

        public static string GetStatusCs(string _source = "00")
        {
            LoadClient();

            string _target = "";
            try
            {
                StatusHub _find = statusHub.Find(p => p.source == _source);
                _target = _find.target_cs;
                _description = _find.target_ds;
            }
            catch (Exception e)
            {
                _message = e.Message;
                _target = "REGISTRADO";
            }
            return _target;
        }

        public static async Task<bool> SetTracking(int _id, TrackingL _tracking)
        {
            LoadClient();

            _retorno = new RetornoL();

            try
            {
                string uri = _base_path + "/api/v1/pedidos/" + _id.ToString() + "/tracking";
                var json = JsonConvert.SerializeObject(_tracking);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(new Uri(uri), stringContent);

                var property = response.GetType().GetProperty("StatusCode");

                if ((HttpStatusCode)property.GetValue(response) == HttpStatusCode.OK)
                {
                    response.EnsureSuccessStatusCode();
                    _message = "HttpStatus.200";
                    return true;
                }

                if ((HttpStatusCode)property.GetValue(response) == HttpStatusCode.BadRequest)
                {
                    var _json = new DataContractJsonSerializer(typeof(RetornoL));
                    _retorno = (RetornoL)_json.ReadObject(await response.Content.ReadAsStreamAsync());
                    _message = "HttpStatus.400";
                    return false;
                }

                if ((HttpStatusCode)property.GetValue(response) == HttpStatusCode.InternalServerError)
                {
                    _message = "HttpStatus.500";
                    return false;
                }

            }
            catch (Exception e)
            {
                _message = e.Message;
            }
            return false;
        }

    }

}
