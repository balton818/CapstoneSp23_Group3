using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
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

        if (!this.errorChecking())
        {
            return;
        }

        var result = await this.ViewModel?.RegisterUser(this.unTextBox.Text, this.pwTextBox.Text,
            this.emailTextBox.Text,
            this.firstNameTextBox.Text, this.lastNameTextBox.Text);
        if (result < 0)
        {
            this.generalError.Text = "error registering user";
            this.generalError.Visibility = Visibility.Visible;
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

    private bool errorChecking()
    {
        var errors = 0;
        this.unErrorLabel.Visibility = Visibility.Collapsed;
        this.pwError.Visibility = Visibility.Collapsed;
        this.emailError.Visibility = Visibility.Collapsed;
        this.nameError.Visibility = Visibility.Collapsed;
        Regex emailPattern = new Regex("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);
        if (!this.pwTextBox.Text.Equals(this.pwConfirmBox.Text))
        {
            this.pwError.Text = "Passwords do not match";
            this.pwError.Visibility = Visibility.Visible;
            errors++;

        }

        if (string.IsNullOrEmpty(this.pwTextBox.Text) || string.IsNullOrEmpty(this.pwConfirmBox.Text))
        {
            this.pwError.Text = "Password Cannot be empty";
            this.pwError.Visibility = Visibility.Visible;
            errors++;
        }
        if (string.IsNullOrEmpty(this.unTextBox.Text))
        {
            this.unErrorLabel.Visibility = Visibility.Visible;
            errors++;
        }
        if (string.IsNullOrEmpty(this.firstNameTextBox.Text) || string.IsNullOrEmpty(this.lastNameTextBox.Text))
        {
            this.nameError.Visibility = Visibility.Visible;
            this.nameError.Text = "Name fields cannot be empty";
            errors++;
        }
        if (string.IsNullOrEmpty(this.emailTextBox.Text))
        {
            this.emailError.Visibility = Visibility.Visible;
            this.emailError.Text = "Email cannot be empty";
            errors++;
        }
        if (!emailPattern.IsMatch(this.emailTextBox.Text))
        {
            this.emailError.Visibility = Visibility.Visible;
            this.emailError.Text = "Email is not valid";
            errors++;
        }

        if (errors > 0)
        {
            return false;
        }
        return true;
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