using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using Team3DesktopApp.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Team3DesktopApp.View;
[ExcludeFromCodeCoverage]
/// <summary>
///     An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class RegistrationPage : Page
{
    #region Properties

    private FoodieViewModel? ViewModel { get; }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="RegistrationPage" /> class.</summary>
    /// <param name="viewModel">The view model.</param>
    public RegistrationPage(FoodieViewModel viewModel)
    {
        this.InitializeComponent();
        this.ViewModel = viewModel;
    }

    #endregion

    #region Methods

    private async void RegisterButton_ClickAsync(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(this.userNameTextBox.Text) &&
            !string.IsNullOrWhiteSpace(this.passwordTextBox.Text) &&
            !string.IsNullOrWhiteSpace(this.emailTextBox.Text) &&
            !string.IsNullOrWhiteSpace(this.firstNameTextBox.Text) &&
            !string.IsNullOrWhiteSpace(this.lastNameTextBox.Text))
        {
            var result = await ((this.ViewModel != null
                ? this.ViewModel.RegisterUser(this.userNameTextBox.Text, this.passwordTextBox.Text,
                    this.emailTextBox.Text,
                    this.firstNameTextBox.Text, this.lastNameTextBox.Text)
                : null)!);
            if (result < 0)
            {
                this.errorLabel.Text = "error registering user";
                this.errorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                if (NavigationService != null)
                {
                    PageNavigation navigate = new PageNavigation(this.ViewModel);
                    navigate.NavigateToPage(this.registerButton.NavUri, NavigationService);
                }
            }
        }
        else
        {
            this.errorLabel.Visibility = Visibility.Visible;
        }
    }

    /// <summary>Handles the Click event of the BackButton_OnClickButton control.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
    private void BackButton_OnClickButton_Click(object sender, RoutedEventArgs e)
    {
        if (NavigationService != null)
        {
            PageNavigation navigate = new PageNavigation(this.ViewModel);
            navigate.NavigateToPage(this.backButton.NavUri, NavigationService);
        }
    }

    #endregion
}