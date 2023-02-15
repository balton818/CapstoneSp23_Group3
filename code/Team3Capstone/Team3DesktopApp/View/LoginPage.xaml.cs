﻿using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Navigation;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;
[ExcludeFromCodeCoverage]
/// <summary>
///     Interaction logic for LoginPage.xaml
/// </summary>
public partial class LoginPage
{
    #region Properties

    /// <summary>Gets the view model.</summary>
    /// <value>The view model.</value>
    private FoodieViewModel ViewModel { get; }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="LoginPage" /> class.</summary>
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
            if (NavigationService != null)
            {
                PageNavigation navigate = new PageNavigation(this.ViewModel);
                navigate.NavigateToPage(this.loginButton.NavUri, NavigationService);
            }
        }
        else
        {
            this.errorMessage.Text = "Username or password is incorrect";
        }
    }

    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {

        if (NavigationService != null)
        {
            PageNavigation navigate = new PageNavigation(this.ViewModel);
            navigate.NavigateToPage(this.registerButton.NavUri, NavigationService);
        }
    }



    #endregion
}