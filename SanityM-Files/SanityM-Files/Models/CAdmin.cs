using System;
using M_FilesApis.MPropertyValue;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using M_FilesApis.Authentication;

namespace SanityM_Files.Models
{
    public class CAdmin
    {
        string baseUrl = ConfigurationManager.AppSettings["hostUrl"];
        public dynamic getProperties(string userName, string password)
        {
           // var vaults = new ExpandoObject();
            try
            {
                Login obj = new Login();
                MPropertyValue propertyValue = new MPropertyValue();
                string vaultGuid = "{65CE6D22-8BF6-4D00-A461-42F8263F4066}";
                string authKey = obj.LoginToServer(baseUrl, userName, password,vaultGuid);
                var res = propertyValue.GetAllProperties(baseUrl, authKey);
               
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            //return "";
        }

        public dynamic getClasses(string userName, string password)
        {
            // var vaults = new ExpandoObject();
            try
            {
                Login obj = new Login();
                MPropertyValue propertyValue = new MPropertyValue();
                string vaultGuid = "{65CE6D22-8BF6-4D00-A461-42F8263F4066}";
                string authKey = obj.LoginToServer(baseUrl, userName, password, vaultGuid);
                var res = propertyValue.GetAllClasses(baseUrl, authKey);

                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            //return "";
        }


    }
}