namespace Team3DesktopApp.Model;

/// <summary>
///     Defines a single recipe step
/// </summary>
public class RecipeStep
{
    #region Properties

    /// <summary>
    ///     Gets or sets the step number which indicates the preparation order.
    /// </summary>
    /// <value>
    ///     The step number.
    /// </value>
    public int StepNumber { get; set; }

    /// <summary>
    ///     Gets or sets the instructions for the associated step.
    /// </summary>
    /// <value>
    ///     The instructions for a step.
    /// </value>
    public string? Instructions { get; set; }

    #endregion
}