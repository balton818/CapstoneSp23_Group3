using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View
{
    /// <summary>
    /// Interaction logic for FoundRecipePage.xaml
    /// </summary>
    public partial class FoundRecipePage : Page
    {
        private FoodieViewModel viewModel = new FoodieViewModel();
        public FoundRecipePage()
        {
            this.InitializeComponent();
        }

        private void viewDetailClick(object sender, RoutedEventArgs e)
        {
            NavButton navButton = (NavButton)sender;
            this.viewModel.RecipeDetailNav(navButton.NavUri, navButton.Text);
        }

        private void navButtonClick(object sender, RoutedEventArgs e)
        {
            NavButton navButton = (NavButton)sender;
            this.viewModel.Navigate(navButton.NavUri);
        }

        private void navMenuClick(object sender, MouseButtonEventArgs e)
        {
            this.navGrid.Visibility = Visibility.Visible;
        }
    }
}
