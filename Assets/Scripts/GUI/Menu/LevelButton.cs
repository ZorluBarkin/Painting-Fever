using Godot;

public partial class LevelButton : Control
{
    public Level level = null;
    public Difficulty Difficulty;
    private int Index;

    // TODO: When progress system is implemented, set this based on last completed level index
    public bool unlocked = false;
    public bool completed = false;  
    public float score;

    [Export] private Color lockedColor;
    [Export] private TextureRect lockedIndicator;
    [Export] private Godot.Collections.Array<TextureRect> scoreIndicators;

    [Export] private TextureButton LevelSelectButton;

    private void Initialize()
    {
        LevelSelectButton.TextureNormal = level.LevelThumbnail;
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
        LevelData levelData = LevelManager.Instance.LevelData;
        // perfect, 3 disco
        if(score >= levelData.levelPerfectThreshold)
        {
            completed = true;
            foreach (var indicator in scoreIndicators)
                indicator.Visible = true;
        }
        // good 2 disco
        else if (score >= levelData.levelSuccessThreshold)
        {
            completed = true;
            for (int i = 0; i <= scoreIndicators.Count / 2; i++)
                scoreIndicators[i].Visible = true;
        }
        // ok 1 disco
        else if (score >= levelData.levelPassedThreshold)
        {
            completed = true;
            for (int i = 0; i <= scoreIndicators.Count / 3; i++)
                scoreIndicators[i].Visible = true;
        }
        // no disco, not completed
        else
        {
            completed = false;
            foreach (var indicator in scoreIndicators)
                indicator.Visible = false;
        }
    }

    private void SetLocked(bool locked)
    {
        if (locked)
        {
            unlocked = false;
            lockedIndicator.Visible = true;
            foreach (var indicator in scoreIndicators)
                indicator.Visible = false;

            LevelSelectButton.Disabled = true;
            LevelSelectButton.SelfModulate = lockedColor;
        }
        else
        {
            unlocked = true;
            lockedIndicator.Visible = false;
            LevelSelectButton.Disabled = false;
            LevelSelectButton.SelfModulate = Colors.White;
            SetScoreVisuals();
        }
    }
}
