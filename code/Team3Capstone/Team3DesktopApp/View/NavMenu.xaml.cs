using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;
[ExcludeFromCodeCoverage]
/// <summary>
///     Interaction logic for NavMenu.xaml
/// </summary>
public partial class NavMenu : UserControl
{
    #region Properties

    public FoodieViewModel? FoodViewModel { get; set; }
    public Page current { get; set; }

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
        this.FoodViewModel.NavigateToPage(navButton.NavUri, this.current.NavigationService);
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