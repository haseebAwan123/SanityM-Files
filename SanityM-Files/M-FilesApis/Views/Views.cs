using MFaaP.MFWSClient;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_FilesApis.Views
{
    public class Views
    {
        //root
        public dynamic GetItemsinRootView(string baseUrl, string key)
        {
            var viewsList = new FolderContentItems();
            try
            {
                var client = new RestClient(baseUrl+"/views/items");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-Authentication", key);
                request.AddHeader("Cookie", "ASP.NET_SessionId=wlshnuqyiz3nwflld3dnuyr5");
                IRestResponse response = client.Execute(request);
                //dynamic responseContent = JsonConvert.DeserializeObject(response.Content);
                 viewsList = JsonConvert.DeserializeObject<FolderContentItems>(response.Content);
                
               
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            return viewsList;
        }

        //items in one root view
        public dynamic GetItemsinView(string baseUrl, string key,int VID)
        {
            var viewsList = new FolderContentItems();
            try
            {
                string ViewID = "v" + VID.ToString();
                var client = new RestClient(baseUrl + "/views/"+ ViewID +"/items");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-Authentication", key);
                request.AddHeader("Cookie", "ASP.NET_SessionId=wlshnuqyiz3nwflld3dnuyr5");
                IRestResponse response = client.Execute(request);
                //dynamic responseContent = JsonConvert.DeserializeObject(response.Content);
                viewsList = JsonConvert.DeserializeObject<FolderContentItems>(response.Content);


            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            return viewsList;
        }

        //items in one sub view
        public dynamic GetSubViewItem(string baseUrl, string key, int VID,int LID)
        {
            
            var viewsList = new FolderContentItems();
            try
            {
                string ViewID = "v" + VID.ToString();
                string L_ID = "L" + LID.ToString();
                var client = new RestClient(baseUrl + "/views/" + ViewID + "/"+ L_ID + "/items");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-Authentication", key);
                request.AddHeader("Cookie", "ASP.NET_SessionId=wlshnuqyiz3nwflld3dnuyr5");
                IRestResponse response = client.Execute(request);
                //dynamic responseContent = JsonConvert.DeserializeObject(response.Content);
                viewsList = JsonConvert.DeserializeObject<FolderContentItems>(response.Content);


            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            return viewsList;
        }

    }
}
