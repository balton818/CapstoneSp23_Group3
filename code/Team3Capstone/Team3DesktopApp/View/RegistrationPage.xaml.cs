using System.Windows;
using System.Windows.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Team3DesktopApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistrationPage : Page
    {

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Method intentionally left empty.
        }

        private void BackButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null)
            {
                NavigationService.Navigate(this.backButton.NavUri);
            }
        }
    }
}
