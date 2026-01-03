using Godot;
using System;

public partial class GUI : CanvasLayer, IEventSubscriber
{
    public static GUI Instance { get; private set; }

    [Export] public MainMenu MainMenu { get; private set; }
    [Export] public HUD HUD { get; private set; }

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

    public void ShowHUD()
    {
        HUD.Visible = true;
    }

    public void HideHUD()
    {
        HUD.Visible = false;

        if (Engine.TimeScale != 1f)
            Engine.TimeScale = 1f;  
    }

    private void OnGameStateChanged(GameState oldState, GameState newState)
    {
        switch (newState)
        {
            case GameState.Menu:
                if (oldState == GameState.Start)
                    ShowMainMenu();
                    HideHUD();
                break;
            case GameState.Play:
                if (oldState == GameState.Menu)
                    HideMainMenu();
                    ShowHUD();
                break;
            default:
                break;
        }
    }
}
