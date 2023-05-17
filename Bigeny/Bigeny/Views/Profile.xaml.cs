using Bigeny.Models;
using Bigeny.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.Extensions;

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

            User me = (User)Application.Current.Properties["user"];
            var source = StorageService.Download(me.Avatar);
            if (source.GetType() == typeof(string))
                avatar_img.Source = (string)source;
            else
                avatar_img.Source = (UriImageSource)source;

            name_Label.Text = me.Nickname;
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            AuthService.Logout();
            Application.Current.MainPage = new Login();
        }

        private async void Rename_Entry(object sender, EventArgs e)
        {
            User updated = await UsersService.ChangeNickname(rename_Entry.Text);
            if (updated.Nickname == null)
            {
                await this.DisplayToastAsync("Это имя уже занято!");
            }
            else
            {
                rename_Entry.Text = "";
                name_Label.Text = updated.Nickname;
                await this.DisplayToastAsync("Ваше имя изменено.");
                Application.Current.Properties["user"] = updated;
            }
         }

        private async void AvatarChange_Tapped(object sender, EventArgs e)
        {
            var user = await UsersService.UploadAvatar();
            var source = StorageService.Download(user.Avatar);
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