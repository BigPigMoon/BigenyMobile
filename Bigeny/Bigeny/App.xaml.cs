using Bigeny.Models;
using Bigeny.Services;
using Bigeny.Views;
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
            //MainPage = new DialogPreview();
            //MainPage = new ChannelPreview();
        }

        protected override void OnStart()
        {
            if (Preferences.Get("DarkTheme", false))
                App.Current.UserAppTheme = OSAppTheme.Dark;
            else
                App.Current.UserAppTheme = OSAppTheme.Light;
        }

        protected override void OnSleep()
        {
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
