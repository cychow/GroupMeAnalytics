using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

using Prism;
using System.Net;
using System.Collections.ObjectModel;
using Windows.Data.Json;
using GroupmeAnalytics.Viewmodels;
using GroupmeAnalytics.Utils;

namespace GroupmeAnalytics.Views {
    public sealed partial class GroupView : Page {

        private MenuItem groupObject;
        public GroupView() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            groupObject = e.Parameter as MenuItem;
            loadData();
        }

        private async void loadData() {
            (DataContext as GroupViewModel).groupID = groupObject.ID;
            System.Diagnostics.Debug.WriteLine("Loading group ID " + (DataContext as GroupViewModel).groupID);
            (DataContext as GroupViewModel).groupName = groupObject.Text;
            (DataContext as GroupViewModel).groupPicture = groupObject.Source;

            // populate user list
            // this really needs its own helper function
            

            JsonObject data = await JsonParser.getJsonResponse("/groups/" + groupObject.ID, null);

            JsonObject groupData = data.GetNamedObject("response");
            JsonArray members = groupData.GetNamedArray("members");

            string defaultSource = "https://i.groupme.com/300x300.png.e8ec5793a332457096bc9707ffc9ac37.avatar";
            

            // System.Diagnostics.Debug.WriteLine(data.ToString());
            foreach (var item in members) {
                JsonObject groupObject = item.GetObject();
                string SourceURL = groupObject.GetNamedValue("image_url").ToString().Trim('"');
                (DataContext as GroupViewModel).Members.Add(new User() {

                    UserPhoto = SourceURL == "null" ? defaultSource : SourceURL,
                    //Source = groupObject.GetNamedString("image_url"),
                    //Text = groupObject.GetNamedString("name")
                    UserNick = groupObject.GetNamedString("nickname"),
                    UserID = groupObject.GetNamedString("user_id")
                }
                );
            }
            // update count?
            System.Diagnostics.Debug.WriteLine("Member count: " + (DataContext as GroupViewModel).Members.Count);
            (DataContext as GroupViewModel).numUsers = "Members (" + (DataContext as GroupViewModel).Members.Count + ")";

            // since we still have parser let's parse some more shit
            data = await JsonParser.getJsonResponse("/groups/" + groupObject.ID + "/messages", null);

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
                currentMessage.Members = (DataContext as GroupViewModel).Members;
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
                (DataContext as GroupViewModel).Messages.Insert(0,currentMessage);                
            }
            // Add a reference to the members to the messages            
            // Now scroll the listbox to the bottom
            Messages.ScrollIntoView(Messages.Items.Last());
            
        }
        

        
        
    }

    public class StripedListView : ListView {
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
            base.PrepareContainerForItemOverride(element, item);
            var listviewitem = element as ListViewItem;
            if (listviewitem != null) {
                var index = IndexFromContainer(element);
                if ((DataContext as GroupViewModel).Messages.ElementAt(index).SenderID == JsonParser.userID) {
                    listviewitem.Background = new SolidColorBrush(Windows.UI.Colors.Azure);
                } else {
                    listviewitem.Background = new SolidColorBrush(Windows.UI.Colors.White);
                }
            }
        }
    }
}