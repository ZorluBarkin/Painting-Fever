using Godot;
using System;

public partial class MainMenu : Control
{
    [Export] private Button continueButton;
    /* [Export] private Button newGameButton;
    [Export] private Button levelSelectorButton;
    [Export] private Button settingsButton;
    [Export] private Button exitButton; */

    public override void _Ready()
    {
        //if (no saved game exists)
        //{
        //    continueButton.Disabled = true;
        //}
    }

    private void OnContinueButtonPressed()
    {
        GD.Print("Start Button Pressed");
        // Add logic to continue from last played levels start
    }
    
    private void OnNewGameButtonPressed()
    {
        GD.Print("New Game Button Pressed");
        // Add logic to start the game
    }

    private void OnLevelSelectorButtonPressed()
    {
        GD.Print("Level Selector Button Pressed");
        // Add logic to open level selector
    }

    private void OnSettingsButtonPressed()
    {
        GD.Print("Settings Button Pressed");
        // Add logic to open settings
    }

    private void OnExitButtonPressed()
    {
        GetTree().Quit();
    }
}
