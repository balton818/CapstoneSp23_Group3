using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login
    {
        private LoginViewModel viewModel = new LoginViewModel();
        public Login()
        {
            this.InitializeComponent();
        }
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (await this.viewModel.LoginAsync(this.userNameTextBox.Text, this.passwordTextBox.Text))
            {
                if (NavigationService != null)
                {
                    NavigationService.Navigate(this.loginButton.NavUri);
                }
            }
            else
            {
                this.errorMessage.Text = "Username or password is incorrect";
            }


        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {


        }

    }
}

