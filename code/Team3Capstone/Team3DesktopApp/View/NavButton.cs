using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace Team3DesktopApp.View;

/// <summary>
/// </summary>
/// <seealso cref="System.Windows.Controls.Button" />
[ExcludeFromCodeCoverage]
public class NavButton : Button
{
    #region Data members

    /// <summary>
    ///     The text property
    /// </summary>
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(NavButton), new PropertyMetadata(null));

    /// <summary>
    ///     The nav URI property
    /// </summary>
    public static readonly DependencyProperty NavUriProperty =
        DependencyProperty.Register("NavUri", typeof(string), typeof(NavButton), new PropertyMetadata(null));

    #endregion

    #region Properties

    /// <summary>
    ///     Gets or sets the text.
    /// </summary>
    /// <value>
    ///     The text.
    /// </value>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    ///     Gets or sets the nav URI.
    /// </summary>
    /// <value>
    ///     The nav URI.
    /// </value>
    public string NavUri
    {
        get => (string)GetValue(NavUriProperty);
        set => SetValue(NavUriProperty, value);
    }

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes the <see cref="NavButton" /> class.
    /// </summary>
    static NavButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(NavButton), new FrameworkPropertyMetadata(typeof(NavButton)));
    }

    #endregion
}