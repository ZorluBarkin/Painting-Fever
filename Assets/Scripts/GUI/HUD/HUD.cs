using Godot;

public partial class HUD : Control, IEventSubscriber
{
    [Export] private ProgressBar levelProgressBar;
    [Export] private Godot.Collections.Array<ColorRect> colorRects;

    [ExportCategory("UI Subscenes")]
    [Export] public PauseMenu PauseMenu { get; private set; }

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("Pause"))
        {
            PauseMenu.Visible = !PauseMenu.Visible;
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

    public void OnPauseButtonPressed()
    {
        PauseMenu.Visible = !PauseMenu.Visible;
    }

    public void OnGameStateChanged(GameState oldState, GameState targetState)
    {
        if (targetState == GameState.Menu)
        {
            PauseMenu.Visible = false;
        }
    }
}
