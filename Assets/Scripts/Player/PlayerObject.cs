using Godot;

public partial class PlayerObject : Node2D
{
    public PlayerColors Color { get; private set; } = PlayerColors.Grey;
    
    [Export] public float MoveSpeed { get; private set; } = 300f;
    
    /// <summary>
    /// changes besed on current speed
    /// </summary>
    public float LaneSwitchTime { get; set; } = 1f;
    
    public bool OnBottomLane { get; private set; } = true;
    public bool AbleToPaint { get; private set; } = false;
    public Level level;
    public Node2D LaneCentrePoint;
    public float laneOffset = 0f;
    
    public override void _Ready()
    {
        OnBottomLane = true;
    }
    
    public override void _Process(double delta)
    {
        // Constant forward movement
        MoveCube(delta);
        base._Process(delta);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("Move Up"))
        {
            GD.Print("Move Up pressed");
            OnBottomLane = false;
        }
        else if (@event.IsActionPressed("Move Down"))
        {
            GD.Print("Move Down pressed");
            OnBottomLane = true;
        }
        base._Input(@event);
    }

    private void MoveCube(double delta)
    {
        //Position += new Vector2(MoveSpeed * (float)delta, 0);
        Position = new Vector2(Position.X + MoveSpeed * (float)delta, LaneCentrePoint.Position.Y + (OnBottomLane ? laneOffset : -laneOffset));
        level.UpdateProgress(MoveSpeed, AbleToPaint);
    }

    public void SetLevelBasedVariables(Level currentLevel)
    {
        level = currentLevel;
        MoveSpeed = LevelManager.Instance.LevelData.GetMoveSpeed(level.Difficulty);
        LaneCentrePoint = level.CentralLinePoint;
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

        // TODO: temporary
        laneOffset = 100f;
    }
}