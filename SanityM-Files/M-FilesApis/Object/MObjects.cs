using MFaaP.MFWSClient;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace M_FilesApis.Object
{
    public class MObjects
    {


        public dynamic getObjectByMetaData(string baseUrl, string userName, string password, string key)
        {
            var objectVersion1 = new ExpandoObject();
            try
            {
                var client = new RestClient(baseUrl +"/objects.aspx?p0="+ userName+" && p1045="+ password);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-Authentication", key);
                request.AddHeader("Cookie", "ASP.NET_SessionId=kcm5k1qqe4cnfunlmaemagqe");
                IRestResponse response = client.Execute(request);
                var objectVersion = JsonConvert.DeserializeObject<dynamic>(response.Content);
                if (objectVersion.Items.Count > 0)
                {
                    
                    return true;
                }
                else
                {
                   return false;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
           
        }

        public dynamic getObjectMetaData(string baseUrl,int ObjectType,int ObjectID,int ObjectVersion, string key)
        {
            var propertyList = new List<dynamic>();
            try
            {
                var client = new RestClient(baseUrl + "/objects/"+ ObjectType +"/"+ ObjectID +"/"+ ObjectVersion +"/properties");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-Authentication", key);
                request.AddHeader("Cookie", "ASP.NET_SessionId=kcm5k1qqe4cnfunlmaemagqe");
                IRestResponse response = client.Execute(request);
                propertyList = JsonConvert.DeserializeObject<List<dynamic>>(response.Content); 
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            return propertyList;
        }


        public dynamic getObjectRecentlyAccessedByMe(string baseUrl, string key)
        {
            var objectVersion1 = new ObjectVersion();
            try
            {
                var client = new RestClient(baseUrl + "/recentlyaccessedbyme");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-Authentication", key);
                request.AddHeader("Cookie", "ASP.NET_SessionId=kcm5k1qqe4cnfunlmaemagqe");
                IRestResponse response = client.Execute(request);
                objectVersion1 = JsonConvert.DeserializeObject<ObjectVersion>(response.Content);
               
            }
            catch (Exception ex)
            {
                return objectVersion1;

            }
            return objectVersion1;
        }




    }
}
