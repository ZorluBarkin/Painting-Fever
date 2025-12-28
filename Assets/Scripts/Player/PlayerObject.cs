using Godot;

public partial class PlayerObject : RigidBody2D
{
    public const float OBJECT_RADIUS = 20f;
    public PlayerColors Color { get; private set; } = PlayerColors.Grey;
    
    [Export] public float MoveSpeed { get; private set; } = 300f;
    [Export] public Sprite2D Shape { get; private set; }
    
    /// <summary>
    /// changes besed on current speed
    /// </summary>
    public float LaneSwitchTime { get; set; } = 1f;
    
    public bool OnBottomLane { get; private set; } = true;
    public bool AbleToPaint { get; private set; } = false;
    public Level level;
    public Marker2D LaneCentrePoint;
    public float laneOffset = 0f;
    
    public override void _Ready()
    {
        OnBottomLane = true;
    }
    
    //public override void _Process(double delta)
    //{
    //    // Constant forward movement
    //    base._Process(delta);
    //}

    public override void _PhysicsProcess(double delta)
    {
        MoveObject();
        base._PhysicsProcess(delta);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("Move Up") && OnBottomLane)
        {
            SwitchGravity(OnBottomLane = false);
        }
        else if (@event.IsActionPressed("Move Down") && !OnBottomLane)
        {
            SwitchGravity(OnBottomLane = true);
        }

        if(@event.IsActionPressed("Color 1"))
        {
            Color = PlayerColors.Red;
            ChangeColor(Colors.Red);
        }
        else if (@event.IsActionPressed("Color 2"))
        {
            Color = PlayerColors.Green;
            ChangeColor(Colors.Green);
        }
        else if (@event.IsActionPressed("Color 3"))
        {
            Color = PlayerColors.Blue;
            ChangeColor(Colors.Blue);
        }

        base._Input(@event);
    }

    private void SwitchGravity(bool bottomLane)
    {
        LinearVelocity = new Vector2(LinearVelocity.X, -GetGravity().Y);

        if (bottomLane)
            GravityScale = 1f;
        else 
            GravityScale = -1f;
    }

    private void MoveObject()
    {
        LinearVelocity = new Vector2(MoveSpeed, LinearVelocity.Y);
    }

    public void SetLevelBasedVariables(Level currentLevel)
    {
        level = currentLevel;
        MoveSpeed = LevelManager.Instance.LevelData.GetMoveSpeed(level.Difficulty);
        LaneCentrePoint = level.CentralLinePoint;
        laneOffset = level.LaneOffset;

        switch (level.Difficulty)
        {
            case Difficulty.Easy:
                LaneSwitchTime = 3f;
                break;
            case Difficulty.Medium:
                LaneSwitchTime = 1.5f;
                break;
            case Difficulty.Hard:
                LaneSwitchTime = 1f;
                break;
            case Difficulty.EasterEgg:
                LaneSwitchTime = 1f;
                break;
        }
    }

    private void ChangeColor(Color newColor)
    {
        Shape.Modulate = newColor;
    }

    private void TeleportToLocation(Vector2 newPosition)
    {
        Position = newPosition;
    }
}