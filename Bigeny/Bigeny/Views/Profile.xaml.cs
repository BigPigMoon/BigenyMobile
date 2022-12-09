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
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
            if (Preferences.Get("DarkTheme", false))
            {

                moon_icon.IsVisible = false;
                sun_icon.IsVisible = true;
            }
            else
            {
                moon_icon.IsVisible= true;
                sun_icon.IsVisible= false;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var source = UsersService.GetAvatar(await UsersService.GetMe());
            if (source.GetType() == typeof(string))
                avatar_img.Source = (string)source;
            else
                avatar_img.Source = (UriImageSource)source;
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            AuthService.Logout();
            Application.Current.MainPage = new Login();
        }

        private void Rename_Entry(object sender, EventArgs e)
        {
            if (rename_Entry.IsEnabled)
            {
            }
            else
            {
            }
            rename_Entry.IsEnabled = !rename_Entry.IsEnabled;
        }

        private async void AvatarChange_Tapped(object sender, EventArgs e)
        {
            await UsersService.UploadAvatar();
            var source = UsersService.GetAvatar(await UsersService.GetMe());
            if (source.GetType() == typeof(string))
                avatar_img.Source = (string)source;
            else
                avatar_img.Source = (UriImageSource)source;
        }

        private void PhoneChange_Tapped(object sender, EventArgs e)
        {

        }

        private void ThemeSwitcher_Tapped(object sender, EventArgs e)
        {
            if (App.Current.UserAppTheme == OSAppTheme.Dark)
            {
                App.Current.UserAppTheme = OSAppTheme.Light;
                Preferences.Set("DarkTheme", false);
                moon_icon.IsVisible = true;
                sun_icon.IsVisible = false;
            }
            else
            {
                App.Current.UserAppTheme = OSAppTheme.Dark;
                Preferences.Set("DarkTheme", true);
                moon_icon.IsVisible = false;
                sun_icon.IsVisible = true;
            }
        }
    }
}