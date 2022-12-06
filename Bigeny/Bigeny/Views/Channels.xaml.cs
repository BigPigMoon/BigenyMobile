using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bigeny.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Channels : ContentPage
    {
        public class ChannelPreviewModel
        {
            public string Name { get; set; }
            public string LastMessage { get; set; }
            // change string to URI
            public string PhotoUri { get; set; }
        };

        List<ChannelPreviewModel> channels;

        public Channels()
        {
            InitializeComponent();
            LoadMyChannelsPreview();
        }

        private void LoadMyChannelsPreview()
        {
            channels = new List<ChannelPreviewModel>
            {
                new ChannelPreviewModel
                {
                    Name = "Пепега анал",
                    LastMessage = "Последнее сообщение",
                    PhotoUri = "avatar.png",
                },
                new ChannelPreviewModel
                {
                    Name = "Пепега анал",
                    LastMessage = "Последнее сообщение",
                    PhotoUri = "avatar.png",
                },
                new ChannelPreviewModel
                {
                    Name = "Пепега анал",
                    LastMessage = "Последнее сообщение",
                    PhotoUri = "avatar.png",
                },
                new ChannelPreviewModel
                {
                    Name = "Пепега анал",
                    LastMessage = "Последнее сообщение",
                    PhotoUri = "avatar.png",
                },
            };
            channels_listView.ItemsSource = channels;
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CreateChannel());
        }

        private async void channels_listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushModalAsync(new ChannelPreview());
        }
    }
}