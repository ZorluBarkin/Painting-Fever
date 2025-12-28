using Godot;

public partial class Camera : Camera2D, IEventSubscriber
{
    private PlayerObject PlayerObject;
    private Marker2D laneCentrePoint;
    public override void _Ready()
    {
        ((IEventSubscriber)this).SubscribeToEvents();
    }
    
    public override void _Process(double delta)
    {
        if (PlayerObject != null)
        {
            Move(PlayerObject, delta);
        }
    }

    public override void _ExitTree()
    {
        ((IEventSubscriber)this).UnsubscribeFromEvents();
    }

    void IEventSubscriber.SubscribeToEvents()
    {
        LevelManager.Instance.LevelLoaded += OnLevelLoaded;
        LevelManager.Instance.LevelUnloaded += OnLevelUnloaded;
    }

    void IEventSubscriber.UnsubscribeFromEvents()
    {
        LevelManager.Instance.LevelLoaded -= OnLevelLoaded; 
        LevelManager.Instance.LevelUnloaded -= OnLevelUnloaded;
    }

    private void Move(PlayerObject playerObject, double delta)
    {
        float verticalPos = Mathf.Clamp(playerObject.Position.Y, laneCentrePoint.Position.Y - playerObject.laneOffset, laneCentrePoint.Position.Y + playerObject.laneOffset);
        Position = new Vector2(Position.X + playerObject.MoveSpeed * (float)delta, verticalPos);
    }

    // TODO: follow if the player is over a certain speed
    private void Follow(float delta)
    {
        Vector2 targetPosition = PlayerObject.Position + Offset;
        Position = targetPosition;
    }

    private void OnLevelLoaded(Level loadedLevel)
    {
        PlayerObject = loadedLevel.PlayerObject;
        laneCentrePoint = PlayerObject.LaneCentrePoint;
        PlayerObject.GotUnstuck += OnGotUnstuck;
        Position = PlayerObject.Position + Offset;
        MakeCurrent();
    }

    private void OnLevelUnloaded(Level unloadedLevel)
    {
        PlayerObject.GotUnstuck -= OnGotUnstuck;
        PlayerObject = null;
    }


    private void OnGotUnstuck()
    {
        ResetToPlayer(PlayerObject);
    }

    private void ResetToPlayer(PlayerObject playerObject)
    {
        Position = playerObject.Position + Offset;
    }
}
