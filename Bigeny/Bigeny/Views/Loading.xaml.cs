using Bigeny.Models;
using Bigeny.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
            User users = await UsersService.GetMe();
            if (users != null)
                Application.Current.MainPage = new Dashboard();
            else
                Application.Current.MainPage = new Login();
        }
    }
}