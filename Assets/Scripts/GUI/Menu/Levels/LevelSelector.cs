using System;
using Godot;

public partial class LevelSelector : Control
{
    [ExportCategory("Level Containers")]
    [Export] private LevelPage easyLevelsContainer;
    [Export] private LevelPage mediumLevelsContainer;
    [Export] private LevelPage hardLevelsContainer;
    [Export] private LevelPage easterEggLevelsContainer;

    [ExportCategory("Page Switch Buttons")]
    [Export] private TextureButton PrevPageButtonScene;
    [Export] private TextureButton NextPageButtonScene;

    [ExportCategory("Level Button PackedScene")]
    [Export] private PackedScene LevelButtonScene;

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
        // TODO: Implement level button creation logic, applicable when multiple levels are added
        // read data
        //switch diff
        //easterEggLevelsContainer.AddLevelButton(LevelButtonScene.Instantiate<LevelButton>()); like so
    }

    private void CloseLevelSelector()
    {
        Visible = false;
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

    public void InstantiateLevel(Difficulty difficulty, int index)
    {
        LevelManager.Instance.InstantiateLevel(difficulty, index);
        CloseLevelSelector();
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
        CloseLevelSelector();
    }
}
