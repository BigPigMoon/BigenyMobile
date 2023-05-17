using Bigeny.Services;
using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bigeny.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration : ContentPage
    {
        public Registration()
        {
            InitializeComponent();
        }

        private async void Registration_Clicked(object sender, EventArgs e)
        {
            string email = email_input.Text;
            string password = password_input.Text;
            string nickname = nickname_input.Text;
            if (await AuthService.Register(email, password, nickname))
            {
                await UsersService.UpdateDeviceToken(CrossFirebasePushNotification.Current.Token);
                Application.Current.Properties["user"] = await UsersService.GetMe();
                Application.Current.MainPage = new Dashboard();
            }
            else
            {
                await DisplayAlert("clicked!", $"Что-то пошло не так.", "close");
            }
        }

        private void SignIn_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();
        }

        private void Visible_Tapped(object sender, EventArgs e)
        {
            password_input.IsPassword = !password_input.IsPassword;
        }
    }
}