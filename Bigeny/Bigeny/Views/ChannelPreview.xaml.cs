using Bigeny.Models;
using Bigeny.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bigeny.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChannelPreview : ContentPage
    {
        public class PostPreview
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Date { get; set; }
            public string Content { get; set; }
            public object Image { get; set; }
            public int Rate { get; set; }
            public Color RateColorUp { get; set; }
            public Color RateColorDown { get; set; }
            public Color RateColor { get; set; }
        };

        List<PostPreview> wallPosts;
        Channel Channel;
        int Id;
        int UserId;
        bool UserSubbed = false;
        string messageImg = null;

        public ChannelPreview(int id)
        {
            InitializeComponent();
            Id = id;
            //UpdatePosts();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Channel = await ChannelService.GetChannel(Id);

            var source = StorageService.Download(Channel.avatar);
            if (source.GetType() == typeof(string))
                channelAvatar_Image.Source = (string)source;
            else
                channelAvatar_Image.Source = (UriImageSource)source;
            channelName_Label.Text = Channel.name;

            var user = (User)Application.Current.Properties["user"];
            UserId = user.Id;
            if (UserId != Channel.ownerId)
            {
                sender_input_view.IsVisible = false;
                subButton_view.IsVisible = true;
            }
            else
            {
                sender_input_view.IsVisible = true;
                subButton_view.IsVisible = false;
            }

            var subs = await ChannelService.GetSubsChannels();

            foreach(var channel in subs)
            {
                if (channel.id == Id)
                {
                    sub_Button.Text = "Отписаться";
                    UserSubbed = true;
                }
            }

            await LoadPost();
        }

        private void UpdatePosts()
        {
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                Task.Run(async () => await LoadPost());

                return true;
            });
        }

        private async Task LoadPost()
        {
            wallPosts = new List<PostPreview>();

            var posts = await PostService.GetPostsFromChannel(Channel.id);

            foreach (var post in posts)
            {
                string beautyDate = UtilService.GetBeautyDate(post.createdAt.ToLocalTime());

                PostRate rate = await PostService.GetPostRate(post.id);

                var source = StorageService.Download(post.image);
                if (source.GetType() == typeof(string) && (string)source == "notfound.png") source = null;

                var p = new PostPreview
                {
                    Id = post.id,
                    Content = post.content,
                    Image = source,
                    Date = beautyDate,
                    Rate = rate.rate,
                };

                if (rate.userRate)
                    p.RateColor = Color.FromHex("#FF1B1C");
                else if (!rate.userRate)
                    p.RateColor = Color.FromHex("#FAFAFA");


                wallPosts.Add(p);
            }
            
            posts_listView.ItemsSource = wallPosts;
        }

        private async void OnBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void OnUpButtonTapped(object sender, EventArgs e)
        {
            if (UserId == Channel.ownerId)
            {
                await this.DisplayToastAsync("Нельзя лайкать самого себя.");
                return;
            }

            var id = Int32.Parse(((Label)sender).ClassId);
            await PostService.SetPostRate(id, true);
            await LoadPost();
        }

        private async void OnDownButtonTapped(object sender, EventArgs e)
        {
            if (UserId == Channel.ownerId)
            {
                await this.DisplayToastAsync("Нельзя дизлайкать самого себя.");
                return;
            }

            var id = Int32.Parse(((Label)sender).ClassId);
            await PostService.SetPostRate(id, false);
            await LoadPost();
        }

        private async void Sender_Tapped(object sender, EventArgs e)
        {
            sender_Button.IsEnabled = false;
            if (sender_input.Text != null && sender_input.Text.Length > 0)
            {
                await PostService.CreatePost(sender_input.Text, messageImg, Id);
                await LoadPost();
                messageImg = null;
                pin_Button.Text = "\ue226";
                sender_input.Text = "";
            }
            sender_Button.IsEnabled = true;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            sub_Button.IsEnabled = false;
            if (UserSubbed)
                sub_Button.Text = "Подпиаться";
            else
                sub_Button.Text = "Отписаться";
            UserSubbed = !UserSubbed;
            await ChannelService.Subscribe(Id);
            sub_Button.IsEnabled = true;
        }

        private async void Pin_Tapped(object sender, EventArgs e)
        {
            if (messageImg == null)
            {
                messageImg = await StorageService.Upload();
                pin_Button.Text = "\ue5cd";
            }
            else
            {
                messageImg = null;
                pin_Button.Text = "\ue226";
            }
        }
    }
}