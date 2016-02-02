using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.Globalization;

namespace GroupmeAnalytics.Viewmodels {
    class Message : Prism.Mvvm.BindableBase {
        private string mesgtext;
        private string nick;
        private string senderid;
        private string senderpicture;
        private string id;
        private ObservableCollection<string> favorites = new ObservableCollection<string>();
        private string timestamp;
        private string attachments;
        private ObservableCollection<User> members;

        public ObservableCollection<User> Members {
            get { return members; }
            set { SetProperty(ref members, value); }
        }

        public string Text {
            get { return mesgtext; }
            set { SetProperty(ref mesgtext, value); }
        }

        public string Sender {
            get { return nick; }
            set { SetProperty(ref nick, value); }
        }

        public string SenderID {
            get { return senderid; }
            set { SetProperty(ref senderid, value); }
        }

        public string SenderPicture {
            get { return senderpicture; }
            set { SetProperty(ref senderpicture, value); }
        }

        public string ID {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        public ObservableCollection<string> Favorites {
            get { return favorites; }
        }

        public string PeopleLiked {
            get {
                string peopleString = "";
                foreach(string userId in Favorites) {
                    //System.Diagnostics.Debug.WriteLine("Looking for user ID " + userId);
                    foreach(User member in Members) {
                        //System.Diagnostics.Debug.WriteLine("User ID: " + member.UserID + " - " + member.UserNick);
                        if (member.UserID.Equals(userId)) {
                            //System.Diagnostics.Debug.WriteLine("Found user of ID" + userId + ": " + member.UserNick);
                            peopleString += member.UserNick + "\n";
                            continue;
                        }
                    }
                }
                return peopleString.TrimEnd('\n');

            }
        }

        public string MessageLiked {
            get { return (favorites.Count > 0) ? "Pink" : "LightGray"; }
        }
        

        public string LikeCount {
            get { return (favorites.Count > 0) ? favorites.Count.ToString() : ""; }
        }

        public string TimeStamp {
            get { return timestamp; }
            set { SetProperty(ref timestamp, value); }
        }

        public string DateTime {
            get {                
                return DateTimeOffset.FromUnixTimeSeconds(Int32.Parse(timestamp)).ToLocalTime().ToString("t");
            }
        }

        public string Attachments {
            get { return attachments; }
            set { SetProperty(ref attachments, value); }
        }



    }
}
