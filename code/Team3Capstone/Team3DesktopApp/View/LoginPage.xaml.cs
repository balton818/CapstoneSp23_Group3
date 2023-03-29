using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;

/// <summary>
///     The login page view
/// </summary>
[ExcludeFromCodeCoverage]
public partial class LoginPage
{
    #region Properties

    /// <summary>Gets the view model.</summary>
    /// <value>The view model.</value>
    private FoodieViewModel? ViewModel { get; }

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
        var foodieViewModel = this.ViewModel;
        if (foodieViewModel != null)
        {
            var id = await foodieViewModel.Login(this.userNameTextBox.Text, this.passwordTextBox.Text);
            if (id >= 0)
            {
                if (NavigationService != null)
                {
                    var navigate = new PageNavigation(foodieViewModel);
                    navigate.NavigateToPage(this.loginButton.NavUri, NavigationService);
                }
            }
            else
            {
                this.errorMessage.Text = "Username or password is incorrect";
            }
        }
    }

    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        if (NavigationService != null)
        {
            var navigate = new PageNavigation(this.ViewModel);
            navigate.NavigateToPage(this.registerButton.NavUri, NavigationService);
        }
    }

    #endregion
}