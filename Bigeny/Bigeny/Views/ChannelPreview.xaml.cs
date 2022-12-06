using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Bigeny.Views.Wall;

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
            public int Rate { get; set; }
        };

        List<PostPreview> wallPosts;

        public ChannelPreview()
        {
            InitializeComponent();
            LoadPost();
        }

        private void LoadPost()
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
            posts_listView.ItemsSource = wallPosts;
        }

        private async void OnBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void OnUpButtonTapped(object sender, EventArgs e)
        {
            var id = ((Label)sender).ClassId;
        }

        private void OnDownButtonTapped(object sender, EventArgs e)
        {
            var id = ((Label)sender).ClassId;
        }

        private async void Comment_Tapped(object sender, EventArgs e)
        {
            var id = ((Label)sender).ClassId;
            await Navigation.PushModalAsync(new Comment());
        }

        private void Sender_Tapped(object sender, EventArgs e)
        {

        }
    }
}