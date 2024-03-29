﻿using Bigeny.Models;
using Bigeny.Services;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bigeny.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Loading : ContentPage
    {
        public Loading()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            try
            {
                User user = await UsersService.GetMe();
                Application.Current.Properties["user"] = user;
                if (user != null)
                    Application.Current.MainPage = new Dashboard();
                else
                    Application.Current.MainPage = new Login();
            }
            catch
            {
                await this.DisplayToastAsync("Проблемы с интернет соединением");
                Application.Current.MainPage = new Login();
            }
        }
    }
}