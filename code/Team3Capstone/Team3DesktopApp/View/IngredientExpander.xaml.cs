using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;

/// <summary>Interaction logic for ingredientExpander.xaml</summary>
[ExcludeFromCodeCoverage]
public partial class IngredientExpander : UserControl
{
    #region Properties

    /// <summary>Gets or sets the name of the ingredient.</summary>
    /// <value>The name of the ingredient.</value>
    public string IngredientName { get; set; }

    /// <summary>Gets or sets the ingredient amount.</summary>
    /// <value>The ingredient amount.</value>
    public int IngredientAmount { get; set; }

    /// <summary>Gets or sets the view model.</summary>
    /// <value>The view model.</value>
    public FoodieViewModel ViewModel { get; set; }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="IngredientExpander" /> class.</summary>
    /// <param name="name">The name.</param>
    /// <param name="amount">The amount.</param>
    /// <param name="viewModel">The view model.</param>
    public IngredientExpander(string name, int amount, FoodieViewModel viewModel)
    {
        this.InitializeComponent();
        this.IngredientName = name;
        this.IngredientAmount = amount;
        this.ViewModel = viewModel;
    }

    #endregion

    #region Methods

    private void PlusButton_OnClick(object sender, RoutedEventArgs e)
    {
        this.IngredientAmount++;
        ;
        this.ViewModel.EditIngredient(this.IngredientName, this.IngredientAmount);
    }

    private void MinusButton_OnClick(object sender, RoutedEventArgs e)
    {
        this.IngredientAmount--;
        this.ViewModel.EditIngredient(this.IngredientName, this.IngredientAmount);
    }

    private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    #endregion
}