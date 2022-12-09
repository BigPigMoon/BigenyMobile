using Bigeny.Models;
using Bigeny.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            public string Name { get; set; }
            public object PhotoUri { get; set; }
        };

        public List<UserPreview> userPreview;
        private List<Users> users;
        public List<UserPreview> selectedItems = new List<UserPreview>();

        public CreateDialog()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            users = await UsersService.GetUsersNotme();
            LoadUserPreview();
        }

        private void LoadUserPreview()
        {
            // i do this changes
            userPreview = new List<UserPreview>();

            foreach (Users user in users)
            {
                var source = UsersService.GetAvatar(user);
                userPreview.Add(new UserPreview { Name = user.Nickname, PhotoUri = source });
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
                userPreview.Find(x => x.Name == item.Name).PhotoUri = UsersService.GetAvatar(users.Find(x => x.Nickname == item.Name));
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
            await Navigation.PopModalAsync();
        }
    }
}