using System.Collections;

namespace Team3DesktopApp.Model;

/// <summary>
///     The pantry class.
/// </summary>
public class Pantry
{
    #region Properties

    /// <summary>Gets the ingredients.</summary>
    /// <value>The collection of ingredients in the pantry.</value>
    public ArrayList Ingredients { get; }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="Pantry" /> class.</summary>
    public Pantry()
    {
        this.Ingredients = new ArrayList();
    }

    #endregion
}