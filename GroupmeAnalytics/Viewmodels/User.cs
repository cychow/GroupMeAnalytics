using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupmeAnalytics.Viewmodels {
    // Each user has a user id, photo id, and nickname

    class User : Prism.Mvvm.BindableBase {
        private string userid;
        private string userphoto;
        private string usernick;

        public string UserID {
            get { return userid; }
            set { SetProperty(ref userid, value); }
        }
        public string UserPhoto {
            get { return userphoto; }
            set { SetProperty(ref userphoto, value); }
        }
        public string UserNick {
            get { return usernick; }
            set { SetProperty(ref usernick, value); }
        }

    }
}
