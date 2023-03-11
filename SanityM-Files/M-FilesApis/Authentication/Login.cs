using MFaaP.MFWSClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace M_FilesApis.Authentication
{
    public class Login
    {


        public  string LoginToServer(string baseUrl, string userName, string password)
        {
            string Token = "";
            try
            { 
                var jsonSerializer = JsonSerializer.CreateDefault();

                var auth = new
                {
                    Username = userName,
                    Password = password,
                   // VaultGuid = "{126EE1EE-F46B-46CD-BB67-5400631CD3F8}" // Use GUID format with {braces}.
                };
                string url = baseUrl+"/server/authenticationtokens.aspx";
                var authenticationRequest = (HttpWebRequest)WebRequest.Create(url);
                authenticationRequest.Method = "POST";

                // Add the authentication details to the request stream.
                using (var streamWriter = new StreamWriter(authenticationRequest.GetRequestStream()))
                {
                    using (var jsonTextWriter = new JsonTextWriter(streamWriter))
                    {
                        jsonSerializer.Serialize(jsonTextWriter, auth);
                    }
                }

                // Execute the request.
                var authenticationResponse = (HttpWebResponse)authenticationRequest.GetResponse();

                // Extract the authentication token.
                string authenticationToken = null;
                using (var streamReader = new StreamReader(authenticationResponse.GetResponseStream()))
                {
                    using (var jsonTextReader = new JsonTextReader(streamReader))
                    {
                        authenticationToken = ((dynamic)jsonSerializer.Deserialize(jsonTextReader)).Value;
                        Token = authenticationToken;
                    }
                }

            }
            catch (Exception ex)
            {



            }
            return Token;
        }

        public string LoginToServer(string baseUrl, string userName, string password , string vaultGuid)
        {
            string Token = "";
            try
            {
                var jsonSerializer = JsonSerializer.CreateDefault();

                var auth = new
                {
                    Username = userName,
                    Password = password,
                    VaultGuid = vaultGuid // Use GUID format with {braces}.
                };
                string url = baseUrl + "/server/authenticationtokens.aspx";
                var authenticationRequest = (HttpWebRequest)WebRequest.Create(url);
                authenticationRequest.Method = "POST";

                // Add the authentication details to the request stream.
                using (var streamWriter = new StreamWriter(authenticationRequest.GetRequestStream()))
                {
                    using (var jsonTextWriter = new JsonTextWriter(streamWriter))
                    {
                        jsonSerializer.Serialize(jsonTextWriter, auth);
                    }
                }

                // Execute the request.
                var authenticationResponse = (HttpWebResponse)authenticationRequest.GetResponse();

                // Extract the authentication token.
                string authenticationToken = null;
                using (var streamReader = new StreamReader(authenticationResponse.GetResponseStream()))
                {
                    using (var jsonTextReader = new JsonTextReader(streamReader))
                    {
                        authenticationToken = ((dynamic)jsonSerializer.Deserialize(jsonTextReader)).Value;
                        Token = authenticationToken;
                    }
                }

            }
            catch (Exception ex)
            {



            }
            return Token;
        }

        public async Task<dynamic> getServerVaults(string baseUrl,string key)
        {
            List<Vault> vaults = new List<Vault>();

            try
            {
                var url =
                      new Uri(baseUrl+"/server/vaults");

                // Build the request.
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("X-Authentication", key);

                // Start the request.
                var responseBody = await httpClient.GetAsync(url).ConfigureAwait(false);
                if (responseBody.StatusCode.Equals(403))
                {
                    return "Login to application failed";
                }
                else
                {
                 vaults = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Vault>>(await responseBody.Content.ReadAsStringAsync().ConfigureAwait(false));
                }

            }
            catch (Exception ex)
            {
                return "Login to application failed";
            }
            return vaults;
        }
    }
}





