using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Team3;
using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private LoginViewModel viewModel = new LoginViewModel();
        public Login()
        {
            this.InitializeComponent();
        }
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (await this.viewModel.LoginAsync(this.UsernameTextBox.Text, this.PasswordTextBox.Text))
            {
                NavigationService.Navigate(this.LoginButton.NavUri);
            }
            else
            {
                this.ErrorMessage.Text = "Username or password is incorrect";
            }


        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {


            if (NavigationService != null)
            {
                NavigationService.Navigate(this.RegisterButton.NavUri);
            }
        }
    }
}

