using System.Windows;
using System.Windows.Controls;
using Team3DesktopApp.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Team3DesktopApp.View;

/// <summary>
///     An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class RegistrationPage : Page
{
    #region Properties

    private FoodieViewModel? ViewModel { get; }

    #endregion

    #region Constructors

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
                this.ViewModel.NavigateToPage(this.registerButton.NavUri, this.NavigationService);
            }
        }
        else
        {
            this.errorLabel.Visibility = Visibility.Visible;
        }
    }

    private void BackButton_OnClickButton_Click(object sender, RoutedEventArgs e)
    {
        this.ViewModel.NavigateToPage(this.backButton.NavUri, this.NavigationService);
    }

    #endregion
}