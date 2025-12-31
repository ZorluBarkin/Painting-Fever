using Godot;
using System;

public partial class GameManager : Node, IEventSubscriber
{
	public static GameManager Instance { get; private set; }
	public static event Action<GameState, GameState> GameStateChanged;

	[Export] private PackedScene GUI;
	[Export] public Node2D GameSceneRoot { get; private set; }
	public GameState CurrentGameState { get; private set; } = GameState.Start;

	public override void _Ready()
	{
		Instance = this;
		InitiateGUI();
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

	private async void InitiateGUI()
	{
		await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
		GetTree().Root.AddChild(GUI.Instantiate());
	}

	public void ReturnToMainMenu()
	{
		LevelManager.Instance.UnloadCurrentLevel();
		ChangeGameState(CurrentGameState, GameState.Menu);
	}

	public static void ChangeGameState(GameState oldState, GameState targetState)
	{
		#if DEBUG
		GD.Print($"Game State changed from {oldState} to {targetState}");
		#endif
		GameStateChanged?.Invoke(oldState, targetState);
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
		Instance.CurrentGameState = targetState;
	}
}
