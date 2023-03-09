using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Team3DesktopApp.View
{
    /// <summary>
    /// Interaction logic for AddToPlanPanel.xaml
    /// </summary>
    public partial class AddToPlanPanel : UserControl
    {
        public string CurrentRecipe { get; set; }
        public AddToPlanPanel()
        {
            InitializeComponent();
        }

        private void addToPlanClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void cancelClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
