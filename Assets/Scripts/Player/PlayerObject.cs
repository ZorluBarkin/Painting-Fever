using Godot;

public partial class PlayerObject : Node2D
{
    public PlayerColors Color { get; private set; } = PlayerColors.Grey;
    
    [Export] public float MoveSpeed { get; set; } = 300f;
    [Export] public float LaneDistance { get; set; } = 100f;
    [Export] public float LaneSwitchSpeed { get; set; } = 10f;
    
    public bool OnBottomLane { get; private set; } = true;
    [Export] public Node2D lineDefiner;
    
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
    }
}