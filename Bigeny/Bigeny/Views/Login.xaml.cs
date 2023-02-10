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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            string email = email_input.Text;
            string password = password_input.Text;
            if (await AuthService.Login(email, password))
            {
                await UsersService.UpdateDeviceToken(CrossFirebasePushNotification.Current.Token);
                Application.Current.MainPage = new Dashboard();
            }
            else
            {
                // TODO: diaplay error of sign in
                await DisplayAlert("", $"Не верный логин или пароль.", "close");
            }
        }

        private void Signup_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Registration();
        }

        private void Visible_Tapped(object sender, EventArgs e)
        {
            password_input.IsPassword = !password_input.IsPassword;
        }
    }
}