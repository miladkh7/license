using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace LicenseDB
{
    class LsenseKey
    {
        public string key { set; get; }
        public string machineID { set; get; }
    }
    class License
    {
        string _apiKey= "5e0c5fac2b88e41892f7dd511abb5469b1a09";
        string _dataBaseName = "gholami-c537";
        public License(string dataBaseName,string apiKey)
        {
            this._dataBaseName = dataBaseName;
            this._apiKey = apiKey;
        }
        public void CheckHardWareID(string dataBase,string license)
        {
            string searchQuary =string.Format(@"{{""key"":""{0}""}}",license);
            string quary = string.Format(@"https://{0}.restdb.io/rest/license?q={1}", dataBase, searchQuary);
            Console.WriteLine(quary);
            var client = new RestClient(quary);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-api-key", _apiKey);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }

    }
}
