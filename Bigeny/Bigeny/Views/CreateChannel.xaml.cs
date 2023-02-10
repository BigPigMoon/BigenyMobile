using Bigeny.Services;
using System;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bigeny.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateChannel : ContentPage
    {
        public string avatar;

        public CreateChannel()
        {
            InitializeComponent();
        }

        private async void NextButton_Clicked(object sender, EventArgs e)
        {
            string name = channel_name_Input.Text;
            string description = description_Input.Text;

            bool ret = await ChannelService.CreateChannel(name, description, avatar);

            if (ret)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                await this.DisplayToastAsync("Что-то пошло не так, попробуете еще раз.");
            }
        }

        private async void OnBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void AvatarButton_Clicked(object sender, EventArgs e)
        {
            avatar = await StorageService.Upload();
            object source = StorageService.Download(avatar);
            if (source.GetType() == typeof(string))
                avatar_Image.Source = (string)StorageService.Download(avatar);
            else
                avatar_Image.Source = (UriImageSource)StorageService.Download(avatar);
        }
    }
}