using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace FunctionApp1
{
    public static class Function2
    {
        [FunctionName("Function2")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            HttpStatusCode result;

            string contentType;

            string _file = "";

            result = HttpStatusCode.BadRequest;

            contentType = req.Content.Headers?.ContentType?.MediaType;

            if (contentType == "application/json")
            {
                string body;

                body = await req.Content.ReadAsStringAsync();

                body = "";
                body += "CABPW000076  2018010905005873000100hpbrasileiro @gmail.com PORTAL LTDA                                                                0                 RUA DOMINGOS SIMOES                     22        SL 10                         VILA SUZANA                   SAO PAULO      SP0563001050308EM FRENTE AO MASP                                 4333212020     0              RUA DOMINGOS SIMOES                     22        SL 10                         VILA SUZANA                   SAO PAULO      SP0563001050308003000000000000                                                  10010089SP009";
                body += "\r\n";
                body += "ITPPW000076  001101000002      000000400000000002000000000008000000000000000N";
                body += "\r\n";

                if (!string.IsNullOrEmpty(body))
                {
                    string name;

                    name = Guid.NewGuid().ToString("n");

                    _file = name + ".TXT";

                    await CreateBlob(_file, body, log);

                    result = HttpStatusCode.OK;
                }
            }

            log.Info("File [" + _file + "]");

            return req.CreateResponse(result, _file); // string.Empty);

        }

        private async static Task CreateBlob(string name, string data, TraceWriter log)
        {
            string accessKey;
            string accountName;
            string connectionString;
            CloudStorageAccount storageAccount;
            CloudBlobClient client;
            CloudBlobContainer container;
            CloudBlockBlob blob;

            accessKey = "devstoreaccount1";
            accountName = "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==";
            connectionString = "DefaultEndpointsProtocol=http;AccountName=" + accountName + ";AccountKey=" + accessKey; // + ";EndpointSuffix=core.windows.net";

            connectionString = "UseDevelopmentStorage=true";

            storageAccount = CloudStorageAccount.Parse(connectionString);

            client = storageAccount.CreateCloudBlobClient();

            container = client.GetContainerReference("insitism-storage");

            await container.CreateIfNotExistsAsync();

            var reference = container.GetDirectoryReference("REMESSA");

            blob = reference.GetBlockBlobReference(name);
            blob.Properties.ContentType = "application/json";

            using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
            {
                await blob.UploadFromStreamAsync(stream);
            }
        }

    }
}
