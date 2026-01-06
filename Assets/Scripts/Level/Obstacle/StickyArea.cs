using Godot;
using System;

public partial class StickyArea : Area2D
{
    [Export] public float SlowdownMultiplier { get; private set; }
    [Export] public float persistDuration = 2.0f;

    private bool isPlayerInside = false;
    private bool effectActive = false;
    private float timeSinceExit = 0.0f;
    private float slowedSpeed = 0.0f;
    private PlayerObject player;

    public override void _Ready()
    {
        SlowdownMultiplier = LevelManager.Instance.LevelData.stickySlowdownMultiplier;
        base._Ready();
    }

    public override void _Process(double delta)
    {
        // Only count time if player has exited but effect is still active
        if (!isPlayerInside && effectActive)
        {
            timeSinceExit += (float)delta;
            
            // Gradually restore speed based on time progress
            float progress = Mathf.Clamp(timeSinceExit / persistDuration, 0.0f, 1.0f);
            player.MoveSpeed = Mathf.Lerp(slowedSpeed, player.OriginalMoveSpeed, progress);
            
            if (timeSinceExit >= persistDuration)
                RemoveEffect();
        }
    }

    public void OnBodyEntered(Node2D body)
    {
        if (body is PlayerObject @player)
        {
            this.player = @player;
            isPlayerInside = true;
            timeSinceExit = 0.0f; // Reset timer when entering
            
            if (!effectActive)
                ApplyEffect();
        }
    }

    public void OnBodyExited(Node2D body)
    {
        if (body is PlayerObject)
        {
            isPlayerInside = false;
            timeSinceExit = 0.0f; // Start counting from 0 when exiting
        }
    }

    private void ApplyEffect()
    {
        if (player == null) return;
        
        player.Sticked = true;
        player.MoveSpeed *= 1 - SlowdownMultiplier;
        slowedSpeed = player.MoveSpeed;
        effectActive = true;
    }

    private void RemoveEffect()
    {
        if (player == null) return;
        
        player.Sticked = false;
        player.MoveSpeed = player.OriginalMoveSpeed;
        effectActive = false;
        timeSinceExit = 0.0f;
    }
}
