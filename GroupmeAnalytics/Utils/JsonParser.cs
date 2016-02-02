using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Collections.ObjectModel;
using Windows.Data.Json;

namespace GroupmeAnalytics.Utils {
    static class JsonParser {
        static public string authToken = "";
        static public string userID = "";
        static public string baseURL = "https://api.groupme.com/v3";

        // TODO: Lots of exception handling
        static public async Task<JsonObject> getJsonResponse(string URL, Dictionary<string,string> parameters) {
            string paramString = "?";
            if (parameters == null) {
                parameters = new Dictionary<string, string>();
            }
            if (!parameters.ContainsKey("token")) {
                parameters.Add("token", authToken);
            }
            foreach (KeyValuePair<string, string> kvp in parameters) {
                paramString += kvp.Key + "=" + kvp.Value + "&";                
            }
            paramString = paramString.TrimEnd('&');
            System.Diagnostics.Debug.WriteLine("Trying " + baseURL + URL + paramString);
            WebRequest request;
            JsonObject data;
            try {
                request = WebRequest.Create(baseURL + URL + paramString);
                WebResponse response = await request.GetResponseAsync();
                if (((int)(response as HttpWebResponse).StatusCode) != 200) {
                    return null;
                }                
                using (var reader = new StreamReader(response.GetResponseStream())) {
                    var responseText = reader.ReadToEnd();
                    data = JsonObject.Parse(responseText);
                }
            } catch (System.Net.WebException e) {
                return null;
            }
            return data;
        }


    }
}

