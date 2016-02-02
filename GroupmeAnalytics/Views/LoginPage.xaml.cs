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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using GroupmeAnalytics.Utils;
using Windows.Data.Json;

namespace GroupmeAnalytics.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page {

        System.Uri LoginURI = new Uri("https://oauth.groupme.com/oauth/authorize?client_id=GnxK6Wj9Gj4q3a25XC0tiy9tpmuHKjZZeD4iW1gBlDk64iyJ");
        internal Frame rootFrame;
        public LoginPage() {
            this.InitializeComponent();
            rootFrame = new Frame();
            AuthView.Navigate(LoginURI);
            
        }

        private async void AuthView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args) {
            // do something to capture the page
            //System.Diagnostics.Debug.WriteLine("Navigating to " + args.Uri);
            if (args.Uri == LoginURI) {
                // do nothing
                return;
            } else if (args.Uri.ToString().Contains("access_token")) {
                // access token found!
                JsonParser.authToken = args.Uri.ToString().Split(new[] { "access_token=" }, StringSplitOptions.None)[1];
                System.Diagnostics.Debug.WriteLine("Found Access Token: " + JsonParser.authToken);
                // get user ID
                JsonObject data = await JsonParser.getJsonResponse("/users/me", null);
                JsonParser.userID = data.GetNamedObject("response").GetNamedString("id");
                System.Diagnostics.Debug.WriteLine("Your ID: " + JsonParser.userID);


                rootFrame.Navigate(typeof(GroupmeAnalytics.Views.ViewShell));
                Window.Current.Content = rootFrame;
                return;
            }
        }
        
    }
}
