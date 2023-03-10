using MFaaP.MFWSClient;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_FilesApis.MPropertyValue
{
    public class MPropertyValue
    {
        public dynamic GetAllProperties(string baseUrl, string key)
        {
            var propertyList = new List<PropertyDef>();
            try
            {
                var client = new RestClient(baseUrl + "/structure/properties");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-Authentication", key);
                request.AddHeader("Cookie", "ASP.NET_SessionId=wlshnuqyiz3nwflld3dnuyr5");
                IRestResponse response = client.Execute(request);
                //dynamic responseContent = JsonConvert.DeserializeObject(response.Content);
                propertyList = JsonConvert.DeserializeObject<List<PropertyDef>>(response.Content);


            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            return propertyList;
        }

        public dynamic GetAllClasses(string baseUrl, string key)
        {
            var classList = new List<ExtendedObjectClass>();
            try
            {
                var client = new RestClient(baseUrl + "/structure/classes");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-Authentication", key);
                request.AddHeader("Cookie", "ASP.NET_SessionId=wlshnuqyiz3nwflld3dnuyr5");
                IRestResponse response = client.Execute(request);
                //dynamic responseContent = JsonConvert.DeserializeObject(response.Content);
                classList = JsonConvert.DeserializeObject<List<ExtendedObjectClass>>(response.Content);


            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            return classList;
        }

        public dynamic GetPropertyValueList(string baseUrl, string key,int ValueListID)
        {
            var ValueList = new Results<ValueListItem>();
            try
            {
                var client = new RestClient(baseUrl + "/valuelists/"+ ValueListID + "/items");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-Authentication", key);
                request.AddHeader("Cookie", "ASP.NET_SessionId=wlshnuqyiz3nwflld3dnuyr5");
                IRestResponse response = client.Execute(request);
                //dynamic responseContent = JsonConvert.DeserializeObject(response.Content);
                ValueList = JsonConvert.DeserializeObject<Results<ValueListItem>>(response.Content);


            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            return ValueList;
        }


        public dynamic GetProperty(string baseUrl, string key,string id)
        {
            var propertyList = new PropertyDef();
            try
            {
                var client = new RestClient(baseUrl + "/structure/properties/"+ id);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-Authentication", key);
                request.AddHeader("Cookie", "ASP.NET_SessionId=wlshnuqyiz3nwflld3dnuyr5");
                IRestResponse response = client.Execute(request);
                //dynamic responseContent = JsonConvert.DeserializeObject(response.Content);
                propertyList = JsonConvert.DeserializeObject<PropertyDef>(response.Content);


            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            return propertyList;
        }

    }
}
