using Godot;

public partial class Trap : Node2D
{
    [Export] private StaticBody2D trapBody;
    [Export] private Node2D SpriteParent;
    [Export] private bool HasMovement = false;
    [Export] private Tween.TransitionType transitionType = Tween.TransitionType.Linear;
    [Export] private float MovementTime = 5f;

    [Export] private bool rotational = false;
    [Export] private float rotationDegree = 5f;

    [Export] private Godot.Collections.Array<Marker2D> trapPositions;

    public override void _Ready()
    {
        Move();
        base._Ready();
    }

    public override void _Process(double delta)
    {
        if (rotational)
            SpriteParent.Rotation += rotationDegree * (float)delta;
        base._Process(delta);
    }

    public void Move()
    {
        if (HasMovement)
        {
            var tween = this.CreateTween()
                .SetLoops(int.MaxValue)
                .SetTrans(transitionType)
                .SetEase(Tween.EaseType.InOut);

            for (int i = 0; i < trapPositions.Count; i++)
            {
                var targetPos = trapPositions[(i + 1) % trapPositions.Count].GlobalPosition;
                tween.TweenProperty(this, "position", targetPos, MovementTime);
            }
        }
    }
}
