using Godot;

public partial class Camera : Camera2D, IEventSubscriber
{
    private PlayerObject PlayerObject;
    public override void _Ready()
    {
        ((IEventSubscriber)this).SubscribeToEvents();
    }
    
    public override void _Process(double delta)
    {
        if (PlayerObject != null)
        {
            Follow((float)delta);
        }
    }

    public override void _ExitTree()
    {
        ((IEventSubscriber)this).UnsubscribeFromEvents();
    }

    void IEventSubscriber.SubscribeToEvents()
    {
        LevelManager.Instance.LevelLoaded += OnLevelLoaded;
    }

    void IEventSubscriber.UnsubscribeFromEvents()
    {
        LevelManager.Instance.LevelLoaded -= OnLevelLoaded;
    }

    private void Follow(float delta)
    {
        Vector2 targetPosition = PlayerObject.Position + Offset;
        Position = targetPosition;
        //Position = Position.Lerp(targetPosition, SmoothSpeed * delta);
    }

    private void OnLevelLoaded(Level loadedLevel)
    {
        PlayerObject = loadedLevel.PlayerObject;
        MakeCurrent();
    }
}
