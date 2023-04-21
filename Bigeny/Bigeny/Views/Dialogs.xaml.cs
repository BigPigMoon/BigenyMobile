using Bigeny.Models;
using Bigeny.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bigeny.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dialogs : ContentPage
    {
        public class DialogPreviewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string LastMessage { get; set; }
            public object PhotoUri { get; set; }
            public bool IsReaded { get; set; }
        };

        public List<DialogPreviewModel> dialogPreviews;
        public List<Dialog> dialogs;
        byte[] hash = null;

        public Dialogs()
        {
            InitializeComponent();
            // TODO: do update later
            //UpdateDialogs();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var newDialogs = await MessagesService.GetDialogs();

            if (newDialogs != null)
            {
                if (dialogs == null)
                {
                    dialogs = newDialogs;
                    LoadDialogsPreview();
                }
                else
                {
                    byte[] hash = UtilService.GetMD5Checksum(dialogs);
                    byte[] newhash = UtilService.GetMD5Checksum(newDialogs);

                    if (!hash.SequenceEqual(newhash))
                    {
                        dialogs = newDialogs;
                        LoadDialogsPreview();
                    }
                }
            }

            UpdateDialogs();
        }

        private void UpdateDialogs()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(2000), () =>
            {
                var newDialogs = Task.Run(async () => await MessagesService.GetDialogs()).Result;

                if (newDialogs != null)
                {
                    if (dialogs == null)
                    {
                        dialogs = newDialogs;
                        LoadDialogsPreview();
                    }
                    else
                    {
                        byte[] hash = UtilService.GetMD5Checksum(dialogs);
                        byte[] newhash = UtilService.GetMD5Checksum(newDialogs);

                        if (!hash.SequenceEqual(newhash))
                        {
                            dialogs = newDialogs;
                            LoadDialogsPreview();
                        }
                    }
                }

                return true;
            });
        }

        private void LoadDialogsPreview()
        {
            dialogPreviews = new List<DialogPreviewModel>();
            foreach (var dialog in dialogs)
            {
                var avatar = StorageService.Download(dialog.avatar);
                string lastMessage = dialog.lastMessage == null ? "" : dialog.lastMessage.content.Length >= 25 ? dialog.lastMessage.content.Substring(0, 25) + "..." : dialog.lastMessage.content;

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
            var window = new CreateDialog();
            await Navigation.PushModalAsync(window);
        }

        private async void dialogs_listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (DialogPreviewModel)e.Item;
            var window = new DialogPreview(item.Id);
            await Navigation.PushModalAsync(window);
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