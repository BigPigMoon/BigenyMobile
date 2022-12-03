using Bigeny.Models;
using Bigeny.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Bigeny.Views.Dialogs;

namespace Bigeny.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : TabbedPage
    {
        public class ChannelPreview
        {
            public string Name { get; set; }
            public string LastMessage { get; set; }
            // change string to URI
            public string PhotoUri { get; set; }
        };

        public class PostPreview
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Date { get; set; }
            public string Content { get; set; }
            public int Rate { get; set; }
        };

        List<ChannelPreview> channels;
        List<PostPreview> wallPosts;

        public Dashboard()
        {
            InitializeComponent();
            LoadMyChannelsPreview();
            LoadPosts();
        }

        private void LoadMyChannelsPreview()
        {
            channels = new List<ChannelPreview>
            {
                new ChannelPreview
                {
                    Name = "Пепега анал",
                    LastMessage = "Последнее сообщение",
                    PhotoUri = "avatar.png",
                },
                new ChannelPreview
                {
                    Name = "Пепега анал",
                    LastMessage = "Последнее сообщение",
                    PhotoUri = "avatar.png",
                },
                new ChannelPreview
                {
                    Name = "Пепега анал",
                    LastMessage = "Последнее сообщение",
                    PhotoUri = "avatar.png",
                },
                new ChannelPreview
                {
                    Name = "Пепега анал",
                    LastMessage = "Последнее сообщение",
                    PhotoUri = "avatar.png",
                },
            };
            channels_listView.ItemsSource = channels;
        }

        private void LoadPosts()
        {
            wallPosts = new List<PostPreview>
            {
                new PostPreview
                {
                    Id = 1,
                    Name="Новости киберспорта",
                    Date="вчера в 13:45",
                    Content="Кто-то там кого-то победил и зарботал сколько-то там денег и бла бла бла.",
                    Rate=10,
                },
                new PostPreview
                {
                    Id = 2,
                    Name="Новости киберспорта",
                    Date="вчера в 13:45",
                    Content="Кто-то там кого-то победил и зарботал сколько-то там денег и бла бла бла.",
                    Rate=10,
                },
                new PostPreview
                {
                    Id = 3,
                    Name="Новости киберспорта",
                    Date="вчера в 13:45",
                    Content="Кто-то там кого-то победил и зарботал сколько-то там денег и бла бла бла.",
                    Rate=-20,
                },
                new PostPreview
                {
                    Id = 4,
                    Name="Новости киберспорта",
                    Date="вчера в 13:45",
                    Content="Кто-то там кого-то победил и зарботал сколько-то там денег и бла бла бла.",
                    Rate=-10,
                },
                new PostPreview
                {
                    Id = 5,
                    Name="Новости киберспорта",
                    Date="вчера в 13:45",
                    Content="Кто-то там кого-то победил и зарботал сколько-то там денег и бла бла бла.",
                    Rate=0,
                },
                new PostPreview
                {
                    Id = 6,
                    Name="Новости киберспорта",
                    Date="вчера в 13:45",
                    Content="Кто-то там кого-то победил и зарботал сколько-то там денег и бла бла бла.",
                    Rate=9,
                },
            };
            wallposts_listView.ItemsSource = wallPosts;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }

        private void channels_listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void OnUpButtonTapped(object sender, EventArgs e)
        {
            var id = ((Label)sender).ClassId;
        }

        private void OnDownButtonTapped(object sender, EventArgs e)
        {
            var id = ((Label)sender).ClassId;
        }

        private void wallposts_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}