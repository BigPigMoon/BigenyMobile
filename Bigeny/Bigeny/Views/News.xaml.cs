﻿using Bigeny.Models;
using Bigeny.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bigeny.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class News : ContentPage
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
            public Color RateColor { get; set; }
        };

        List<PostPreview> wallPosts;
        int UserId;

        public News()
        {
            InitializeComponent();
            //UpdatePosts();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await LoadPosts();
            UserId = ((User)Application.Current.Properties["user"]).Id;
        }

        private void UpdatePosts()
        {
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                var list = Task.Run(async () => await PostService.GetPosts()).Result;

                if (list != null && list.Count != wallPosts.Count)
                {
                    // show "new posts" button
                }

                return true;
            });
        }

        private async Task LoadPosts()
        {
            wallPosts = new List<PostPreview>();

            var posts = await PostService.GetPosts();

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
                    Image=source,
                    Date = beautyDate,
                    Rate = rate.rate,
                    Avatar = avatar,
                    ChannelId = post.channelId,
                };

                if (rate.userRate)
                    p.RateColor = Color.FromHex("#FF1B1C");
                else if (!rate.userRate)
                    p.RateColor = Color.FromHex("#FAFAFA");


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