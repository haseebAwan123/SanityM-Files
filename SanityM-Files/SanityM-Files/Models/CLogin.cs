using M_FilesApis.Authentication;
using M_FilesApis.Object;
using MFaaP.MFWSClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace SanityM_Files.Models
{
    public class CLogin
    {
        string baseUrl = ConfigurationManager.AppSettings["hostUrl"];
        public dynamic getServerVaults(string userName, string password)
        {
            var vaults = new ExpandoObject();
            try
            {
                Login obj = new Login();
                string baseUrl = ConfigurationManager.AppSettings["hostUrl"]; 
                string authKey = obj.LoginToServer(baseUrl, userName, password);
                var res = obj.getServerVaults(baseUrl, authKey).Result;
                var aas = res.GetType().ToString();
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            //return "";
        }

        public bool Login(string userName, string password)
        {
            var isExist = false;
            try
            {
                Login auth = new Login();
                MObjects obj = new MObjects();

                string vaultGuid = "{65CE6D22-8BF6-4D00-A461-42F8263F4066}";
                string authKey = auth.LoginToServer(baseUrl, userName, password, vaultGuid);
                isExist = obj.getObjectByMetaData(baseUrl,userName,password, authKey);
               
            }
            catch (Exception ex)
            {
                return isExist;

            }
            return isExist;
        }
    }
}