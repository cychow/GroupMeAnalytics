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
using WinRTXamlToolkit.Controls.Extensions;

namespace GroupmeAnalytics.Views {
    public sealed partial class GroupView : Page {

        private MenuItem groupObject;
        public GroupView() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            groupObject = e.Parameter as MenuItem;
            loadData();
            var MessagesScrollbar = Messages.GetFirstDescendantOfType<ScrollViewer>();
            var scrollbars = MessagesScrollbar.GetDescendantsOfType<ScrollBar>().ToList();
            var VerticalBar = scrollbars.FirstOrDefault(x => x.Orientation == Orientation.Vertical);
            if (VerticalBar != null) {
                System.Diagnostics.Debug.WriteLine("Successfully registered");
                VerticalBar.Scroll += MessageBar_OnScroll;
            } else {
                System.Diagnostics.Debug.WriteLine("Failed to find vertical bar");
            }
        }

        private async void loadData() {
            (DataContext as GroupViewModel).groupID = groupObject.ID;
            System.Diagnostics.Debug.WriteLine("Loading group ID " + (DataContext as GroupViewModel).groupID);
            (DataContext as GroupViewModel).groupName = groupObject.Text;
            (DataContext as GroupViewModel).groupPicture = groupObject.Source;

            // populate user list
            (DataContext as GroupViewModel).Members = await JsonParser.getUsers(groupObject.ID);
            (DataContext as GroupViewModel).numUsers = "Members (" + (DataContext as GroupViewModel).Members.Count + ")";


            (DataContext as GroupViewModel).Messages = await JsonParser.getMessages(groupObject.ID, (DataContext as GroupViewModel).Members, new DateTime(2016,1,1,0,0,0));
            
            // Add a reference to the members to the messages            
            // Now scroll the listbox to the bottom
            Messages.ScrollIntoView(Messages.Items.Last());
            
        }

        private void MessageBar_OnScroll(object sender, ScrollEventArgs e) {
            if (e.ScrollEventType != ScrollEventType.EndScroll) return;
            if ((sender as ScrollBar) == null) return;

            System.Diagnostics.Debug.WriteLine(e.NewValue.ToString());
            if (e.NewValue <= (sender as ScrollBar).Minimum) {
                System.Diagnostics.Debug.WriteLine("Top of Messages!");

            }
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