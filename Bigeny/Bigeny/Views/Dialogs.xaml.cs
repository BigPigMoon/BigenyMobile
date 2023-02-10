using Bigeny.Models;
using Bigeny.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bigeny.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dialogs : ContentPage
    {
        public class DialogPreviewModel {
            public int Id { get; set; }
            public string Name { get; set; }
            public string LastMessage { get; set; }
            public object PhotoUri { get; set; }
            public bool IsReaded { get; set; }
        };

        public List<DialogPreviewModel> dialogPreviews;
        public List<Dialog> dialogs;

        public Dialogs()
        {
            InitializeComponent();
            // TODO: do update later
            //UpdateDialogs();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            dialogs = await MessagesService.GetDialogs();
            LoadDialogsPreview();
        }

        private void UpdateDialogs()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                var newDialogs = Task.Run(async () => await MessagesService.GetDialogs()).Result;
                dialogPreviews = new List<DialogPreviewModel>();
                foreach (var dialog in dialogs)
                {
                    var avatar = StorageService.Download(dialog.avatar);

                    string lastMessage = dialog.messages.Count == 0 ? "" : dialog.messages[0].content.Length >= 25 ? dialog.messages[0].content.Substring(0, 25) + "..." : dialog.messages[0].content;

                    dialogPreviews.Add(new DialogPreviewModel
                    {
                        Id = dialog.id,
                        Name = dialog.name,
                        PhotoUri = avatar,
                        IsReaded = !dialog.isReaded,
                        LastMessage = lastMessage
                    });
                }

                if (dialogPreviews == null || dialogPreviews.Count == 0 || newDialogs.Count == 0) return true;
                bool changed = false;

                foreach (var dialog in newDialogs)
                {
                    var di = dialogPreviews.Find(d => d.Id == dialog.id);
                    if (di.LastMessage.Length == 0) continue;
                    if (di.LastMessage != dialog.messages[0].content)
                    {
                        di.LastMessage= dialog.messages[0].content;
                        di.IsReaded = false;
                        changed = true;
                    }
                }
                if (changed)
                    dialogs_listView.ItemsSource = dialogPreviews;
                return true;
            });
        }

        private void LoadDialogsPreview()
        {
            dialogPreviews = new List<DialogPreviewModel>();
            foreach (var dialog in dialogs)
            {
                var avatar = StorageService.Download(dialog.avatar);
                string lastMessage = dialog.messages.Count == 0 ? "" : dialog.messages[0].content.Length >= 25 ? dialog.messages[0].content.Substring(0, 25) + "..." : dialog.messages[0].content;

                dialogPreviews.Add(new DialogPreviewModel
                {
                    Id = dialog.id,
                    Name = dialog.name,
                    PhotoUri = avatar,
                    IsReaded = !dialog.isReaded,
                    LastMessage = lastMessage
                });
            }
            dialogs_listView.ItemsSource = dialogPreviews;
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CreateDialog());
            // TODO: PLS UPDATE SCREEN AFTER RETURN FROM THIS SHIT
        }

        private async void dialogs_listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (DialogPreviewModel)e.Item;
            await Navigation.PushModalAsync(new DialogPreview(item.Id));
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
            List<DialogPreviewModel> tmpList;

            if (string.IsNullOrEmpty(findingString))
                tmpList = dialogPreviews.ToList();
            else
                tmpList = dialogPreviews.FindAll(x => x.Name.IndexOf(findingString, StringComparison.OrdinalIgnoreCase) > -1);

            dialogs_listView.ItemsSource = tmpList;
            dialogs_listView.HeightRequest = tmpList.Count * 100;
        }
    }
}