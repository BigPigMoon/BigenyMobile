using Bigeny.Models;
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
    public partial class Dialogs : ContentPage
    {
        public class DialogPreviewModel {
            public string Name { get; set; }
            public string LastMessage { get; set; }
            // change string to URI
            public string PhotoUri { get; set; }
            public bool IsReaded { get; set; }
        };

        public List<DialogPreviewModel> dialogPreviews;

        public Dialogs()
        {
            InitializeComponent();
            LoadDialogsPreview();
        }

        private void LoadDialogsPreview()
        {
            dialogPreviews = new List<DialogPreviewModel>
            {
                new DialogPreviewModel
                {
                    Name = "Пепега",
                    LastMessage = "Привет",
                    PhotoUri = "avatar.png",
                    IsReaded = false,
                },
                new DialogPreviewModel
                {
                    Name = "Пепега2",
                    LastMessage = "Здорова",
                    PhotoUri = "https://s3.amazonaws.com/stickers.wiki/mmdpepe_stickers/163514.512.webp",
                    IsReaded = true,
                },
                new DialogPreviewModel
                {
                    Name = "Пепега3",
                    LastMessage = "Куку",
                    PhotoUri = "avatar.png",
                    IsReaded = false,
                },
                new DialogPreviewModel
                {
                    Name = "Пепега4",
                    LastMessage = "ААААА",
                    PhotoUri = "avatar.png",
                    IsReaded = true,
                },
            };
            dialogs_listView.ItemsSource = dialogPreviews;
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CreateDialog());
        }

        private async void dialogs_listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushModalAsync(new DialogPreview());
        }
    }
}