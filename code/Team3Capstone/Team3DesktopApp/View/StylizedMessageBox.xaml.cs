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
using System.Windows.Shapes;

namespace Team3DesktopApp.View
{
    /// <summary>
    /// Interaction logic for StylizedMessageBox.xaml
    /// </summary>
    public partial class StylizedMessageBox : Window
    {
        static StylizedMessageBox newMessageBox;
        static string Button_id;

        public StylizedMessageBox()
        {
            InitializeComponent();
        }

        public static string ShowBox(string txtMessage, string txtTitle)
        {
            newMessageBox = new StylizedMessageBox();
            newMessageBox.Owner = App.Current.MainWindow;
            newMessageBox.messageContent.Text = txtMessage;
            newMessageBox.titleBlock.Text = txtTitle;
            newMessageBox.ShowDialog();
            return Button_id;
        }

        public static Tuple<string, string> ShowBox(string txtMessage, string txtTitle, int txtQuantity)
        {
            newMessageBox = new StylizedMessageBox();
            newMessageBox.Owner = App.Current.MainWindow;
            newMessageBox.messageContent.Text = txtMessage;
            newMessageBox.titleBlock.Text = txtTitle;
            newMessageBox.quantityTextBox.Visibility = Visibility.Visible;
            newMessageBox.quantityTextBox.Text = txtQuantity.ToString();
            newMessageBox.ShowDialog();
            var tuple = new Tuple<string, string>(Button_id, newMessageBox.quantityTextBox.Text);
            return tuple;

        }

        private bool isQuantity()
        {
            if (newMessageBox.quantityTextBox.Visibility == Visibility.Visible)
            {
                return true;
            }

            return false;
        }

        private bool isInvalidQuantity()
        {
            return string.IsNullOrEmpty(newMessageBox.quantityTextBox.Text) ||
                   newMessageBox.quantityTextBox.Text.All(Char.IsDigit) == false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.isQuantity())
            {
                if (!this.isInvalidQuantity())
                {
                    Button_id = "1";
                    newMessageBox.Close();
                }
                else
                {
                    newMessageBox.errorLabel.Visibility = Visibility.Visible;
                }
            }
            else
            {
                Button_id = "1";
                newMessageBox.Close();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Button_id = "2";
            newMessageBox.Close();
        }
    }
}
