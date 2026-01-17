using Godot;
using System;

public partial class MainMenu : Control, IEventSubscriber
{
    [Export] private Button continueButton;
    [Export] private LevelSelector levelSelector;

    public override void _Ready()
    {
        ((IEventSubscriber)this).SubscribeToEvents();
        //if (no saved game exists)
        //{
        //    continueButton.Disabled = true;
        //}
    }

    public override void _ExitTree()
    {
        ((IEventSubscriber)this).UnsubscribeFromEvents();
    }

    void IEventSubscriber.SubscribeToEvents()
    {
        GameManager.GameStateChanged += OnGameStateChanged;
    }

    void IEventSubscriber.UnsubscribeFromEvents()
    {
        GameManager.GameStateChanged -= OnGameStateChanged;
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
        LevelManager.Instance.InstantiateLevel(Difficulty.Easy, 0);
        GameManager.ChangeGameState(GameState.Menu, GameState.Play);
    }

    private void OnLevelSelectorButtonPressed()
    {
        GD.Print("Level Selector Button Pressed");
        levelSelector.Visible = true;
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

    private void OnGameStateChanged(GameState oldState, GameState targetState)
    {
        Visible = targetState == GameState.Menu;
    }
}
