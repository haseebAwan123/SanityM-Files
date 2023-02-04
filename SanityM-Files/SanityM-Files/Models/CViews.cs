using M_FilesApis.Authentication;
using M_FilesApis.Views;
using MFaaP.MFWSClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SanityM_Files.Models
{
    public class CViews
    {
        string baseUrl = ConfigurationManager.AppSettings["hostUrl"];
       
        public dynamic getRootView(string userName, string password)
        {
            var viewsList = new FolderContentItems();
            try
            {
                Login auth = new Login();
                string vaultGuid = "{65CE6D22-8BF6-4D00-A461-42F8263F4066}";
                string authKey = auth.LoginToServer(baseUrl, userName, password, vaultGuid);
                Views view = new Views();
                viewsList = view.GetItemsinRootView(baseUrl, authKey);
                }
            catch (Exception ex)
            {
                return ex.Message;

            }
            return viewsList;
        }

        public dynamic getViewItems(string userName, string password,int VID)
        {
            var viewsList = new FolderContentItems();
            try
            {
                Login auth = new Login();
                string vaultGuid = "{65CE6D22-8BF6-4D00-A461-42F8263F4066}";
                string authKey = auth.LoginToServer(baseUrl, userName, password, vaultGuid);
                Views view = new Views();
                viewsList = view.GetItemsinView(baseUrl, authKey, VID);
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            return viewsList;
        }

        public dynamic getSubViewItems(string userName, string password, int VID,int LID)
        {
            var viewsList = new FolderContentItems();
            try
            {
                Login auth = new Login();
                string vaultGuid = "{65CE6D22-8BF6-4D00-A461-42F8263F4066}";
                string authKey = auth.LoginToServer(baseUrl, userName, password, vaultGuid);
                Views view = new Views();
                viewsList = view.GetSubViewItem(baseUrl, authKey, VID, LID);
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            return viewsList;
        }
    }
}