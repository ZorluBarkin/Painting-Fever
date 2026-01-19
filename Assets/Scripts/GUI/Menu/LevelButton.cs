using Godot;

public partial class LevelButton : TextureButton
{
    public Level level = null;
    public Difficulty Difficulty;
    private int Index;

    // TODO: When progress system is implemented, set this based on last completed level index
    public bool unlocked = false;
    public bool completed = false;
    public float score;

    [Export] private TextureRect lockedIndicator;
    [Export] private Godot.Collections.Array<TextureRect> scoreIndicators;

    private void Initialize()
    {
        TextureNormal = level.LevelThumbnail;
        Difficulty = level.Difficulty;
        Index = level.LevelIndex;
    }

    public void SetLevel(Level levelData)
    {
        level = levelData;
        Initialize();
    }

    private void OnLevelButtonPressed()
    {
        LevelManager.Instance.InstantiateLevel(Difficulty, Index);
    }

    private void SetScoreVisuals()
    {
        
        if(score >= 100f) // perfect, 3 disco
        {
            completed = true;
            foreach (var indicator in scoreIndicators)
                indicator.Visible = true;
        }
        else if (score >= 90f) // good 2 disco
        {
            completed = true;
            for (int i = 0; i <= scoreIndicators.Count / 2; i++)
                scoreIndicators[i].Visible = true;
        }
        else if (score >= 75f) // ok 1 disco
        {
            completed = true;
            for (int i = 0; i <= scoreIndicators.Count / 3; i++)
                scoreIndicators[i].Visible = true;
        }
        else // no disco, not completed 
        {
            completed = false;
            foreach (var indicator in scoreIndicators)
                indicator.Visible = false;
        }
    }

    private void SetLockedVisual()
    {
        lockedIndicator.Visible = true;
        foreach (var indicator in scoreIndicators)
            indicator.Visible = false;
        Disabled = true;
        unlocked = false;
    }
}
