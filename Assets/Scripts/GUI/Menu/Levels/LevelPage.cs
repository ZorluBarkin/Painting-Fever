using Godot;

public partial class LevelPage : VBoxContainer
{
    [Export] public Difficulty Difficulty;
    [Export] private LevelRow UpperlevelRow;
    [Export] private LevelRow LowerlevelRow;

    public bool AddLevelButton(LevelButton levelButton)
    {
        if (UpperlevelRow.AddLevelButton(levelButton))
        {
            levelButton.Difficulty = Difficulty;
            return true;
        }
        else if (LowerlevelRow.AddLevelButton(levelButton))
        {
            levelButton.Difficulty = Difficulty;
            return true;
        }
        
        return false;
    }
}
