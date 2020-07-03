using System;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Net;
using BALIBAR.Models;
using Newtonsoft.Json;
using Facebook;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace BALIBAR.Handlers
{
    public class FaceBookHandler 
    {
        private const string FacebookApiID = "2682828845371928";
        private const string FacebookApiSecret = "e1654646df828222ff12a80f07ddb20c";

        private const string PageID = "103846034722719";                                      
        private const string fb_exchange_token = "EAAmIBMRSUhgBABpCq38ewufVyhxcjjoXd0wZCYqHnTZCx33mlK9mpNvFOtzv6WOR8S9ZANpC1INk98afYrjCDuEBZBbS9Uj4Fdc7fyYH6qFr3Js215ZBeN4DZBSVPFcQMXr9VKT4q90kbZAp588rsRiZCNvshcZChPYHm1BrjsYRWK04MvoZCQRiPE";

        private const string AuthenticationUrlFormat =
            "https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id={0}&client_secret={1}&fb_exchange_token={2}";

        static string GetAccessToken(string apiID, string apiSecret, string pageID)
        {
            string accessToken = string.Empty;
            string url = string.Format(AuthenticationUrlFormat, apiID, apiSecret, fb_exchange_token);

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            using (System.IO.Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                String responseString = reader.ReadToEnd();

                dynamic stuff = JsonConvert.DeserializeObject(responseString);

                accessToken = stuff["access_token"];
            }

            if (accessToken.Trim().Length == 0)
                throw new Exception("There is no Access Token");

            return accessToken;
        }

        public static void PostMessage(Bar bar)
        {
            try
            {
                string accessToken = fb_exchange_token;
                FacebookClient facebookClient = new FacebookClient(accessToken);

                dynamic messagePost = new ExpandoObject();
                messagePost.access_token = accessToken;
                messagePost.message = "NEW!🎉 NEW!🎉 NEW!🎉\n\n" +
                                      "Check Out our new bar " + bar.Name + " !🥳\n" +
                                      "Highly recommended for " + bar.Type.Name + " lovers ⭐\n" +
                                      "Come to visit us in " + bar.Address + " 📍";

                string url = string.Format("/{0}/feed", PageID);
                var result = facebookClient.Post(url, messagePost);
            }
            catch( IOException e)
            {
            }
        }

        public static void Post(Bar bar) { }
    }
}
