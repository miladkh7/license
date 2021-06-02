using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace LicenseDB
{
    class LisenseKey
    {
        public string _id { set; get; }
        public string key { set; get; }

        public string machineID { set; get; }
    }
    class License
    {
        string _apiKey= "5e0c5fac2b88e41892f7dd511abb5469b1a09";
        string _dataBaseName = "gholami-c537";
        string _id;
        string HWID, licenseKey;
        public License(string dataBaseName,string apiKey)
        {
            this._dataBaseName = dataBaseName;
            this._apiKey = apiKey;
            
        }
        public string CheckHardWareID(string dataBase,string license)
        {
            string searchQuary =string.Format(@"{{""key"":""{0}""}}",license);
            string quary = string.Format(@"https://{0}.restdb.io/rest/license?q={1}", dataBase, searchQuary);
            Console.WriteLine(quary);
            var client = new RestClient(quary);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-api-key", _apiKey);
            IRestResponse response = client.Execute(request);
            var userLisences = JsonConvert.DeserializeObject <List<LisenseKey>>(response.Content);
         

            try
            {
                LisenseKey userLisence = userLisences[0];
                Console.WriteLine(userLisence._id);
                this.licenseKey = license;
                this._id = userLisence._id;
                return userLisence.machineID;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public void RegisterNewLisence(string HWID)
        {
            
            string quary = string.Format(@"https://{0}.restdb.io/rest/license/{1}", this._dataBaseName, this._id);

            var client = new RestClient(quary);
            var request = new RestRequest(Method.PUT);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", _apiKey);
            request.AddHeader("content-type", "application/json");
            string updateCommand = string.Format(@"{{""key"":""{0}"",""machineID"":""{1}""}}", this.licenseKey, HWID);
            request.AddParameter("application/json",updateCommand, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            Console.WriteLine("salam");
        }

    }
}
