using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bigeny.Views.DialogElements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagePreview : ContentView
    {
        public MessagePreview(string message, string time, string from, bool isLeft)
        {
            InitializeComponent();
            message_label.Text = message;
            time_label.Text = time;

            if (from != null)
                from_label.Text = from;
            else
                from_label.IsVisible= false;

            if (isLeft)
            {
                message_frame.HorizontalOptions = LayoutOptions.Start;
                message_frame.Margin = new Thickness(10, 10, 100, 10);
            }
            else
            {
                message_frame.HorizontalOptions= LayoutOptions.End;
                message_frame.Margin = new Thickness(100, 10, 10, 10);
            }
        }
    }
}