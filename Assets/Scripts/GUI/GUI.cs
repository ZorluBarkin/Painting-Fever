using Godot;
using System;

public partial class GUI : CanvasLayer, IEventSubscriber
{
    public static GUI Instance { get; private set; }

    [Export] public MainMenu MainMenu { get; private set; }

    public override void _Ready()
    {
        Instance = this;
        ((IEventSubscriber)this).SubscribeToEvents();
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

    public void ShowMainMenu()
    {
        MainMenu.Visible = true;
    }
    
    public void HideMainMenu()
    {
        MainMenu.Visible = false;
    }

    private void OnGameStateChanged(GameState oldState, GameState newState)
    {
        switch (newState)
        {
            case GameState.Menu:
                if (oldState == GameState.Start)
                    ShowMainMenu();
                break;
            case GameState.Play:
                if (oldState == GameState.Menu)
                    HideMainMenu();
                break;
            default:
                break;
        }
    }
}
