using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FunctionApp1.Common;
using FunctionApp1.Hub;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FunctionApp1
{
    public static class Function5
    {
        [FunctionName("Function5")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            int _icont = 0;

            log.Info("GetApiData-Starts");

            List<string> _condPgto = new List<string>();

            DateTime _ini;
            DateTime _fim;

            _ini = Help.ChangeTime(DateTime.Now, 0, 0, 0, 0);
            _fim = Help.ChangeTime(DateTime.Now, 23, 59, 59, 0);

            _ini = _ini.AddDays(Constants.LEAN_DATE_RANGE);

            HubLean.GetPedidos(new Parametro
            {
                pagina = Constants.LEAN_START_PAGE,
                totalRegistrosPorPagina = Constants.LEAN_REGS_PER_PAGE,
                dataInicio = _ini.ToString("yyyy-MM-dd THH:mm:ss.000Z"),
                dataFim = _fim.ToString("yyyy-MM-dd THH:mm:ss.000Z"),
                pedidoStatus = ""
            }).Wait();

            if (HubLean.GetMessage().Length != 0)
                log.Error(String.Format("HubLean.GetPedidos.Exception [{0}]", HubLean.GetMessage()));

            foreach (PedidoL p in HubLean._pedidos)
            {

                //if (p.id.Equals("79") == false) continue;

                // _log.Info(String.Format("UPDATE [dbo].[Pedido] SET numeroPedidoAplicacaoId = {0} WHERE numeroPedidoAplicacao = '{1}';", p.id, p.numero));

                log.Info(String.Format("Id [{0}]", p.id));
                log.Info(String.Format("Numero [{0}]", p.numero));
                log.Info(String.Format("Documento [{0}]", p.cliente_Documento));
                log.Info(String.Format("Ibge [{0}]", p.cliente_Endereco_IBGE));
                log.Info(String.Format("Pagamento.Nome [{0}]", p.pagamento_Nome));
                log.Info(String.Format("Pagamento.Parcelas [{0}]", p.pagamento_NumeroParcelas));

                foreach (Informacoes_AdicionaisL ia in p.cliente_InformacoesAdicionais)
                {
                    if (ia.nome.Equals("Especialidade"))
                    {
                        log.Info(String.Format("Especialidade [{0}]", ia.valor));
                    }
                    if (ia.nome.Equals("Profissão"))
                    {
                        log.Info(String.Format("Profissao [{0}]", ia.valor));
                    }

                    if (ia.nome.Equals("CRO"))
                    {
                        log.Info(String.Format("CRO [{0}]", ia.valor));
                    }
                    if (ia.nome.Equals("CRO Estado"))
                    {
                        log.Info(String.Format("CRO Estado [{0}]", ia.valor));
                    }

                    if (ia.nome.Equals("Tipo"))
                    {
                        log.Info(String.Format("Tipo [{0}]", ia.valor));
                    }
                }

                _condPgto.Add(p.pagamento_Nome);
                foreach (Pedido_ItemL pi in p.itens)
                {
                    log.Info(String.Format("\tSku [{0}", pi.sku));
                    log.Info(String.Format("\tQuantidade [{0}]", pi.quantidade));
                    log.Info(String.Format("\tUnitario [{0}]", pi.precoUnitario));
                    log.Info(String.Format("\tTotal [{0}]", pi.precoTotal));
                }

                //log.Info(HubLean.GetJsonPedido(p));
                //Console.WriteLine(HubLean.GetJsonPedido(p));

                _icont++;
            }

            foreach (var _item in _condPgto)
            {
                log.Info(String.Format("condPgto [{0}]", _item));
            }

            DefParametro(log, _ini, _fim, _icont);

            log.Info("GetApiData-End");

            return req.CreateResponse(HttpStatusCode.OK, "Function5 ... ");
        }

        public static void DefParametro(TraceWriter log, DateTime _ini, DateTime _fim, int _icont)
        {
            JObject jsonObject = new JObject(
                new JProperty("start_date", _ini.ToString("yyyy-MM-dd THH:mm:ss.000Z")),
                new JProperty("end_date", _fim.ToString("yyyy-MM-dd THH:mm:ss.000Z")),
                new JProperty("start_page", Constants.LEAN_START_PAGE),
                new JProperty("regs_per_page", Constants.LEAN_REGS_PER_PAGE),
                new JProperty("date_range", Constants.LEAN_DATE_RANGE),
                new JProperty("orders_processes", _icont)
            );
            log.Info(JsonConvert.SerializeObject(jsonObject));
        }

    }
}
