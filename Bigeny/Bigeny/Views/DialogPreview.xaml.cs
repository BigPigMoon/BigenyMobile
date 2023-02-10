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
using Plugin.FirebasePushNotification;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using Bigeny.Http;
using CarouselView.FormsPlugin.Abstractions;

namespace Bigeny.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DialogPreview : ContentPage
    {
        private new readonly int Id;
        private List<Message> messages;
        private User user;
        DateTime lastDate;
        Dialog dialog;

        public DialogPreview(int id)
        {
            InitializeComponent();
            Id = id;
            UpdateMessages();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            user = await UsersService.GetMe();
            dialog = await MessagesService.GetDialog(Id);
            var source = StorageService.Download(dialog.avatar);
            if (source.GetType() == typeof(string))
                dialogAvatar_Image.Source = (string)source;
            else
                dialogAvatar_Image.Source = (UriImageSource)source;
            dialogName_Label.Text = dialog.name;
            await LoadMessages(Id);
        }

        private void UpdateMessages()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Dictionary<int, string> people = new Dictionary<int, string>();
                var list = Task.Run(async () => await MessagesService.GetMessages(Id)).Result;
                if (list != null && list.Count == 0 || messages.Count == 0) return true;
                if (list != null && list.Last().id != messages.Last().id)
                {
                    var message = list.Last();
                    if (message.createdAt.ToLocalTime().Day > lastDate.Day)
                    {
                        lastDate = message.createdAt.ToLocalTime();
                        AddDate($"{lastDate.Day} {lastDate.Month}");
                    }

                    if (dialog.count > 2)
                    {
                        if (people.ContainsKey(message.ownerId))
                        {
                            AddMessage(message.content, message.createdAt.ToLocalTime().ToString("HH:mm"), people[message.ownerId], message.ownerId != user.Id);
                        }
                        else
                        {
                            string ownerMame = Task.Run(async () => await UsersService.GetUser(message.ownerId)).Result.Nickname;
                            people.Add(message.ownerId, ownerMame);
                            AddMessage(message.content, message.createdAt.ToLocalTime().ToString("HH:mm"), people[message.ownerId], message.ownerId != user.Id);
                        }
                    }
                    else
                    {
                        AddMessage(message.content, message.createdAt.ToLocalTime().ToString("HH:mm"), null, message.ownerId != user.Id);
                    }

                    messages = list.ToList();
                    Task.Run(async () =>
                    {
                        var lastChild = dialog_stack.Children.Last();
                        if (lastChild != null)
                            await message_ScrollView.ScrollToAsync(lastChild, ScrollToPosition.MakeVisible, false);
                    });
                }
                return true;
            });
        }

        private async Task LoadMessages(int id)
        {
            messages = await MessagesService.GetMessages(id);
            if (messages.Count == 0) return;
            dialog_stack.Children.Clear();

            Dictionary<int, string> people = new Dictionary<int, string>();
            
            lastDate = messages[0].createdAt.ToLocalTime();
            DateTimeFormatInfo info = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat;
            AddDate($"{lastDate.Day} {info.MonthGenitiveNames[lastDate.Month - 1]}");
            foreach(var message in messages)
            {
                if (message.createdAt.ToLocalTime().Day > lastDate.Day)
                {
                    lastDate= message.createdAt.ToLocalTime();
                    AddDate($"{lastDate.Day} {info.MonthGenitiveNames[lastDate.Month - 1]}");
                }
                if (dialog.count > 2)
                {
                    if (people.ContainsKey(message.ownerId))
                    {
                        AddMessage(message.content, message.createdAt.ToLocalTime().ToString("HH:mm"), people[message.ownerId], message.ownerId != user.Id);
                    }
                    else
                    {
                        string ownerName = (await UsersService.GetUser(message.ownerId)).Nickname;
                        people.Add(message.ownerId, ownerName);
                        AddMessage(message.content, message.createdAt.ToLocalTime().ToString("HH:mm"), people[message.ownerId], message.ownerId != user.Id);
                    }
                }
                else
                {
                    AddMessage(message.content, message.createdAt.ToLocalTime().ToString("HH:mm"), null, message.ownerId != user.Id);
                }
            }
            var lastChild = dialog_stack.Children.Last();
            if (lastChild != null)
                await message_ScrollView.ScrollToAsync(lastChild, ScrollToPosition.MakeVisible, false);
        }

        private void AddMessage(string message, string time, string from, bool isLeft)
        {
            dialog_stack.Children.Add(new MessagePreview(message, time, from, isLeft));
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
            sender_Button.IsEnabled= false;
            if (sender_input.Text != null && sender_input.Text.Length > 0)
            {
                await MessagesService.Send(Id, sender_input.Text);
                if (DateTime.Now.ToLocalTime().Day > lastDate.Day)
                {
                    lastDate = DateTime.Now.ToLocalTime();
                    DateTimeFormatInfo info = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat;
                    AddDate($"{lastDate.Day} {info.MonthGenitiveNames[lastDate.Month - 1]}");
                }
                if (messages.Count == 0)
                {
                    string ownerName = (await UsersService.GetMe()).Nickname;
                    AddMessage(sender_input.Text, DateTime.Now.ToLocalTime().ToString("HH:mm"), ownerName, false);
                }
                sender_input.Text = "";
            }
            sender_Button.IsEnabled = true;
            //sender_input.Focus();
        }
    }
}