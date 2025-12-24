using System.Diagnostics.CodeAnalysis;
using Godot;

public partial class Level : Node
{
    [Export] public Node2D centralLinePoint;
    [Export] public PlayerObject playerObject;

    public float Progress { get; private set; } = 0f;
    public float Distance { get; private set; } = 0f;

    [Export] float maxDistance = 3000f;
    [Export] float requiredProgress = 2000f;

    [Export] public AudioStream LevelMusic;
    [Export] public float MusicBPM;

    public bool LevelStarted { get; private set; } = false;

    public override void _EnterTree()
    {
        #if DEBUG
        if (CheckErrors())
            return;
        #endif

        playerObject.lineDefiner = centralLinePoint;
        SoundManager.Instance.PlayLevelMusic(LevelMusic);
        
        LevelStarted = true;
        base._Ready();
    }

    public void UpdateProgress(float distanceTraveled, bool painting)
    {
        Distance = distanceTraveled;
        if (painting)
            Progress = distanceTraveled;
    }

    private void CheckLevelEnd()
    {
        if (Distance >= maxDistance)
        {
            if (Progress >= requiredProgress)
            {
                #if DEBUG
                GD.Print("Level Complete!");
                #endif
                // TODO: add end popup
            }
            else 
            {
                #if DEBUG
                GD.Print("Level Failed!");
                #endif
                // TODO: add end popups
            }
        }
    }

    private bool CheckErrors()
    {
        bool hasErrors = false;
        if (LevelMusic == null)
        {
            GD.PrintErr("No level music assigned!");
            hasErrors = true;
        }

        if (MusicBPM <= 0)
        {
            GD.PrintErr("Invalid BPM assigned for level music!");
            hasErrors = true;
        }
        return hasErrors;
    }
}
