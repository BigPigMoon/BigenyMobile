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
        public MessagePreview(string message, string time, bool isLeft)
        {
            InitializeComponent();
            message_label.Text = message;
            time_label.Text = time;
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