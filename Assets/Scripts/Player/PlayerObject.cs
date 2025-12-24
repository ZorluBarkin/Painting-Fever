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
    public Node2D LaneCentrePoint;
    
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
        Position += new Vector2(MoveSpeed * (float)delta, 0);
        LevelManager.Instance.CurrentLevel.UpdateProgress(MoveSpeed, AbleToPaint);
    }

    public void SetMovementSpeed(float speed, Difficulty difficulty)
    {
        MoveSpeed = speed;
        switch (difficulty)
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
}