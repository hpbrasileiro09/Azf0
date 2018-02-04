using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1.Hub
{
    public class HubCS
    {
        private static bool _loaded = false;

        private static string _message = "";

        private static string _description = "";

        public static PedidosC _pedidos;

        public static ProdutosC _produtos;

        public static EstoqueC _estoque;

        public static HttpClient _client;

        public static string _base_path = "http://52.170.19.41:8050";

        public static List<PaymentType> condicaoPagamento;

        public static string GetMessage()
        {
            return _message;
        }

        public static void LoadClient()
        {
            if (_loaded == false)
            {

                _client = new HttpClient();
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add("Accept", "application/json");
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                condicaoPagamento = new List<PaymentType>();
                condicaoPagamento.Add(new PaymentType("001", "A VISTA", "DEP", 1));

                condicaoPagamento.Add(new PaymentType("002", "BOL PRAZO 1X", "BOL", 1));
                condicaoPagamento.Add(new PaymentType("003", "BOLETO 2X", "BOL", 2));
                condicaoPagamento.Add(new PaymentType("004", "BOLETO 3X", "BOL", 3));
                condicaoPagamento.Add(new PaymentType("005", "BOLETO 4X", "BOL", 4));
                condicaoPagamento.Add(new PaymentType("006", "BOLETO 5X", "BOL", 5));
                condicaoPagamento.Add(new PaymentType("007", "BOLETO 6X", "BOL", 6));

                condicaoPagamento.Add(new PaymentType("008", "C CRED PRAZO 1X", "CC ", 1));
                condicaoPagamento.Add(new PaymentType("009", "CART CREDITO 2X", "CC ", 2));
                condicaoPagamento.Add(new PaymentType("010", "CART CREDITO 3X", "CC ", 3));
                condicaoPagamento.Add(new PaymentType("011", "CART CREDITO 4X", "CC ", 4));
                condicaoPagamento.Add(new PaymentType("012", "CART CREDITO 5X", "CC ", 5));
                condicaoPagamento.Add(new PaymentType("013", "CART CREDITO 6X", "CC ", 6));

                condicaoPagamento.Add(new PaymentType("008", "C CRED PRAZO 1X", "CC", 1));
                condicaoPagamento.Add(new PaymentType("009", "CART CREDITO 2X", "CC", 2));
                condicaoPagamento.Add(new PaymentType("010", "CART CREDITO 3X", "CC", 3));
                condicaoPagamento.Add(new PaymentType("011", "CART CREDITO 4X", "CC", 4));
                condicaoPagamento.Add(new PaymentType("012", "CART CREDITO 5X", "CC", 5));
                condicaoPagamento.Add(new PaymentType("013", "CART CREDITO 6X", "CC", 6));

                _loaded = true;
            }
        }

        public static string GetJsonEstoque(EstoqueItemC _item)
        {
            return JsonConvert.SerializeObject(_item);
        }

        public static string GetJsonEstoque()
        {
            return JsonConvert.SerializeObject(_estoque);
        }

        public static string GetJsonPedido(PedidoC _pedido)
        {
            return JsonConvert.SerializeObject(_pedido);
        }

        public static string GetJsonPedidos()
        {
            return JsonConvert.SerializeObject(_pedidos);
        }

        public static string GetJsonProduto(ProdutoC _produto)
        {
            return JsonConvert.SerializeObject(_produto);
        }

        public static string GetJsonProdutos(ProdutosC _produtos)
        {
            return JsonConvert.SerializeObject(_produtos);
        }

        public static string GetJsonProdutos()
        {
            return JsonConvert.SerializeObject(_produtos);
        }

        public static string GetCondicaoPagamento(string _source = "DEP", int _times = 1)
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

        public static async Task<bool> GetPedidos()
        {
            LoadClient();

            _message = "";

            try
            {
                string uri = _base_path + "/api/pedido";
                HttpResponseMessage response = await _client.GetAsync(uri);

                var property = response.GetType().GetProperty("StatusCode");
                if ((HttpStatusCode)property.GetValue(response) == HttpStatusCode.OK)
                {
                    response.EnsureSuccessStatusCode();
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(PedidosC));
                    _pedidos = (PedidosC)jsonSerializer.ReadObject(await response.Content.ReadAsStreamAsync());
                    return true;
                }
            }
            catch (Exception e)
            {
                _message = e.Message;
            }
            return false;
        }

        public static async Task<bool> GetProdutos(string _produtoId = "")
        {
            LoadClient();

            _message = "";

            try
            {
                string uri = _base_path + "/api/produto" + (_produtoId.Length > 0 ? "/" : "") + _produtoId;
                HttpResponseMessage response = await _client.GetAsync(uri);

                var property = response.GetType().GetProperty("StatusCode");
                if ((HttpStatusCode)property.GetValue(response) == HttpStatusCode.OK)
                {
                    response.EnsureSuccessStatusCode();
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(ProdutosC));
                    _produtos = (ProdutosC)jsonSerializer.ReadObject(await response.Content.ReadAsStreamAsync());
                    return true;
                }
            }
            catch (Exception e)
            {
                _message = e.Message;
            }
            return false;
        }

        public static async Task<bool> GetEstoque(string _produtoId = "")
        {
            LoadClient();

            _message = "";

            try
            {
                string uri = _base_path + "/api/estoque" + (_produtoId.Length > 0 ? "/" : "") + _produtoId;
                HttpResponseMessage response = await _client.GetAsync(uri);

                var property = response.GetType().GetProperty("StatusCode");
                if ((HttpStatusCode)property.GetValue(response) == HttpStatusCode.OK)
                {
                    response.EnsureSuccessStatusCode();
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(EstoqueC));
                    _estoque = (EstoqueC)jsonSerializer.ReadObject(await response.Content.ReadAsStreamAsync());
                    return true;
                }
            }
            catch (Exception e)
            {
                _message = e.Message;
            }
            return false;
        }

        public static async Task<bool> UpdEstoque(EstoqueC _estoque)
        {
            LoadClient();

            _message = "";

            try
            {
                string uri = _base_path + "/api/estoque";
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

        public static async Task<bool> SetTracking(StatusC _status)
        {
            LoadClient();

            _message = "";

            try
            {
                string uri = _base_path + "/api/pedido/status";
                var json = JsonConvert.SerializeObject(_status);
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

        public static async Task<bool> AddProduto(ProdutosC _produtos)
        {
            LoadClient();

            _message = "";

            try
            {
                string uri = _base_path + "/api/produto";
                var json = JsonConvert.SerializeObject(_produtos);
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
