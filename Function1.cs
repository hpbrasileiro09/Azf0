using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace FunctionApp1
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            SMContext db = new SMContext();

            int _icont = 0;

            log.Info("GetApiProducts-Starts");

            List<string> _condPgto = new List<string>();

            Hub.HubLean.GetProdutos().Wait();

            if (Hub.HubLean.GetMessage().Length != 0)
                log.Error(String.Format("HubLean.GetProduto.Exception [{0}]", Hub.HubLean.GetMessage()));

            if (Hub.HubLean._produtos != null)
            {
                foreach (var _item in Hub.HubLean._produtos)
                {
                    foreach (var _var in _item.variacoes)
                    {
                        log.Info(new String('-', 30));
                        log.Info(String.Format("Id [{0}]", _item.id));
                        log.Info(String.Format("Nome [{0}]", _item.nome));
                        log.Info(String.Format("Sku [{0}]", _var.sku));
                        log.Info(String.Format("Estoque [{0}]", _var.estoque));
                        log.Info(String.Format("PrecoVenda [{0}]", _var.precoVenda));
                        log.Info(String.Format("PrecoCusto [{0}]", _var.precoCusto));
                        log.Info(String.Format("PrecoPromocional [{0}]", _var.precoPromocional));
                        _icont++;
                    }
                }
            }

            log.Info(new String('-', 30));
            log.Info(String.Format("_icont [{0}]", _icont));

            log.Info("GetApiProducts-End");

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(Hub.HubLean._produtos), Encoding.UTF8, "application/json")
            };

        }

    }
}
