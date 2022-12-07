using Bigeny.Models;
using Bigeny.Services;
using Bigeny.Views;
using System;
using System.Collections.Generic;
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

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
