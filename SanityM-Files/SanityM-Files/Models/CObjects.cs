using M_FilesApis.Authentication;
using M_FilesApis.Object;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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



    }
}