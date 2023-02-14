using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View
{
    /// <summary>
    /// Interaction logic for NavMenu.xaml
    /// </summary>
    public partial class NavMenu : UserControl
    {
        public FoodieViewModel? FoodViewModel { get; set; }
        public Page current { get; set; }

        public NavMenu()
        {
            this.InitializeComponent();
        }

        private void NavButton_Click(object sender, RoutedEventArgs e)
        {

            NavButton navButton = (NavButton)sender;
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
    }
}
