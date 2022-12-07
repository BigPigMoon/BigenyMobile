using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Bigeny.Views.DialogElements;

namespace Bigeny.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DialogPreview : ContentPage
    {
        public DialogPreview()
        {
            InitializeComponent();
            LoadMessages();
        }

        private void LoadMessages()
        {
            AddDate("3 сентября");
            AddMessage("Ты пидор", "9:30", true);
            AddMessage("Ты пидор", "9:30", true);
            AddMessage("А может Ты пидор", "9:30", true);
            AddMessage("Нет Ты пидор", "9:30", false);
            AddMessage("Ну я пидор", "9:30", true);
            AddMessage("Ты пидор", "9:30", false);
            AddDate("3 сентября");
            AddMessage("Ты пидор", "9:30", true);
            AddMessage("Ты пидор", "9:30", true);
            AddMessage("А может Ты пидор", "9:30", true);
            AddMessage("Нет Ты пидор", "9:30", false);
            AddMessage("Ну я пидор", "9:30", true);
            AddMessage("Ты пидор", "9:30", false);
            AddDate("3 сентября");
            AddMessage("Я", "9:30", true);
            AddMessage("А", "9:30", true);
            AddMessage("Ты пидорТы пидорТы пидорТы пидорТы пидорТы пидорТы пидорТы пидорТы пидорТы пидорТы пидор", "9:30", true);
            AddMessage("А может Ты пидор", "9:30", true);
            AddMessage("Нет Ты пидор", "9:30", false);
            AddMessage("Ну я пидор", "9:30", true);
            AddMessage("Ты пидор", "9:30", false);
            AddDate("3 сентября");
            AddMessage("Ты пидор", "9:30", true);
            AddMessage("Ты пидор", "9:30", true);
            AddMessage("А может Ты пидор", "9:30", true);
            AddMessage("Нет Ты пидор", "9:30", false);
            AddMessage("Ну я пидор", "9:30", true);
            AddMessage("Ты пидор", "9:30", false);
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

        private void Sender_Tapped(object sender, EventArgs e)
        {

        }
    }
}