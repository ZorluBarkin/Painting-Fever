using System;
using Godot;

public partial class LevelSelector : Control
{
    [ExportCategory("Level Containers")]
    [Export] private VBoxContainer easyLevelsContainer;
    [Export] private VBoxContainer mediumLevelsContainer;
    [Export] private VBoxContainer hardLevelsContainer;
    [Export] private VBoxContainer easterEggLevelsContainer;

    [ExportCategory("Page Switch Buttons")]
    [Export] private TextureButton PrevPageButtonScene;
    [Export] private TextureButton NextPageButtonScene;

    [ExportCategory("Level Button Colors")]
    [Export] private Color LockedLevelColor = Colors.Gray;
    [Export] private Color completedLevelColor = Colors.Green;
    [Export] private Color notCompletedLevelColor = Colors.Yellow;
    [Export] private Color EasterEggLevelColor = Colors.Purple;

    private Difficulty selectedDifficulty;
    private LevelData levelData;

    public override void _Ready()
    {
        levelData = LevelManager.Instance.LevelData;
        selectedDifficulty = Difficulty.Easy;
        PrevPageButtonScene.Disabled = true;
        SelectLevelPage(selectedDifficulty);
        base._Ready();
    }

    private void CreateLevelButtons()
    {
        
    }

    private void OnLevelButtonPressed(int levelIndex)
    {
        GD.Print($"Level {levelIndex} selected for difficulty {selectedDifficulty}");
        LevelManager.Instance.InstantiateLevel(selectedDifficulty, levelIndex);
        GetTree().ChangeSceneToFile("res://Assets/Scenes/GameScene.tscn");
    }

    private void SelectLevelPage(Difficulty selectedDifficulty)
    {
        switch (selectedDifficulty)
        {
            case Difficulty.Easy:
                easyLevelsContainer.Visible = true;
                mediumLevelsContainer.Visible = false;
                hardLevelsContainer.Visible = false;
                easterEggLevelsContainer.Visible = false;
                break;
            case Difficulty.Medium:
                easyLevelsContainer.Visible = false;
                mediumLevelsContainer.Visible = true;
                hardLevelsContainer.Visible = false;
                easterEggLevelsContainer.Visible = false;
                break;
            case Difficulty.Hard:
                easyLevelsContainer.Visible = false;
                mediumLevelsContainer.Visible = false;
                hardLevelsContainer.Visible = true;
                easterEggLevelsContainer.Visible = false;
                break;
            case Difficulty.EasterEgg:
                easyLevelsContainer.Visible = false;
                mediumLevelsContainer.Visible = false;
                hardLevelsContainer.Visible = false;
                easterEggLevelsContainer.Visible = true;
                break;
        }
    }

    private void OnPrevPageButtonPressed()
    {
        int targetLevel = (int)selectedDifficulty - 1;
        
        if (targetLevel < 0) targetLevel = Enum.GetNames(typeof(Difficulty)).Length - 1;
        else selectedDifficulty = (Difficulty)(targetLevel % Enum.GetNames(typeof(Difficulty)).Length);

        SelectLevelPage(selectedDifficulty);

        if (Difficulty.Easy == selectedDifficulty)
        {
            PrevPageButtonScene.Disabled = true;
            NextPageButtonScene.Disabled = false;
        }
        else
        {
            PrevPageButtonScene.Disabled = false;
            NextPageButtonScene.Disabled = false;
        }
    }

    private void OnNextPageButtonPressed()
    {
        int targetLevel = (int)selectedDifficulty + 1;
        
        if (targetLevel >= Enum.GetNames(typeof(Difficulty)).Length) targetLevel = 0;
        else selectedDifficulty = (Difficulty)(targetLevel % Enum.GetNames(typeof(Difficulty)).Length);

        SelectLevelPage(selectedDifficulty);

        if (Difficulty.EasterEgg == selectedDifficulty)
        {
            NextPageButtonScene.Disabled = true;
            PrevPageButtonScene.Disabled = false;
        }
        else
        {
            NextPageButtonScene.Disabled = false;
            PrevPageButtonScene.Disabled = false;
        }
    }

    private void OnExitButtonPressed()
    {
        Visible = false;
    }
}
