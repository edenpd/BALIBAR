using System;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Net;
using BALIBAR.Models;
using Newtonsoft.Json;
using Facebook;

namespace BALIBAR.Handlers
{
    public class FaceBookHandler
    {
        private const string FacebookApiID = "1735489799967249";
        private const string FacebookApiSecret = "d143113a444503cb28c6b28fe3cd5325";

        private const string PageID = "103846034722719";                                      
        private const string fb_exchange_token = "EAAEhHOxwCdkBACoT3cpwyZAsNRuRvaN6mS8fh3CVcMJibBPISqUUZBZAjZARfsGGy6l7NSZCXTL7u5lsmyMumNpDLfbOWPGZAv1ZCbWCZBcimmYWr069TD9BPDXZBbGcGbsyuXujieWDyZCrHUpTZAbSbEZAO6QZBRKpDyFUZC482wZCfodaLPo7zxZAqozBU3uZC6ZC69ZCiUL7KAPyBgQXZBoXUXlFYJs2ZCqa5Uj9upH6jFEIYejzX3wZDZD";

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
            //try
            //{
            //    string accessToken = fb_exchange_token;
            //    FacebookClient facebookClient = new FacebookClient(accessToken);

            //    dynamic messagePost = new ExpandoObject();
            //    messagePost.access_token = accessToken;
            //    messagePost.message = "Check out the new bar: " + bar.Name + "\n\n Located in: " + bar.Address + "\n\n Recommended for " +
            //        bar.Type.Name + "lovers!";

            //    string url = string.Format("/{0}/feed", PageID);
            //    var result = facebookClient.Post(url, messagePost);
            //}
            //catch
            //{

            //}

            //var client = new FacebookClient(fb_exchange_token);

            //dynamic parameters = new ExpandoObject();
            //parameters.title = "test title";
            //parameters.message = "test message";

            //var result = client.Post(PageID + "/feed", parameters);
        }
    }
}