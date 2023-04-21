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

            rename_Entry.Text = me.Nickname;
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            AuthService.Logout();
            Application.Current.MainPage = new Login();
        }

        private async void Rename_Entry(object sender, EventArgs e)
        {
            // TODO: if user doesn't change anything
            if (!rename_Entry.IsReadOnly)
            {
                User updated = await UsersService.ChangeNickname(rename_Entry.Text);
                if (updated == null)
                {
                    rename_Entry.Text = ((User)Application.Current.Properties["user"]).Nickname;
                    await this.DisplayToastAsync("This name is already hosted");
                }
                else
                {
                    rename_Entry.Text = updated.Nickname;
                    await this.DisplayToastAsync("Your name was changed");
                }
            }
            rename_Entry.IsReadOnly = !rename_Entry.IsReadOnly;
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