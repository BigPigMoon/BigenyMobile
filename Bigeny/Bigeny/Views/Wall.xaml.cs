using System;
using System.Collections.Generic;
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
        };

        List<PostPreview> wallPosts;

        public Wall()
        {
            InitializeComponent();
            LoadPosts();
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

        private void OnUpButtonTapped(object sender, EventArgs e)
        {
            var id = ((Label)sender).ClassId;
        }

        private void OnDownButtonTapped(object sender, EventArgs e)
        {
            var id = ((Label)sender).ClassId;
        }

        private async void wallposts_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushModalAsync(new ChannelPreview());
        }

        private async void Comment_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Comment());
        }
    }
}