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
        }

        protected override void OnStart()
        {
            if (Preferences.Get("DarkTheme", false))
                App.Current.UserAppTheme = OSAppTheme.Dark;
            else
                App.Current.UserAppTheme = OSAppTheme.Light;
        }

        protected override void OnResume()
        {
            if (Preferences.Get("DarkTheme", false))
                App.Current.UserAppTheme = OSAppTheme.Dark;
            else
                App.Current.UserAppTheme = OSAppTheme.Light;
        }
    }
}
