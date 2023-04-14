using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;

namespace BHNASAapp.Models{
    public class BHClient : HttpClient{
        private readonly string BasePath;
        private readonly string MEDIA_TYPE;
        private readonly string APIKEY;
        public BHClient(string baseAddress, string basePath, string mediaType, string apiKey){
            BaseAddress = new Uri(baseAddress);
            BasePath = basePath;
            MEDIA_TYPE = mediaType;
            APIKEY = apiKey;
        }
        public async Task<BHModel> Get(){
            try{
                SetupHeaders();
                var response = await GetAsync(BasePath + $"?api_key={APIKEY}");
                Console.WriteLine(response);
                if (response.IsSuccessStatusCode) {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(result);
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<BHModel>(result); ;
                }
                else{
                    Console.WriteLine("in else");
                    throw new Exception
                    ($"Failed to retrieve items returned {response.StatusCode}");
                }
            }
            catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }
        protected virtual void SetupHeaders() {
            DefaultRequestHeaders.Clear();
            //Define request data format
            DefaultRequestHeaders.Accept.Add(new
           MediaTypeWithQualityHeaderValue(MEDIA_TYPE));
        }
    }
}
//Brendan Hannon CPS-3330-01 Spring2023 HW 7