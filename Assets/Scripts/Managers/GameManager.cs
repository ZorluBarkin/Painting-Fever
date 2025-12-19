using Godot;
using System;

public partial class GameManager : Node, IEventSubscriber
{
    public static GameManager Instance { get; private set; }

    public static event Action<GameState, GameState> GameStateChanged;

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

    public static void ChangeGameState(GameState oldState, GameState targetState)
    {
        GD.Print($"Game State changed from {oldState} to {targetState}");
    }

    public static void OnGameStateChanged(GameState oldState, GameState targetState)
    {
        switch (targetState)
        {
            case GameState.Start:
                break;
            case GameState.loading:
                break;
            case GameState.Play:
                break;
            case GameState.Paused:
                break;
            case GameState.End:
                break;
            default:
                break;
        }
    }
}
