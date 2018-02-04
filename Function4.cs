using System;
using System.Linq;
using FunctionApp1.Hub;
using FunctionApp1.Common;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Threading.Tasks;
using System.Net;

namespace FunctionApp1
{
    public static class Function4
    {
        private static EstoqueL _estoqueHub;

        [FunctionName("Function4")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            //GetOrders(log);

            return req.CreateResponse(HttpStatusCode.OK, "Function5 ... ");
        }

        public static void GetOrders(TraceWriter log)
        {

            int _icont = 0;

            log.Info("GetOrders-Starts");

            DateTime _ini;
            DateTime _fim;

            _ini = Help.ChangeTime(DateTime.Now, 0, 0, 0, 0);
            _fim = Help.ChangeTime(DateTime.Now, 23, 59, 59, 0);

            _ini = _ini.AddDays(Constants.LEAN_DATE_RANGE);

            SMContext db = new SMContext();

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

            try
            {

                foreach (PedidoL p in HubLean._pedidos)
                {

                    log.Info(String.Format("numeroPedidoAplicacao [{0}]", p.numero));

                    if (VerifyOrder(p, log) == false)
                    {
                        log.Error(String.Format("VerifyOrder [{0}] - {1}", p.numero, "Invalid Data From Api Order !!"));
                        continue;
                    }

                    var _pedido = new Pedido();

                    _pedido = db.Pedido.Where(ph => ph.numeroPedidoAplicacao.Equals(p.numero)).FirstOrDefault();

                    if (_pedido != null)
                    {
                        log.Warning(String.Format("Status [{0}]", "Already integrated with Hub!"));
                        continue;
                    }

                    string nextVal = HubLean.GetNumeroPedido(p.numero.ToString(), Updater.PW_DS);

                    log.Info(String.Format("Id [{0}]", p.id));
                    log.Info(String.Format("Nextval [{0}]", nextVal));
                    log.Info(String.Format("Numero [{0}]", p.numero));
                    log.Info(String.Format("Cidade [{0}]", p.cliente_Endereco_Cidade));
                    log.Info(String.Format("Estado [{0}]", p.cliente_Endereco_UF));
                    log.Info(String.Format("Entrega.Cidade [{0}]", p.entrega_Cidade));
                    log.Info(String.Format("Entrega.Estado [{0}]", p.entrega_UF));
                    log.Info(String.Format("Pagamento.Nome [{0}]", p.pagamento_Nome));
                    log.Info(String.Format("Pagamento.NumeroParcelas [{0}]", p.pagamento_NumeroParcelas));

                    _pedido = new Pedido();

                    _pedido.id = Guid.NewGuid();
                    _pedido.numeroPedido = nextVal;
                    _pedido.numeroPedidoAplicacao = p.numero;
                    _pedido.numeroPedidoAplicacaoId = int.Parse(p.id);
                    _pedido.numeroPedidoTotvs = "";
                    _pedido.notaFiscal = "";
                    _pedido.status = 0;
                    _pedido.hubStatus = OrderStatus.INTEGRADO_A_APLICACAO;
                    _pedido.dataCriacao = DateTime.Now;
                    _pedido.valorTotal = 0;
                    _pedido.valorDesconto = 0;
                    _pedido.valorFinal = 0;

                    _pedido.vendedor = Constants.TOTVS_SELLER; /* _DEBUG_ */
                    _pedido.mensagem = ""; /* _DEBUG_ */

                    _pedido.condicaoPagamento = HubLean.GetCondicaoPagamento(p.pagamento_Nome, int.Parse(p.pagamento_NumeroParcelas));

                    _pedido.quantidadeParcelas = int.Parse(p.pagamento_NumeroParcelas);
                    _pedido.integradoTotvs = 0;
                    _pedido.dataIntegracaoTotvs = DateTime.Now;
                    _pedido.dataEnvioTotvs = DateTime.Now;
                    _pedido.retornoTotvs = "00";
                    _pedido.valorTotalFrete = 0;

                    var _cliente = new Cliente();

                    log.Info(String.Format("cliente_Documento [{0}]", p.cliente_Documento));

                    _cliente = db.Cliente.Where(c => c.cpfCnpj.Equals(p.cliente_Documento)).FirstOrDefault();

                    if (_cliente != null)
                    {
                        log.Info("Already-Client");
                        log.Info(String.Format("CpfCnpj [{0}]", _cliente.cpfCnpj));
                        log.Info(String.Format("Nome [{0}]", _cliente.nome));
                        log.Info(String.Format("Cidade [{0}]", _cliente.enderecoCidade));
                        log.Info(String.Format("Estado [{0}]", _cliente.enderecoEstado));
                        log.Info(String.Format("Entrega.Cidade [{0}]", _cliente.enderecoEntregaCidade));
                        log.Info(String.Format("Entrega.Estado [{0}]", _cliente.enderecoEntregaEstado));
                    }
                    else
                    {

                        _cliente = new Cliente();

                        _cliente.id = Guid.NewGuid();
                        _cliente.cpfCnpj = p.cliente_Documento;
                        _cliente.nome = p.cliente_Nome;
                        _cliente.email = p.cliente_Email;
                        _cliente.dataCriacao = DateTime.Now;

                        _cliente.endereco = p.cliente_Endereco_Logradouro;
                        _cliente.enderecoNumero = p.cliente_Endereco_Numero;
                        _cliente.enderecoComplemento = p.cliente_Endereco_Complemento;
                        _cliente.enderecoBairro = p.cliente_Endereco_Bairro;
                        _cliente.enderecoCep = p.cliente_Endereco_CodigoPostal;
                        _cliente.enderecoCidade = p.cliente_Endereco_Cidade;
                        _cliente.enderecoEstado = p.cliente_Endereco_UF;

                        _cliente.enderecoIbge = (p.cliente_Endereco_IBGE.Length <= 5 ? p.cliente_Endereco_IBGE : p.cliente_Endereco_IBGE.Substring(2, 5));

                        _cliente.enderecoReferencia = p.cliente_Endereco_Referencia;

                        _cliente.telefone = p.cliente_Endereco_Telefone1;
                        _cliente.telefone2 = p.cliente_Endereco_Telefone2;
                        _cliente.telefone3 = "";

                        _cliente.enderecoEntrega = p.entrega_Logradouro;
                        _cliente.enderecoEntregaNumero = p.entrega_Numero;
                        _cliente.enderecoEntregaComplemento = p.entrega_Complemento;
                        _cliente.enderecoEntregaBairro = p.entrega_Bairro;
                        _cliente.enderecoEntregaCep = p.entrega_CodigoPostal;
                        _cliente.enderecoEntregaCidade = p.entrega_Cidade;
                        _cliente.enderecoEntregaEstado = p.entrega_UF;

                        _cliente.enderecoEntregaIbge = p.cliente_Endereco_Referencia;

                        log.Info("New-Client");
                        log.Info(String.Format("CpfCnpj [{0}]", _cliente.cpfCnpj));
                        log.Info(String.Format("Nome [{0}]", _cliente.nome));
                        log.Info(String.Format("Cidade [{0}]", _cliente.enderecoCidade));
                        log.Info(String.Format("Estado [{0}]", _cliente.enderecoEstado));
                        log.Info(String.Format("Entrega.Cidade [{0}]", _cliente.enderecoEntregaCidade));
                        log.Info(String.Format("Entrega.Estado [{0}]", _cliente.enderecoEntregaEstado));

                        _cliente.cro = "10010089"; /* _DEBUG_ */
                        _cliente.croEstado = "SP"; /* _DEBUG_ */

                        _cliente.especialidade = "";
                        _cliente.profissao = "";

                        _cliente.tipo = 0;

                        foreach (Informacoes_AdicionaisL ia in p.cliente_InformacoesAdicionais)
                        {
                            if (ia.nome.Equals("Especialidade"))
                            {
                                _cliente.especialidade = ia.valor;
                            }
                            if (ia.nome.Equals("Profissão"))
                            {
                                _cliente.profissao = ia.valor;
                            }

                            if (ia.nome.Equals("CRO"))
                            {
                                _cliente.cro = ia.valor;
                            }
                            if (ia.nome.Equals("CRO Estado"))
                            {
                                _cliente.croEstado = ia.valor;
                            }
                            if (ia.nome.Equals("Tipo"))
                            {
                                _cliente.tipo = 0; // ia.valor;
                            }
                        }

                        db.Cliente.Add(_cliente);

                        db.SaveChanges();

                    }

                    _pedido.cliente = _cliente;

                    _estoqueHub = new EstoqueL();

                    int _xcont = 0;
                    foreach (Pedido_ItemL pi in p.itens)
                    {
                        _pedido.adicionarItem(
                            pi.sku,
                            pi.descricao,
                            "N", // brinde
                            Decimal.Parse(pi.quantidade),
                            Decimal.Parse(pi.precoUnitario),
                            Decimal.Parse(pi.desconto)
                        );

                        _estoqueHub.estoques.Add(new EstoqueItemL(pi.sku, int.Parse(pi.quantidade)));
                        _xcont++;
                    }

                    db.Pedido.Add(_pedido);
                    db.SaveChanges();

                    if (_xcont > 0)
                    {
                        UpdEstoqueHub(log, db);

                        EstoqueC _estoque = new EstoqueC();
                        foreach (var _item in _estoqueHub.estoques)
                        {
                            _estoque.produtos.Add(new EstoqueItemC("", _item.sku, "", _item.quantidade));
                        }
                        HubCS.UpdEstoque(_estoque).Wait();

                        if (HubCS.GetMessage().Length != 0)
                            log.Error(String.Format("GetOrders.HubCS.UpdApiEstoqueCs.Exception [{0}]", HubCS.GetMessage()));
                    }

                    HubTotvs hub = new HubTotvs();

                    if (hub.txtFile(_pedido) == true)
                    {
                        var _newOrder = db.Pedido.Find(_pedido.id);
                        if (_newOrder != null)
                        {
                            _newOrder.hubStatus = OrderStatus.ARQUIVO_INTEGRACAO_GERADO;
                            db.SaveChanges();
                            _icont++;
                        }
                    }
                    else
                    {
                        log.Info(String.Format("Problemas ao gerar arquivo [{0}.REM]!", _pedido.numeroPedido));
                        if (hub.GetMessage().Length != 0)
                            log.Error(String.Format("HubTotvs.txtFile.Exception [{0}]", hub.GetMessage()));
                    }

                }

            }
            catch (Exception e)
            {
                log.Error("GetOrders.Exception [" + e.Message + "]");
                String innerMessage = ((e.InnerException != null) ? e.InnerException.Message : "");
                log.Error("GetOrders.InnerException [" + innerMessage + "]");
            }

            DefParametro(log, _ini, _fim, _icont);

            log.Info("GetOrders-End");

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

        public static bool VerifyOrder(PedidoL _pedido, TraceWriter log)
        {
            bool _resp = true;
            if (_pedido.cliente_Email == "")
            {
                log.Error(String.Format("{0} {1}", "FALTA_EMAIL", -1));
                _resp = false;
            }
            if (_pedido.cliente_Documento == "")
            {
                log.Error(String.Format("{0} {1}", "FALTA_CNPJ", TotvsStatus.FALTA_CNPJ));
                _resp = false;
            }
            if (_pedido.cliente_RazaoSocial == "")
            {
                log.Error(String.Format("{0} {1}", "FALTA_RAZAO_SOCIAL", TotvsStatus.FALTA_RAZAO_SOCIAL));
                _resp = false;
            }
            if (_pedido.cliente_NomeFantasia == "")
            {
                log.Error(String.Format("{0} {1}", "FALTA_NOME_FANTASIA", TotvsStatus.FALTA_NOME_FANTASIA));
                _resp = false;
            }

            if (_pedido.cliente_Endereco_Logradouro == "")
            {
                log.Error(String.Format("{0} {1}", "FALTA_ENDERECO_LOGRADOURO", TotvsStatus.FALTA_ENDERECO));
                _resp = false;
            }
            if (_pedido.cliente_Endereco_Numero == "")
            {
                log.Error(String.Format("{0} {1}", "FALTA_ENDERECO_NUMERO", TotvsStatus.FALTA_ENDERECO));
                _resp = false;
            }
            if (_pedido.cliente_Endereco_Bairro == "")
            {
                log.Error(String.Format("{0} {1}", "FALTA_ENDERECO_BAIRRO", TotvsStatus.FALTA_ENDERECO));
                _resp = false;
            }
            if (_pedido.cliente_Endereco_CodigoPostal == "")
            {
                log.Error(String.Format("{0} {1}", "FALTA_ENDERECO_CEP", TotvsStatus.FALTA_ENDERECO));
                _resp = false;
            }
            if (_pedido.cliente_Endereco_Cidade == "")
            {
                log.Error(String.Format("{0} {1}", "FALTA_ENDERECO_CIDADE", TotvsStatus.FALTA_ENDERECO));
                _resp = false;
            }
            if (_pedido.cliente_Endereco_UF == "")
            {
                log.Error(String.Format("{0} {1}", "FALTA_ENDERECO_UF", TotvsStatus.FALTA_ENDERECO));
                _resp = false;
            }

            if (_pedido.cliente_Endereco_IBGE == "")
            {
                log.Error(String.Format("{0} {1}", "COD_MUNICIPIO_INVALIDO", TotvsStatus.COD_MUNICIPIO_INVALIDO));
                _resp = false;
            }

            if (_pedido.entrega_Logradouro == "")
            {
                log.Error(String.Format("{0} {1}", "FALTA_ENDERECO_ENTREGA_LOGRADOURO", TotvsStatus.FALTA_ENDERECO_ENTREGA));
                _resp = false;
            }
            if (_pedido.entrega_Numero == "")
            {
                log.Error(String.Format("{0} {1}", "FALTA_ENDERECO_ENTREGA_NUMERO", TotvsStatus.FALTA_ENDERECO_ENTREGA));
                _resp = false;
            }
            if (_pedido.entrega_Bairro == "")
            {
                log.Error(String.Format("{0} {1}", "FALTA_ENDERECO_ENTREGA_BAIRRO", TotvsStatus.FALTA_ENDERECO_ENTREGA));
                _resp = false;
            }
            if (_pedido.entrega_CodigoPostal == "")
            {
                log.Error(String.Format("{0} {1}", "FALTA_ENDERECO_ENTREGA_CEP", TotvsStatus.FALTA_ENDERECO_ENTREGA));
                _resp = false;
            }
            if (_pedido.entrega_Cidade == "")
            {
                log.Error(String.Format("{0} {1}", "FALTA_ENDERECO_ENTREGA_CIDADE", TotvsStatus.FALTA_ENDERECO_ENTREGA));
                _resp = false;
            }
            if (_pedido.entrega_UF == "")
            {
                log.Error(String.Format("{0} {1}", "FALTA_ENDERECO_ENTREGA_UF", TotvsStatus.FALTA_ENDERECO_ENTREGA));
                _resp = false;
            }

            if (_pedido.pagamento_Nome == "")
            {
                log.Error(String.Format("{0} {1}", "PAGAMENTO_INVALIDO", TotvsStatus.PAGAMENTO_INVALIDO));
                _resp = false;
            }

            /* 
            TotvsStatus.PRODUTO_INVALIDO
            TotvsStatus.QUANTIDADE_ZERADA
            TotvsStatus.UNITARIO_INVALIDO
            TotvsStatus.DESCONTO_INVALIDO
            TotvsStatus.TOTAL_INVALIDO
            TotvsStatus.PEDIDO_JAH_IMPORTADO 
            */

            //_resp = true; /* _DEBUG_ */

            return _resp;
        }

        public static bool UpdEstoqueHub(TraceWriter log, SMContext db, int _reset = 0)
        {
            bool _resp = false;

            EstoqueL _estoqueHubD = new EstoqueL();

            int _icont = 0;
            foreach (var _item in _estoqueHub.estoques)
            {
                log.Info(String.Format("Sku [{0}]", _item.sku.Trim()));
                log.Info(String.Format("Estoque [{0}]", _item.quantidade));

                var _produto = new Produto();

                //_produto = db.Produto.First(c => c.idTotvs.Equals(_item.sku.Trim()));

                _produto = db.Produto.Where(c => c.idTotvs.Equals(_item.sku.Trim())).FirstOrDefault();

                if (_produto != null)
                {
                    log.Info(String.Format("Estoque.Atual [{0}]", _produto.estoque));
                    if (_reset == 0)
                    {
                        _produto.estoque = _produto.estoque - _item.quantidade;
                    }
                    else
                    {
                        log.Info(String.Format("Reset [OK]"));
                        _produto.estoque = _item.quantidade;
                    }
                    _estoqueHubD.estoques.Add(new EstoqueItemL(_item.sku.Trim(), _produto.estoque));
                    db.SaveChanges();
                    log.Info("Update [OK]");
                    _icont++;
                }
                else
                    log.Warning("Update [NOK-Product_Not_Found!]");
            }

            if (_icont > 0)
            {
                _estoqueHub = _estoqueHubD;
            }

            return _resp;
        }

    }
}
