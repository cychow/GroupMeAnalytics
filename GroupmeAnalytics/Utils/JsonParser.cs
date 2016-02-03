using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Collections.ObjectModel;
using Windows.Data.Json;
using GroupmeAnalytics.Viewmodels;

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

        static public async Task<ObservableCollection<User>> getUsers(string groupID) {

            ObservableCollection<User> Members = new ObservableCollection<User>();

            JsonObject data = await JsonParser.getJsonResponse("/groups/" + groupID, null);

            JsonObject groupData = data.GetNamedObject("response");
            JsonArray members = groupData.GetNamedArray("members");

            string defaultSource = "https://i.groupme.com/300x300.png.e8ec5793a332457096bc9707ffc9ac37.avatar";


            // System.Diagnostics.Debug.WriteLine(data.ToString());
            foreach (var item in members) {
                JsonObject groupObject = item.GetObject();
                string SourceURL = groupObject.GetNamedValue("image_url").ToString().Trim('"');
                Members.Add(new User() {

                    UserPhoto = SourceURL == "null" ? defaultSource : SourceURL,
                    //Source = groupObject.GetNamedString("image_url"),
                    //Text = groupObject.GetNamedString("name")
                    UserNick = groupObject.GetNamedString("nickname"),
                    UserID = groupObject.GetNamedString("user_id")
                }
                );
            }
            // update count?
            System.Diagnostics.Debug.WriteLine("Member count: " + Members.Count);
            return Members;

        }

        static public async Task<ObservableCollection<Message>> getMessages(string groupID, ObservableCollection<User> Members, DateTime since) {

            ObservableCollection<Message> Messages = new ObservableCollection<Message>();
            JsonObject data = await JsonParser.getJsonResponse("/groups/" + groupID, null);
            
            data = await JsonParser.getJsonResponse("/groups/" + groupID + "/messages", null);

            JsonObject messageData = data.GetNamedObject("response");
            JsonArray messages = messageData.GetNamedArray("messages");
            foreach (JsonValue messageVal in messages) {
                JsonObject message = messageVal.GetObject();
                Message currentMessage = new Message();
                try {
                    currentMessage.Text = message.GetNamedString("text");
                } catch (System.Runtime.InteropServices.COMException) {
                    currentMessage.Text = "";
                }
                currentMessage.SenderID = message.GetNamedString("sender_id");
                currentMessage.Sender = message.GetNamedString("name");
                try {
                    currentMessage.SenderPicture = message.GetNamedString("avatar_url");
                } catch (System.Runtime.InteropServices.COMException) {
                    currentMessage.SenderPicture = "";
                }
                //System.Diagnostics.Debug.WriteLine("Message sender avatar url: " + currentMessage.SenderPicture);
                currentMessage.ID = message.GetNamedString("id");
                currentMessage.TimeStamp = message.GetNamedNumber("created_at").ToString();
                currentMessage.Members = Members;
                JsonArray attachments = (message.GetNamedArray("attachments") as JsonArray);
                if (attachments.Count != 0) {
                    try {
                        System.Diagnostics.Debug.WriteLine(attachments.First().GetObject().GetNamedValue("url").ToString().Trim('"'));
                        currentMessage.Attachments = attachments.First().GetObject().GetNamedValue("url").ToString().Trim('"');
                    } catch (System.Exception) {
                        // if it's a mention don't handle it
                    }
                }

                foreach (JsonValue favoriter in message.GetNamedArray("favorited_by")) {
                    if (favoriter.ValueType == JsonValueType.Null) {
                        break;
                    }
                    currentMessage.Favorites.Add(favoriter.ToString().Trim('"'));
                }
                Messages.Insert(0, currentMessage);
            }
            return Messages;
        }

    }
}

