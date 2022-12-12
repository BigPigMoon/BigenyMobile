using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Bigeny.Views.DialogElements;
using Bigeny.Models;
using System.Threading.Tasks;
using Bigeny.Services;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace Bigeny.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DialogPreview : ContentPage
    {
        private int Id;

        public DialogPreview(int id)
        {
            InitializeComponent();
            Id = id;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Dialog dialog = await MessagesService.GetDialog(Id);
            var source = StorageService.Download(dialog.avatar);
            if (source.GetType() == typeof(string))
                dialogAvatar_Image.Source = (string)source;
            else
                dialogAvatar_Image.Source = (UriImageSource)source;
            dialogName_Label.Text = dialog.name;
            await LoadMessages(Id);
        }

        private async Task LoadMessages(int id)
        {
            List<Message> messages = await MessagesService.GetMessages(id);
            if (messages.Count == 0) return;
            dialog_stack.Children.Clear();
            var user = await UsersService.GetMe();
            DateTime lastDate = messages[0].createdAt.ToLocalTime();
            DateTimeFormatInfo info = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat;
            AddDate($"{lastDate.Day} {info.MonthGenitiveNames[lastDate.Month - 1]}");
            foreach(var message in messages)
            {
                if (message.createdAt.ToLocalTime().Day > lastDate.Day)
                {
                    lastDate= message.createdAt.ToLocalTime();
                    AddDate($"{lastDate.Day} {lastDate.Month}");
                }
                AddMessage(message.content, message.createdAt.ToLocalTime().ToString("HH:MM"), message.ownerId != user.Id);
            }
            var lastChild = dialog_stack.Children.Last();
            if (lastChild != null)
                await message_ScrollView.ScrollToAsync(lastChild, ScrollToPosition.MakeVisible, false);
        }

        private void AddMessage(string message, string time, bool isLeft)
        {
            dialog_stack.Children.Add(new MessagePreview(message, time, isLeft));
        }

        private void AddDate(string date)
        {
            dialog_stack.Children.Add(new TimePreview(date));
        }

        private async void OnBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void Avatar_Tapped(object sender, EventArgs e)
        {

        }

        private async void Sender_Tapped(object sender, EventArgs e)
        {
            await MessagesService.Send(Id, sender_input.Text);
            await LoadMessages(Id);
            sender_input.Text = "";
        }
    }
}