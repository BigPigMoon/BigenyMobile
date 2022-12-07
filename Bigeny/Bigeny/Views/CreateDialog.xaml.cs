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
            public string PhotoUri { get; set; }
        };

        public List<UserPreview> userPreview;
        public List<UserPreview> selectedItems = new List<UserPreview>();

        public CreateDialog()
        {
            InitializeComponent();
            LoadUserPreview();
        }

        private void LoadUserPreview()
        {
            userPreview = new List<UserPreview>
            {
                new UserPreview { Name = "Вася", PhotoUri = "avatar.png" },
                new UserPreview { Name = "Маша", PhotoUri = "avatar.png" },
                new UserPreview { Name = "Саша", PhotoUri = "avatar.png" },
                new UserPreview { Name = "Саша Белый", PhotoUri = "avatar.png" },
                new UserPreview { Name = "Рома", PhotoUri = "avatar.png" },
            };

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
                userPreview.Find(x => x.Name == item.Name).PhotoUri = "avatar.png";
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