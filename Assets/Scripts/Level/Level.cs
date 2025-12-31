using Godot;

public partial class Level : Node
{
    [ExportCategory("Level Metadata")]
    [Export] public int LevelIndex { get; set; }
    [Export] public Difficulty Difficulty { get; private set; }
    [Export] public float MaxDistance { get; private set; } = 3000f;
    [Export] public float RequiredProgress { get; private set; } = 2000f;
    [Export] public float LaneOffset { get; private set; } = 0f;


    [ExportCategory("Music Section")]
    [Export] public AudioStream LevelMusic {get; private set; }
    [Export] public float MusicBPM { get; private set; } = 0f;

    [ExportCategory("In-Level Godot Nodes")]
    [Export] public Marker2D CentralLinePoint { get; private set; }
    //[Export] public PackedScene PlayerObjectScene { get; private set; }

    // Runtime variables
    public PlayerObject PlayerObject { get; private set; }
    public float Progress { get; private set; } = 0f;
    public float Distance { get; private set; } = 0f;
    public bool LevelStarted { get; private set; } = false;

    public override void _EnterTree()
    {
        if (!ReadyLevel())
            return;
            // NOTE: maybe return to main menu or show error message
        
        PlayerObject.LaneCentrePoint = CentralLinePoint;
        SoundManager.Instance.PlayLevelMusic(LevelMusic);
        
        LevelStarted = true;
        base._Ready();
    }

    /// <summary>
    /// Check if level is ready to be played <br/>
    /// Returns false if there are errors
    /// </summary>
    /// <returns></returns>
    private bool ReadyLevel()
    {
        bool noErrors = true;
        if (LevelMusic == null)
        {
            GD.PrintErr("No level music assigned!");
            noErrors = false;
        }

        if (MusicBPM <= 0)
        {
            GD.PrintErr("Invalid BPM assigned for level music!");
            noErrors = false;
        }

        // TODO: Temporary remove once dynamic calculations via markers are added
        if (LaneOffset <= 0f)
        {
            GD.PrintErr("Invalid Lane Offset assigned!");
            noErrors = false;
        }
        else
            LaneOffset -= PlayerObject.OBJECT_RADIUS;
        
        if (!GetPlayerObject())
        {
            GD.PrintErr("No PlayerObject found in level!");
            noErrors = false;
        }

        return noErrors;
    }

    private bool GetPlayerObject()
    {
        foreach (Node child in GetChildren())
        {
            if (child is PlayerObject player)
            {
                PlayerObject = player;
                SetPlayerVariables();
                return true;
            }
        }

        return false;
    }

    private void SetPlayerVariables()
    {
        PlayerObject.SetLevelBasedVariables(this);
    } 

    public void UpdateProgress(float distanceTraveled, bool painting)
    {
        Distance = distanceTraveled;
        if (painting)
            Progress = distanceTraveled;
    }

    private void CheckLevelEnd()
    {
        if (Distance >= MaxDistance)
        {
            if (Progress >= RequiredProgress)
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
}
