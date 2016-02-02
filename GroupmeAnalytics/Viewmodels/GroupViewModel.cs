using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;


namespace GroupmeAnalytics.Viewmodels {
    class GroupViewModel : ViewModelBase {
        private ObservableCollection<User> members = new ObservableCollection<User>();
        private ObservableCollection<Message> messages = new ObservableCollection<Message>();
        private string membercount;


        public String groupID { get; set; }
        public String groupName { get; set; }
        public String groupPicture { get; set; }


        public string numUsers {
            get { return membercount; }
            set { SetProperty(ref membercount, value); }
        }
        public GroupViewModel() {

        }
        public ObservableCollection<User> Members {
            get { return members; }
        }

        public ObservableCollection<Message> Messages {
            get { return messages; }
        }
    }
}
