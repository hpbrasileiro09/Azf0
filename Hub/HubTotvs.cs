using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1.Hub
{
    public class HubTotvs
    {

        private string _message = "";

        public string GetMessage()
        {
            return _message;
        }

        public bool txtFile(Pedido _pedido)
        {

            _message = "";

            try
            {

                //define header do arquivo
                string header = "";

                header = header + "CAB";
                header = header + this.txtFileTrataString(_pedido.numeroPedido, 10, ' ', 1);
                header = header + this.txtFileTrataData(_pedido.dataCriacao);
                header = header + this.txtFileTrataString(txtFileExtrairNumeros(_pedido.cliente.cpfCnpj), 14, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.email, 50, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.nome, 50, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.nomeFantasia, 25, ' ', 1);
                header = header + this.txtFileTrataString(this.txtFileExtrairNumeros(_pedido.cliente.inscricaoEstadual), 18, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.endereco, 40, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.enderecoNumero, 10, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.enderecoComplemento, 30, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.enderecoBairro, 30, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.enderecoCidade, 15, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.enderecoEstado, 2);
                header = header + this.txtFileTrataString(this.txtFileExtrairNumeros(_pedido.cliente.enderecoCep), 8, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.enderecoIbge, 5, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.enderecoReferencia, 50, ' ', 1);
                header = header + this.txtFileTrataString(this.txtFileExtrairNumeros(_pedido.cliente.telefone), 15, ' ', 1);
                header = header + this.txtFileTrataString(this.txtFileExtrairNumeros(_pedido.cliente.telefone2), 15, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.endereco, 40, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.enderecoNumero, 10, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.enderecoComplemento, 30, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.enderecoBairro, 30, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.enderecoCidade, 15, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.enderecoEstado, 2);
                header = header + this.txtFileTrataString(this.txtFileExtrairNumeros(_pedido.cliente.enderecoCep), 8, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.enderecoIbge, 5, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.condicaoPagamento, 3, ' ', 1);
                header = header + this.txtFileTrataString(this.txtFileTrataDecimal(_pedido.valorTotalFrete), 12, '0', 0);

                //Layout 2.0
                header = header + this.txtFileTrataString(_pedido.cliente.enderecoEntregaReferencia, 50, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.cro, 8, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.croEstado, 2, ' ', 1);

                //Layout 3.0
                header = header + this.txtFileTrataString(_pedido.vendedor, 6, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.mensagem, 200, ' ', 1);

                //Layout 3.0 - 2018.01.22 #
                header = header + this.txtFileTrataString(_pedido.cliente.especialidade, 50, ' ', 1);
                header = header + this.txtFileTrataString(_pedido.cliente.profissao, 50, ' ', 1);

                //define body do arquivo
                string body = "";
                int itemSequencial = 1;
                foreach (PedidoItem item in _pedido.itens)
                {
                    body = body + "ITP";
                    body = body + this.txtFileTrataString(_pedido.numeroPedido, 10, ' ', 1);
                    body = body + this.txtFileTrataString(itemSequencial.ToString(), 3, '0', 0);
                    body = body + this.txtFileTrataString(item.produtoSku, 15, ' ', 1);
                    body = body + this.txtFileTrataString(this.txtFileTrataDecimal(item.quantidade), 9, '0', 0);
                    body = body + this.txtFileTrataString(this.txtFileTrataDecimal(item.valorUnitario), 12, '0', 0);
                    body = body + this.txtFileTrataString(this.txtFileTrataDecimal(item.valorTotal), 12, '0', 0);
                    body = body + this.txtFileTrataString(this.txtFileTrataDecimal(item.valorDesconto), 12, '0', 0);

                    //Layout 3.0
                    body = body + this.txtFileTrataString(item.brinde, 1, ' ', 1);

                    body = body + "\r\n";
                    itemSequencial++;
                }

                //constroi nome do arquivo
                string nomeArquivo = _pedido.numeroPedido + ".REM";

                //gera arquivo texto
                this.txtFileWriter(nomeArquivo, header, body);

            }
            catch (Exception e)
            {

                _message = e.Message;

                return false;
            }

            return true;

        }

        public void txtFileWriter(string _nomeArquivo, string _fileHeader, string _fileBody)
        {

            string _path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(_path);
            var path_origem = System.IO.Path.Combine(directory, "REMESSA");
            var origem = System.IO.Path.Combine(path_origem, _nomeArquivo);

            //write text file
            using (StreamWriter sw = new StreamWriter(origem))
            {
                sw.WriteLine(_fileHeader);
                sw.WriteLine(_fileBody);
                sw.Close();
            }
        }

        public string removerAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }

        public string txtFileTrataString(string _value, int _tamanho, char _caracter = ' ', int _direcao = 1)
        {
            string _resp = "";

            if (_value != null)
            {
                _value = removerAcentos(_value);
                if (_value.Length < _tamanho)
                {
                    string tabs = new String(_caracter, _tamanho - _value.Length);
                    if (_direcao == 0)
                        _resp = tabs.ToString() + _value;
                    else
                        _resp = _value + tabs.ToString();
                }
                else
                {
                    _resp = _value.Substring(0, _tamanho);
                }
            }
            else
            {
                string tabs = new String(_caracter, _tamanho);
                _resp = tabs.ToString();
            }
            return _resp;
        }

        public string txtFileTrataData(DateTime _data)
        {
            //_data = new DateTime(2017,10,10);

            string dataFormatada = "";

            dataFormatada = dataFormatada + _data.Year.ToString();

            //adiciona zero para meses menores que 10
            if (_data.Month < 10)
            {
                dataFormatada = dataFormatada + "0" + _data.Month.ToString();
            }
            else
            {
                dataFormatada = dataFormatada + _data.Month.ToString();
            }

            //adiciona zero para dias menores que 10
            if (_data.Day < 10)
            {
                dataFormatada = dataFormatada + "0" + _data.Day.ToString();
            }
            else
            {
                dataFormatada = dataFormatada + _data.Day.ToString();
            }


            return dataFormatada;
        }

        public string txtFileTrataDecimal(Decimal _valor)
        {
            if (_valor != 0)
            {
                string result = _valor.ToString("#.00");
                return this.txtFileExtrairNumeros(result);
            }
            else { return "0"; }

        }

        public string txtFileExtrairNumeros(string _valor)
        {
            if (_valor != null)
            {
                string result = "";
                char[] caracteres = _valor.ToCharArray();
                //percorre caracteres
                foreach (char c in caracteres)
                {
                    if (Char.IsNumber(c))
                    {
                        result = result + c.ToString();
                    }
                }

                return result;
            }
            else { return "0"; }

        }

    }

    public class StatusHub
    {
        public StatusHub(int _target, string _target_ds, string _target_pw, string _target_cs, string _source, string _source_ds)
        {
            target = _target;
            target_ds = _target_ds;
            target_pw = _target_pw;
            target_cs = _target_cs;
            source = _source;
            source_ds = _source_ds;
        }
        public int target { get; set; }
        public string target_ds { get; set; }
        public string target_pw { get; set; }
        public string target_cs { get; set; }
        public string source { get; set; }
        public string source_ds { get; set; }
    }

    public class PaymentType
    {
        public PaymentType(string _target, string _description, string _source, int _times)
        {
            target = _target;
            description = _description;
            source = _source;
            times = _times;
        }
        public string target { get; set; }
        public string description { get; set; }
        public string source { get; set; }
        public int times { get; set; }
    }
}
