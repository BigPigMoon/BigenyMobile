using Bigeny.Models;
using Bigeny.Services;
using Bigeny.Views;
using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bigeny
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new Loading();
            //MainPage = new CreateDialog();
            //MainPage = new DialogPreview();
            //MainPage = new ChannelPreview();
        }

        protected override void OnStart()
        {
            if (Preferences.Get("DarkTheme", false))
                App.Current.UserAppTheme = OSAppTheme.Dark;
            else
                App.Current.UserAppTheme = OSAppTheme.Light;
            CrossFirebasePushNotification.Current.OnTokenRefresh += OnRefreshDeviceToken;
        }

        private async void OnRefreshDeviceToken(object source, FirebasePushNotificationTokenEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine($"TOKEN: {CrossFirebasePushNotification.Current.Token}");
            await UsersService.UpdateDeviceToken(CrossFirebasePushNotification.Current.Token);
        }

        protected override void OnSleep()
        {
            CrossFirebasePushNotification.Current.OnTokenRefresh += OnRefreshDeviceToken;
        }

        protected override void OnResume()
        {
            if (Preferences.Get("DarkTheme", false))
                App.Current.UserAppTheme = OSAppTheme.Dark;
            else
                App.Current.UserAppTheme = OSAppTheme.Light;
            CrossFirebasePushNotification.Current.OnTokenRefresh += OnRefreshDeviceToken;
        }
    }
}
