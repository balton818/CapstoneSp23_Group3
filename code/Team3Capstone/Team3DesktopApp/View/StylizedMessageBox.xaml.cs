using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;

namespace Team3DesktopApp.View;

/// <summary>
///     Interaction logic for StylizedMessageBox.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class StylizedMessageBox
{
    #region Data members

    private static StylizedMessageBox newMessageBox = null!;
    private static string buttonId = null!;

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="StylizedMessageBox" /> class.</summary>
    public StylizedMessageBox()
    {
        this.InitializeComponent();
    }

    #endregion

    #region Methods

    /// <summary>Shows a stylized dialog box.</summary>
    /// <param name="txtMessage">The text message to display to the user.</param>
    /// <param name="txtTitle">The text title for the message box.</param>
    /// <returns>
    ///   1 for OK and 2 for cancel
    /// </returns>
    public static string ShowBox(string txtMessage, string txtTitle)
    {
        newMessageBox = new StylizedMessageBox();
        newMessageBox.Owner = Application.Current.MainWindow;
        newMessageBox.messageContent.Text = txtMessage;
        newMessageBox.titleBlock.Text = txtTitle;
        newMessageBox.ShowDialog();
        return buttonId;
    }

    /// <summary>Shows a stylized dialog box with input box.</summary>
    /// <param name="txtMessage">The text message to display to the user.</param>
    /// <param name="txtTitle">The text title for the message box.</param>
    /// <param name="txtQuantity"> the original quantity amount to put in the textbox</param>
    /// <returns>a tuple with 1 for ok and 2 for cancel as item1 and the quantity confirmed by the user as item2</returns>
    public static Tuple<string, string> ShowBox(string txtMessage, string txtTitle, int txtQuantity)
    {
        newMessageBox = new StylizedMessageBox();
        newMessageBox.Owner = Application.Current.MainWindow;
        newMessageBox.messageContent.Text = txtMessage;
        newMessageBox.titleBlock.Text = txtTitle;
        newMessageBox.quantityTextBox.Visibility = Visibility.Visible;
        newMessageBox.quantityTextBox.Text = txtQuantity.ToString();
        newMessageBox.ShowDialog();
        var tuple = new Tuple<string, string>(buttonId, newMessageBox.quantityTextBox.Text);
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
               newMessageBox.quantityTextBox.Text.All(char.IsDigit) == false;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        if (this.isQuantity())
        {
            if (!this.isInvalidQuantity())
            {
                buttonId = "1";
                newMessageBox.Close();
            }
            else
            {
                newMessageBox.errorLabel.Visibility = Visibility.Visible;
            }
        }
        else
        {
            buttonId = "1";
            newMessageBox.Close();
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        buttonId = "2";
        newMessageBox.Close();
    }

    #endregion
}