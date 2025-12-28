using Godot;
using System;

public partial class PlayerObject : CharacterBody2D
{
    public const float OBJECT_RADIUS = 20f;
    public const float GRAVITY = 9.81f;

    public PlayerColors Color { get; private set; } = PlayerColors.Grey;
    
    [Export] public float MoveSpeed { get; private set; }
    [Export] public float MaxStuckTime { get; private set; }
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
    public float GravityDirection = 1f;

    public bool Stuck { get; private set; } = false;
    public float StuckTime { get; private set; } = 0f;

    public event Action GotUnstuck;
    
    public override void _Ready()
    {
        OnBottomLane = true;
        // offset to bottom lane
        Position = new Vector2(Position.X, LaneCentrePoint.Position.Y + laneOffset - 10f);
        ChangeColor(Colors.Gray);
    }

    public override void _PhysicsProcess(double delta)
    {
        MoveObject();
        MoveAndSlide();

        if (StuckTime > MaxStuckTime)
        {
            GD.Print("Got stuck too long");
            // TODO: fail the level
        }

        // change to direction check later
        if (GetRealVelocity().X <= 0f)
        {
            Stuck = true;
            StuckTime += (float)delta;
        }
        else
        {
            if (Stuck == true)
            {
                GotUnstuck?.Invoke();
            }
            
            Stuck = false;
            StuckTime = 0f;
        }

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
        //Position = new Vector2(Position.X, LaneCentrePoint.Position.Y + (bottomLane ? laneOffset - 20f : -laneOffset + 20f));
        GravityDirection = bottomLane ? 1f : -1f;
    }

    /// <summary>
    /// Use only in _PhysicsProcess, maybe move this to process with delta?
    /// </summary>
    private void MoveObject()
    {
        Vector2 velocity = Velocity;
        velocity.X = MoveSpeed;
        velocity.Y = GRAVITY * GravityDirection * 100f;
        //velocity.Y += GRAVITY * GravityDirection /* * (float)delta */;
        Velocity = velocity;
    }

    public void SetLevelBasedVariables(Level currentLevel)
    {
        level = currentLevel;
        MoveSpeed = LevelManager.Instance.LevelData.GetMoveSpeed(level.Difficulty);
        MaxStuckTime = LevelManager.Instance.LevelData.GetMaxStuckTime(level.Difficulty);
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