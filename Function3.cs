using System;
using System.Collections.Generic;
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
using Newtonsoft.Json;

namespace FunctionApp1
{
    public static class Function3
    {
        [FunctionName("Function3")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

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

            //string _file = "fa84add9665f49b980395a9d0f81cd07.TXT";

            List<String> _group = new List<String>();

            var _files = container.ListBlobs();
            foreach (IListBlobItem _item in _files)
            {
                log.Info(String.Format("Container [{0}]", _item.Uri));
                _group.Add(_item.Uri.ToString());
            }

            var sampleDir = container.GetDirectoryReference("REMESSA");
            var destDir = container.GetDirectoryReference("ENVIADO");

            _files = sampleDir.ListBlobs();
            foreach(IListBlobItem _item in _files)
            {
                CloudBlockBlob _blob = (CloudBlockBlob) _item;
                _blob.FetchAttributes();
                log.Info(String.Format("Directory [REMESSA][{0}]", _item.Uri));
                _group.Add(_item.Uri.ToString());
                _group.Add(JsonConvert.SerializeObject(_blob));

                try
                {
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Constants.TOTVS_FTP_HOST + "/" + _blob.Name);
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential(Constants.TOTVS_FTP_LGN, Constants.TOVS_FTP_PWD);
                    StreamReader sourceStream = new StreamReader(_blob.OpenRead());
                    byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                    sourceStream.Close();
                    request.ContentLength = fileContents.Length;
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(fileContents, 0, fileContents.Length);
                    requestStream.Close();

                    CloudBlockBlob destBlob;
                    string name = _blob.Uri.Segments.Last();
                    destBlob = destDir.GetBlockBlobReference(name);
                    await destBlob.StartCopyAsync(_blob);
                    _blob.Delete();

                }
                catch (Exception e)
                {
                    log.Error("UploadOrders.Exception [" + e.Message + "]");
                }
            }

            /*
            var fileToCreate = sampleDir.GetBlockBlobReference("hernando.TXT");
            fileToCreate.UploadText("Hello Brou!");
            */

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(_group), Encoding.UTF8, "application/json")
            };

            //return req.CreateResponse(HttpStatusCode.OK, "Function 3 ...");
        }
    }
}
