using Godot;

public partial class HUD : Control, IEventSubscriber
{
    public static bool PauseMenuOpen { get; private set; } = false;
    [Export] private ProgressBar levelProgressBar;
    [Export] private Godot.Collections.Array<ColorRect> colorRects;

    [ExportCategory("UI Subscenes")]
    [Export] private PauseMenu pauseMenu;

    public override void _Ready()
    {
        PauseMenuOpen = false;
        base._Ready();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("Pause"))
        {
            pauseMenu.Visible = !pauseMenu.Visible;
            PauseMenuOpen = pauseMenu.Visible;
        }
        base._Input(@event);
    }

    void IEventSubscriber.SubscribeToEvents()
    {
        GameManager.GameStateChanged += OnGameStateChanged;
    }

    void IEventSubscriber.UnsubscribeFromEvents()
    {
        GameManager.GameStateChanged -= OnGameStateChanged;
    }

    public void UpdateProgressBar(float progress)
    {
        levelProgressBar.Value = progress;
    }
    
    public void SelectColour(int index)
    {
        //colorRects[index].Shine(); // TODO: add Shine effect
    }

    public void OnGameStateChanged(GameState oldState, GameState targetState)
    {
        if (targetState == GameState.Menu)
        {
            pauseMenu.Visible = false;
            PauseMenuOpen = false;
        }
    }
}
