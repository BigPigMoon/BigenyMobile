using Bigeny.Models;
using Bigeny.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Bigeny.Views.Channels;

namespace Bigeny.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Channels : ContentPage
    {
        public class ChannelPreviewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string LastMessage { get; set; }
            // change string to URI
            public object PhotoUri { get; set; }
        };

        List<ChannelPreviewModel> channels;

        public Channels()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadMyChannelsPreview();
        }

        private async Task LoadMyChannelsPreview()
        {
            var list = await ChannelService.GetSubsChannels();
            channels = new List<ChannelPreviewModel>();

            foreach (var channel in list)
            {
                var avatar = StorageService.Download(channel.avatar);

                string lastMessage = channel.posts.Count == 0 ? "" : channel.posts[0].content.Length >= 25 ? channel.posts[0].content.Substring(0, 25) + "..." : channel.posts[0].content;

                channels.Add(new ChannelPreviewModel
                {
                    Id = channel.id,
                    Name = channel.name,
                    PhotoUri = avatar,
                    LastMessage = lastMessage
                });
            }

            channels_listView.ItemsSource = channels;
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CreateChannel());
        }

        private async void channels_listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (ChannelPreviewModel)e.Item;
            await Navigation.PushModalAsync(new ChannelPreview(item.Id));
        }
    }
}