using M_FilesApis.Authentication;
using M_FilesApis.MPropertyValue;
using M_FilesApis.Object;
using MFaaP.MFWSClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace SanityM_Files.Models
{
    public class CObjects
    {
        
        string baseUrl = ConfigurationManager.AppSettings["hostUrl"];

        public dynamic getObjectMetaData(string userName, string password,int type,int ID, int version)
        {
            // var viewsList = new FolderContentItems();
            try
            {
                Login auth = new Login();
                string vaultGuid = "{65CE6D22-8BF6-4D00-A461-42F8263F4066}";
                string authKey = auth.LoginToServer(baseUrl, userName, password, vaultGuid);
                MObjects obj = new MObjects();
                var res = obj.getObjectMetaData(baseUrl, type, ID, version, authKey);
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            
        }

        public dynamic getObjectRecentlyAccessedByMe(string userName, string password)
        {
            // var viewsList = new FolderContentItems();
            try
            {
                Login auth = new Login();
                string vaultGuid = "{65CE6D22-8BF6-4D00-A461-42F8263F4066}";
                string authKey = auth.LoginToServer(baseUrl, userName, password, vaultGuid);
                MObjects obj = new MObjects();
                var res = obj.getObjectRecentlyAccessedByMe(baseUrl, authKey);
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }

        }

        public dynamic GetPropertyValueList(int ValueListID)
        {
            // var viewsList = new FolderContentItems();
            try
            {
                Login auth = new Login();
                string vaultGuid = "{65CE6D22-8BF6-4D00-A461-42F8263F4066}";
                string authKey = auth.LoginToServer(baseUrl, "Admin", "123", vaultGuid);
                MPropertyValue obj = new MPropertyValue();
                var res = obj.GetPropertyValueList(baseUrl, authKey, ValueListID);
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }

        }

        public dynamic GetProperty(string PID)
        {
            // var viewsList = new FolderContentItems();
            try
            {
                Login auth = new Login();
                string vaultGuid = "{65CE6D22-8BF6-4D00-A461-42F8263F4066}";
                string authKey = auth.LoginToServer(baseUrl, "Admin", "123", vaultGuid);
                MPropertyValue obj = new MPropertyValue();
                var res = obj.GetProperty(baseUrl, authKey, PID);
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }

        }
        public async System.Threading.Tasks.Task updateAsync(List<PropertyValue> payload, string ID, string type)
        {
            Login auth = new Login();
            string vaultGuid = "{65CE6D22-8BF6-4D00-A461-42F8263F4066}";
            string authKey = auth.LoginToServer(baseUrl, "Admin", "123", vaultGuid);

            // Create a HttpClient.
            var client = new System.Net.Http.HttpClient();

            // Authenticate.
            client.DefaultRequestHeaders.Add("X-Authentication",authKey);
          
         
            var ov2 = await client.PostAsync(new Uri("http://localhost/REST/objects/" + type + "/" + ID+"/latest/properties?_method=PUT"),
                         new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload))).ConfigureAwait(false);
           
        }

    }
}