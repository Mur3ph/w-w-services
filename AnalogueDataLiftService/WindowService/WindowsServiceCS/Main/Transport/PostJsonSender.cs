using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using WindowsServiceCS.Main.Logging;

namespace WindowsServiceCS.Main.Transport
{
    class PostJsonSender
    {
        private HttpClient _httpClient;
        private static readonly Logger _log = new Logger(new PostJsonSender().GetType());

        public PostJsonSender() { }

        //Post Json and remote database name to the web service address from the registry
        public async void postJson(string postJsonData, string webServiceUrl)
        {
            try
            {
                _log.Debug("+postJson()");
                using (_httpClient = new HttpClient())
                {
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage wcfResponse = await _httpClient.PostAsync(new Uri(webServiceUrl), new StringContent(postJsonData, Encoding.UTF8, "application/json"));
                    if (wcfResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string responce = await wcfResponse.Content.ReadAsStringAsync();
                    }
                }
                _log.Debug("-postJson()");
            }
            catch (HttpRequestException hre)
            {
                _log.Error("-postJson(): Message: " + hre.Message + " InnerException: " + hre.InnerException);
            }
            catch (TaskCanceledException e)
            {
                _log.Error("-postJson(): Message: " + e.Message + " InnerException: " + e.InnerException);
            }
            catch (Exception ex)
            {
                _log.Error("-postJson(): Message: " + ex.Message + " InnerException: " + ex.InnerException);
            }
            finally
            {
                //TODO: Can't close httpClient in an Asynchronous method as another process could be using it as we are closing it
                //_log.Debug("-postJson() Finally ");
                //if (_httpClient != null)
                //{
                //    _httpClient.Dispose();
                //    _httpClient = null;
                //}
            }
        }
    }
}
