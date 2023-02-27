using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;
[ExcludeFromCodeCoverage]
/// <summary>
///     Interaction logic for NavMenu.xaml
/// </summary>
public partial class NavMenu : UserControl
{
    #region Properties

    public FoodieViewModel FoodViewModel { get; set; }
    public Page Current { get; set; }

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
        if (this.Current.NavigationService != null)
        {
            PageNavigation navigate = new PageNavigation(this.FoodViewModel);
            navigate.NavigateToPage(navButton.NavUri, this.Current.NavigationService);
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