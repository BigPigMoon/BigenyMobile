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
    public partial class Comment : ContentPage
    {
        public class PostPreview
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Date { get; set; }
            public string PhotoUri { get; set; }
            public string Content { get; set; }
        };

        List<PostPreview> comments;

        public Comment()
        {
            InitializeComponent();
            LoadComments();
        }

        private void LoadComments()
        {
            comments = new List<PostPreview>
            {
                new PostPreview
                {
                    Id = 1,
                    Name="Новости киберспорта",
                    Date="вчера в 13:45",
                    PhotoUri="avatar.png",
                    Content="Кто-то там кого-то победил и зарботал сколько-то там денег и бла бла бла.",
                },
                new PostPreview
                {
                    Id = 2,
                    Name="Новости киберспорта",
                    Date="вчера в 13:45",
                    PhotoUri="avatar.png",
                    Content="Заебись",
                },
                new PostPreview
                {
                    Id = 3,
                    Name="Новости киберспорта",
                    Date="вчера в 13:45",
                    PhotoUri="avatar.png",
                    Content="Кто-то там кого-то победил и зарботал сколько-то там денег и бла бла бла.",
                },
                new PostPreview
                {
                    Id = 4,
                    Name="Новости киберспорта",
                    Date="вчера в 13:45",
                    PhotoUri="avatar.png",
                    Content="Кто-то там кого-то победил и зарботал сколько-то там денег и бла бла бла.",
                },
                new PostPreview
                {
                    Id = 5,
                    Name="Новости киберспорта",
                    Date="вчера в 13:45",
                    PhotoUri="avatar.png",
                    Content="Кто-то там кого-то победил и зарботал сколько-то там денег и бла бла бла.",
                },
                new PostPreview
                {
                    Id = 6,
                    Name="Новости киберспорта",
                    Date="вчера в 13:45",
                    PhotoUri="avatar.png",
                    Content="Кто-то там кого-то победил и зарботал сколько-то там денег и бла бла бла.",
                },
            };
            comments_listView.ItemsSource = comments;
        }

        private async void OnBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void Sender_Tapped(object sender, EventArgs e)
        {

        }
    }
}