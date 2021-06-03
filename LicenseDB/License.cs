using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using DeviceId;
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
        string _collectionName;
        string _id;
        string  licenseKey;
        string HWID = new DeviceIdBuilder()
        .AddMachineName()
        .AddProcessorId()
        .AddMotherboardSerialNumber()
        .AddSystemDriveSerialNumber()
        .ToString();
        public License(string dataBaseName,string CollectionName,string apiKey)
        {
            this._dataBaseName = dataBaseName;
            this._collectionName = CollectionName;
            this._apiKey = apiKey;
            
        }
        private string CheckHardWareID(string dataBase,string collection,string license)
        {
            string searchQuary =string.Format(@"{{""key"":""{0}""}}",license);
            string quary = string.Format(@"https://{0}.restdb.io/rest/{1}?q={2}", dataBase,collection, searchQuary);
            var client = new RestClient(quary);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-api-key", _apiKey);
            IRestResponse response = client.Execute(request);
            var userLisences = JsonConvert.DeserializeObject <List<LisenseKey>>(response.Content);


            try
            {
                LisenseKey userLisence = userLisences[0];
                this.licenseKey = license;
                this._id = userLisence._id;
                return userLisence.machineID;
            }
            catch (Exception)
            {

                return null;
            }
        }
        
        private void RegisterNewLisence(string HWID)
        {
            
            string quary = string.Format(@"https://{0}.restdb.io/rest/{1}/{2}", this._dataBaseName,this._collectionName, this._id);

            var client = new RestClient(quary);
            var request = new RestRequest(Method.PUT);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", _apiKey);
            request.AddHeader("content-type", "application/json");
            string updateCommand = string.Format(@"{{""key"":""{0}"",""machineID"":""{1}""}}", this.licenseKey, HWID);
            request.AddParameter("application/json",updateCommand, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }
        public bool CheckLicense(string productLicense)
        {
            string result = this.CheckHardWareID(this._dataBaseName, this._collectionName,productLicense);
            if (result == null)
            {
                Console.WriteLine("Invalid Licencse");
                return false;
            }
            else if (result == string.Empty)
            {
                this.RegisterNewLisence(HWID);
                Console.WriteLine("Successfully Register Your License");

                return true;

            }
            else if (result == HWID)
            {
                Console.WriteLine("you registered this machine yet");
                return true;
            }
            Console.WriteLine("this License key used before");
          
                return false;
        }

    }
}
