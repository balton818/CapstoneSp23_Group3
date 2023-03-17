using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;

/// <summary>
///     Interaction logic for NavMenu.xaml
///     Custom control for hamburger navigation menu
/// </summary>
[ExcludeFromCodeCoverage]
public partial class NavMenu
{
    #region Properties

    /// <summary>Gets or sets the food view model.</summary>
    /// <value>The main view model for the app.</value>
    public FoodieViewModel? FoodViewModel { get; set; }

    /// <summary>Gets or sets the current page displayed.</summary>
    /// <value>The current page the nav menu exists on</value>
    public Page? Current { get; set; }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="NavMenu" /> class.</summary>
    public NavMenu()
    {
        this.InitializeComponent();
    }

    #endregion

    #region Methods

    private void NavButton_Click(object sender, RoutedEventArgs e)
    {
        var navButton = (NavButton)sender;
        var current = this.Current;
        if (current is { NavigationService: { } })
        {
            var navigate = new PageNavigation(this.FoodViewModel);
            var page = this.Current;
            if (page is { NavigationService: { } })
            {
                navigate.NavigateToPage(navButton.NavUri, page.NavigationService);
            }
        }
    }

    private void navMenu_Click(object sender, RoutedEventArgs e)
    {
        if (this.navGrid.Visibility != Visibility.Visible)
        {
            this.navGrid.Visibility = Visibility.Visible;
        }
        else
        {
            this.navGrid.Visibility = Visibility.Collapsed;
        }
    }

    #endregion
}