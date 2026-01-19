using Godot;

public partial class LevelRow : HBoxContainer
{
    [Export] private int maxButtonNumber = 5;

    public bool AddLevelButton(LevelButton levelButton)
    {
        if (GetChildCount() < maxButtonNumber)
        {
            AddChild(levelButton);
            return true;
        }
        return false;
    }
}
