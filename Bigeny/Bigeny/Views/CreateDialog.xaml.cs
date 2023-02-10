using Bigeny.Models;
using Bigeny.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Bigeny.Views.CreateDialog;

namespace Bigeny.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateDialog : ContentPage
    {
        public class UserPreview
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public object PhotoUri { get; set; }
            public string Avatar { get; set; }
        };

        public List<UserPreview> userPreview;
        private List<User> users;
        public List<UserPreview> selectedItems = new List<UserPreview>();
        public string avatar = null;

        public CreateDialog()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            users = await UsersService.GetUsers();
            LoadUserPreview();
        }

        private void LoadUserPreview()
        {
            // i do this changes
            userPreview = new List<UserPreview>();

            foreach (User user in users)
            {
                var source = StorageService.Download(user.Avatar);
                userPreview.Add(new UserPreview { Name = user.Nickname, PhotoUri = source, Id=user.Id, Avatar = user.Avatar });
            }

            users_listView.ItemsSource = userPreview;
            users_listView.HeightRequest = 100 * userPreview.Count;
        }

        private async void OnBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void users_listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (UserPreview)e.Item;
            if (userPreview.Find(x => x.Name == item.Name).PhotoUri != "done.png")
            {
                userPreview.Find(x => x.Name == item.Name).PhotoUri = "done.png";
                selectedItems.Add(item);
            }
            else
            {
                userPreview.Find(x => x.Name == item.Name).PhotoUri = StorageService.Download(users.Find(x => x.Nickname == item.Name).Avatar);
                selectedItems.Remove(item);
            }

            chat_name_frame.IsVisible = selectedItems.Count > 1;
            Search();
        }

        private void SearchLabel_Tapped(object sender, EventArgs e)
        {
            Search();
        }

        private void userName_searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            string findingString = userName_searchBar.Text;
            List<UserPreview> userPreviews;

            if (string.IsNullOrEmpty(findingString))
                userPreviews = userPreview.ToList();
            else
                userPreviews = userPreview.FindAll(x => x.Name.IndexOf(findingString, StringComparison.OrdinalIgnoreCase) > -1);

            users_listView.ItemsSource = userPreviews;
            users_listView.HeightRequest = userPreviews.Count * 100;
        }

        private async void NextButton_Clicked(object sender, EventArgs e)
        {
            List<int> users = new List<int>();
            for (int i = 0; i < selectedItems.Count; i++)
            {
                users.Add(selectedItems[i].Id);
            }

            if (selectedItems.Count == 0)
            {
                await this.DisplayToastAsync("Выберети пользователя или пользователей");
                return;
            }

            string name;
            if (selectedItems.Count >= 2)
            {
                if (chat_name_Entry.Text.Length == 0)
                {
                    await this.DisplayToastAsync("Укажите имя пользователя");
                    return;
                }
                name = chat_name_Entry.Text;
            }
            else
            {
                name = selectedItems[0].Name;
            }

            if (avatar == null)
            {
                if (selectedItems.Count < 2)
                    avatar = selectedItems[0].Avatar;
            }

            bool result = await MessagesService.CreateDialog(users, name, avatar);
            if (!result)
                await this.DisplayToastAsync("This dialog not unique!");
            else
                await Navigation.PopModalAsync();
        }

        private async void avatarImage_Clicked(object sender, EventArgs e)
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