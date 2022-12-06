using Bigeny.Services;
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
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            AuthService.Logout();
            Application.Current.MainPage = new Login();
        }

        private void Rename_Entry(object sender, EventArgs e)
        {
            if (rename_Entry.IsEnabled)
            {
            }
            else
            {
            }
            rename_Entry.IsEnabled = !rename_Entry.IsEnabled;
        }

        private void AvatarChange_Tapped(object sender, EventArgs e)
        {

        }

        private void PhoneChange_Tapped(object sender, EventArgs e)
        {

        }
    }
}