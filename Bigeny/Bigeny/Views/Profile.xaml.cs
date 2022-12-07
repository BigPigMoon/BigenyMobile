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

        private void AvatarChange_Tapped(object sender, EventArgs e)
        {

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