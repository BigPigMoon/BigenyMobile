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
    public partial class TimePreview : ContentView
    {
        public TimePreview(string date)
        {
            InitializeComponent();
            date_label.Text = date;
        }
    }
}