using Godot;
using System;

public partial class PlayerObject : CharacterBody2D
{
    public const float OBJECT_RADIUS = 20f;
    public const float GRAVITY = 9.81f;

    public PlayerColors Color { get; private set; } = PlayerColors.Grey;
    
    [Export] public Sprite2D Shape { get; private set; }
    public Level level;
    private float slowDownMultiplier;
    public float MoveSpeed { get; private set; }
    public float MaxStuckTime { get; private set; }
    private float LaneSwitchMult{ get; set; }
    
    public bool OnBottomLane { get; private set; } = true;
    public bool AbleToPaint { get; private set; } = false;
    
    public Marker2D LaneCentrePoint;
    public float laneOffset = 0f;
    public float GravityDirection = 1f;

    public bool Sticked { get; private set; } = false;
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

        CheckCollisions();

        if (StuckTime > MaxStuckTime)
        {
            LevelFailed();
            return;
        }

        // TODO: change to direction check later
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
        if (GUI.Instance.PauseMenuOpened) return;
        
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

    public void SetLevelBasedVariables(Level currentLevel)
    {
        level = currentLevel;
        slowDownMultiplier = 1 - LevelManager.Instance.LevelData.stickySlowdownMultiplier;
        MoveSpeed = LevelManager.Instance.LevelData.GetMoveSpeed(level.Difficulty);
        LaneSwitchMult = MoveSpeed / 5f; // 80f at 400 speed, 200f at 1000 speed
        MaxStuckTime = LevelManager.Instance.LevelData.GetMaxStuckTime(level.Difficulty);
        LaneCentrePoint = level.CentralLinePoint;
        laneOffset = level.LaneOffset;
    }

    private void SwitchGravity(bool bottomLane)
    {
        GravityDirection = bottomLane ? 1f : -1f;
    }

    /// <summary>
    /// Use only in _PhysicsProcess, maybe move this to process with delta?
    /// </summary>
    private void MoveObject()
    {
        Vector2 velocity = Velocity;
        velocity.X = MoveSpeed;
        velocity.Y = GRAVITY * GravityDirection * LaneSwitchMult;
        Velocity = velocity;
    }

    private void CheckCollisions()
    {
        CollisionObject2D collider = GetLastSlideCollision() != null ? (CollisionObject2D)GetLastSlideCollision().GetCollider() : null;
        if (collider == null) return;
        
        if (collider.GetCollisionLayerValue(3)) // 3rd layer is saw / spikes
        {
            LevelFailed();
        }
        else if (collider.GetCollisionLayerValue(4))
        {
            if(!Sticked)
            {
                Sticked = true;
                MoveSpeed *= slowDownMultiplier;
            }
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

    private void OnScreenExited()
    {
        LevelFailed();
    }

    private void LevelFailed()
    {
        #if DEBUG
        GD.Print("Level Failed");
        #endif
        GameManager.Instance.ReturnToMainMenu();
    }
}