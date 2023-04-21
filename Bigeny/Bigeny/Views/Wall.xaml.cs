using Bigeny.Models;
using Bigeny.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bigeny.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Wall : ContentPage
    {
        public class PostPreview
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Date { get; set; }
            public string Content { get; set; }
            public int Rate { get; set; }
            public object Avatar { get; set; }
            public object Image { get; set; }
            public int ChannelId { get; set; }
            public Color RateColorUp { get; set; }
            public Color RateColorDown { get; set; }
            public Color RateColor { get; set; }
        };

        List<PostPreview> wallPosts;
        int UserId;

        public Wall()
        {
            InitializeComponent();
            //UpdatePosts();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            UserId = ((User)Application.Current.Properties["user"]).Id;
            await LoadPosts();
        }

        private void UpdatePosts()
        {
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                Task.Run(async () => await LoadPosts());

                return true;
            });
        }

        private async Task LoadPosts()
        {
            wallPosts = new List<PostPreview>();

            var posts = await PostService.GetPostFromSubscribesChannel();

            if (posts == null)
            {
                await this.DisplayToastAsync("Что-то пошло не так.");
                return;
            }

            foreach (var post in posts)
            {
                var channel = await ChannelService.GetChannel(post.channelId);

                string beautyDate = UtilService.GetBeautyDate(post.createdAt.ToLocalTime());
                PostRate rate = await PostService.GetPostRate(post.id);
                var avatar = StorageService.Download(channel.avatar);
                var source = StorageService.Download(post.image);
                if (source.GetType() == typeof(string) && (string)source == "notfound.png") source = null;

                var p = new PostPreview
                {
                    Id = post.id,
                    Name = channel.name,
                    Content = post.content,
                    Image = source,
                    Date = beautyDate,
                    Rate = rate.rate,
                    Avatar = avatar,
                    ChannelId = post.channelId,
                };

                if (Preferences.Get("DarkTheme", false))
                {
                    p.RateColorUp = Color.FromHex("#FAFAFA");
                    p.RateColorDown = Color.FromHex("#FAFAFA");
                    p.RateColor = Color.FromHex("#FAFAFA");
                }
                else
                {
                    p.RateColorUp = Color.FromHex("#210124");
                    p.RateColorDown = Color.FromHex("#210124");
                    p.RateColor = Color.FromHex("#210124");
                }

                if (rate.userRate > 0)
                    p.RateColorUp = Color.FromHex("#06BA63");
                else if (rate.userRate < 0)
                    p.RateColorDown = Color.FromHex("#FF1B1C");

                if (rate.rate > 0)
                    p.RateColor = Color.FromHex("#06BA63");
                else if (rate.rate < 0)
                    p.RateColor = Color.FromHex("#FF1B1C");


                wallPosts.Add(p);
            }

            wallposts_listView.ItemsSource = wallPosts;
        }

        private async void OnUpButtonTapped(object sender, EventArgs e)
        {
            var id = Int32.Parse(((Label)sender).ClassId);
            var post = await PostService.GetPost(id);
            var channel = await ChannelService.GetChannel(post.channelId);

            if (UserId == channel.ownerId)
            {
                await this.DisplayToastAsync("Нельзя лайкать самого себя.");
                return;
            }

            await PostService.SetPostRate(id, true);
            await LoadPosts();
        }

        private async void OnDownButtonTapped(object sender, EventArgs e)
        {
            var id = Int32.Parse(((Label)sender).ClassId);
            var post = await PostService.GetPost(id);
            var channel = await ChannelService.GetChannel(post.channelId);

            if (UserId == channel.ownerId)
            {
                await this.DisplayToastAsync("Нельзя дизлайкать самого себя.");
                return;
            }

            await PostService.SetPostRate(id, false);
            await LoadPosts();
        }

        private async void wallposts_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (PostPreview)e.Item;
            await Navigation.PushModalAsync(new ChannelPreview(item.ChannelId));
        }
    }
}