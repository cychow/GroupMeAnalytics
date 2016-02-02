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

// Splashscreen stuff
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using GroupmeAnalytics.Views;
using Windows.Security.Authentication.Web;

namespace GroupmeAnalytics.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// borrowed from https://msdn.microsoft.com/en-us/library/windows/apps/mt187309.aspx
    /// </summary>
    public partial class ExtendedSplash : Page {
        internal Rect splashImageRect;
        private SplashScreen splash;
        internal bool dismissed = false;
        internal Frame rootFrame;
        public ExtendedSplash(SplashScreen splashscreen, bool loadState) {
            this.InitializeComponent();
            //Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar.BackgroundColor = Windows.UI.Color.FromArgb(255, 0, 172, 242);
            Window.Current.SizeChanged += new WindowSizeChangedEventHandler(ExtendedSplash_OnResize);

            splash = splashscreen;
            if (splash != null) {
                splash.Dismissed += new TypedEventHandler<SplashScreen, object>(DismissedEventHandler);

                splashImageRect = splash.ImageLocation;
                PositionImage();

                //PositionRing();

                PositionLoginButton();
            }
            rootFrame = new Frame();
        }

        void PositionImage() {
            extendedSplashImage.SetValue(Canvas.LeftProperty, splashImageRect.X);
            extendedSplashImage.SetValue(Canvas.TopProperty, splashImageRect.Y);
            extendedSplashImage.Height = splashImageRect.Height;
            extendedSplashImage.Width = splashImageRect.Width;

        }

        //void PositionRing() {
        //    splashProgressRing.SetValue(Canvas.LeftProperty, splashImageRect.X + (splashImageRect.Width * 0.5) - (splashProgressRing.Width * 0.5));
        //    splashProgressRing.SetValue(Canvas.TopProperty, (splashImageRect.Y + splashImageRect.Height + splashImageRect.Height * 0.1));
        //}

        void PositionLoginButton() {
            LoginButton.SetValue(Canvas.LeftProperty, splashImageRect.X + (splashImageRect.Width * 0.5) - (LoginButton.Width * 0.5));
            LoginButton.SetValue(Canvas.TopProperty, (splashImageRect.Y + splashImageRect.Height + splashImageRect.Height * 0.1));
        }

        void DismissedEventHandler(SplashScreen sender, object e) {
            dismissed = true;
        


        }

        void DismissExtendedSplash() {
            //Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar.BackgroundColor = Windows.UI.Color.FromArgb(255, 255, 255, 255);
            rootFrame.Navigate(typeof(GroupmeAnalytics.Views.LoginPage));
            Window.Current.Content = rootFrame;
        }

        void ExtendedSplash_OnResize(Object sender, WindowSizeChangedEventArgs e) {
            if (splash != null) {
                splashImageRect = splash.ImageLocation;
                PositionImage();
                //PositionRing();
                PositionLoginButton();
            }
        }

        void LoginButton_Click(object sender, RoutedEventArgs e) {
            // do OAuth stuff here
            // or... add a new window, whatever
            // navigate to a new view, then, fuck it
            //System.Uri url = new Uri("https://oauth.groupme.com/oauth/authorize?client_id=GnxK6Wj9Gj4q3a25XC0tiy9tpmuHKjZZeD4iW1gBlDk64iyJ");
            //WebView authView = new WebView(WebViewExecutionMode.SeparateThread);
            
            //authView.Navigate(url);

            DismissExtendedSplash();
        }


    }
}
