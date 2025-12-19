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
        //GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    void IEventSubscriber.UnsubscribeFromEvents()
    {
        //GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    public void ShowMainMenu()
    {
        MainMenu.Visible = true;
    }
    
    public void HideMainMenu()
    {
        MainMenu.Visible = false;
    }
}
