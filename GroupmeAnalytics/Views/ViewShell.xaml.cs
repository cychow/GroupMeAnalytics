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

using Prism;
using System.Net;
using System.Collections.ObjectModel;
using Windows.Data.Json;
using GroupmeAnalytics.Viewmodels;
using GroupmeAnalytics.Utils;
//using GroupmeAnalytics.Views;
namespace GroupmeAnalytics.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewShell : Page {
        public ViewShell() {
            this.InitializeComponent();
            // set title bar color
            //Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar.BackgroundColor = Windows.UI.Color.FromArgb(255,0,172,242);
            SplitViewFrame.Navigate(typeof(Views.IntroView));
            refreshGroups();
        }

        /*
        public MainPage() {
            this.InitializeComponent();
            refreshGroups();
        }*/

        public async void refreshGroups() {
            /*
            string baseURL = "https://api.groupme.com/v3";
            WebRequest request = WebRequest.Create(baseURL + "/groups?token=" + accessToken + "&per_page=30");
            WebResponse response = await request.GetResponseAsync();
            JsonObject data;
            using (var reader = new StreamReader(response.GetResponseStream())) {
                var responseText = reader.ReadToEnd();
                data = JsonObject.Parse(responseText);
            }
            */

            Dictionary<string,string> parameters = new Dictionary<string, string>();
            parameters.Add("per_page", "45");

            
            JsonObject data = await JsonParser.getJsonResponse("/groups", parameters);
            if (data == null) {
                return;
            }
            JsonArray groups = data.GetNamedArray("response");
            string defaultSource = "https://i.groupme.com/300x300.png.e8ec5793a332457096bc9707ffc9ac37.avatar";
            // System.Diagnostics.Debug.WriteLine(data.ToString());
            foreach (var item in groups) {
                JsonObject groupObject = item.GetObject();
                string SourceURL = groupObject.GetNamedValue("image_url").ToString().Trim('"');
                (DataContext as MainViewModel).Menu.Add(new MenuItem() {

                    Source = SourceURL == "null" ? defaultSource : SourceURL,
                    //Source = groupObject.GetNamedString("image_url"),
                    //Text = groupObject.GetNamedString("name")
                    Text = groupObject.GetNamedString("name"),
                    ID = groupObject.GetNamedString("id")
                }
                );
            }
            //for (JsonObject group = groups.as
            // add things with this
            //(DataContext as MainViewModel).Menu.Add()
        }

        private void GroupMenu_Expand(object sender, RoutedEventArgs e) {
            mainSplitView.IsPaneOpen = !mainSplitView.IsPaneOpen;
        }

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            // Close the glorious sidebar menu
            mainSplitView.IsPaneOpen = false;

            if (e.AddedItems.Count > 0) {
                var menuItem = e.AddedItems.First() as MenuItem;
                System.Diagnostics.Debug.WriteLine("Clicked on ID " + menuItem.ID);
                // Do stuff to the other pane
                //(DataContext as MainViewModel).selectedItem = menuItem;
                SplitViewFrame.Navigate(typeof(Views.GroupView), menuItem);
            }
        }
    }
}
