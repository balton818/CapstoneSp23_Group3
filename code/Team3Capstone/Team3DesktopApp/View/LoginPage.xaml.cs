using System;
using System.Windows;
using System.Windows.Navigation;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;

/// <summary>
///     Interaction logic for LoginPage.xaml
/// </summary>
public partial class LoginPage
{
    #region Properties

    private FoodieViewModel ViewModel { get; }

    #endregion

    #region Constructors

    public LoginPage()
    {
        this.InitializeComponent();
        this.ViewModel = new FoodieViewModel();
    }

    #endregion

    #region Methods

    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        int id = await this.ViewModel.Login(this.userNameTextBox.Text, this.passwordTextBox.Text);
        if (id >= 0)
        {
            NavigationService ns = this.NavigationService;
            this.ViewModel.Userid = id;
            this.ViewModel.NavigateToPage(this.loginButton.NavUri, ns);
        }
        else
        {
            this.errorMessage.Text = "Username or password is incorrect";
        }
    }

    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {

        NavigationService ns = this.NavigationService;
        this.ViewModel.NavigateToPage(this.registerButton.NavUri, ns);
    }



    #endregion
}