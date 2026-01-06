using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class LevelManager : Node
{
	public static LevelManager Instance { get; private set; }

	public Level CurrentLevel { get; private set; }
	public PackedScene TargetLevel { get; private set; }

	[ExportCategory("Game Data")]
	[Export] public LevelData LevelData { get; private set; }
	[Export] public ColorData ColorData { get; private set; }

	[Export] public float PausedTimeScale { get; private set; } = 0.05f;

	public event Action<Level> LevelLoaded;
	public event Action<Level> LevelUnloaded;

	public LevelManager() { Instance = this; }

	#if DEBUG
	public override void _Input(InputEvent @event)
	{
		if(@event.IsActionPressed("debug_reload_level")) // keybind: R
		{
			UnloadCurrentLevel();
			Instance.InstantiateLevel(CurrentLevel.Difficulty, 0);
		}
		base._Input(@event);
	}
	#endif

	public void InstantiateLevel(Difficulty difficulty, int index)
	{
		TargetLevel = null;
		switch (difficulty)
		{
			case Difficulty.Easy:
				if (index < LevelData.EasyLevelScenes.Count)
					TargetLevel = LevelData.EasyLevelScenes[index];
				break;
			case Difficulty.Medium:
				if (index < LevelData.MediumLevelScenes.Count)
					TargetLevel = LevelData.MediumLevelScenes[index];
				break;
			case Difficulty.Hard:
				if (index < LevelData.HardLevelScenes.Count)
					TargetLevel = LevelData.HardLevelScenes[index];
				break;
			case Difficulty.EasterEgg:
				if (index < LevelData.EasterEggLevelScenes.Count)
					TargetLevel = LevelData.EasterEggLevelScenes[index];
				break;
		}
		// TODO: Temporary remove once levels are implemented
		TargetLevel = LevelData.DevLevels[0];

		if (TargetLevel == null)
		{
			GD.PrintErr($"Level Doesn't Exist for Difficulty: {difficulty}, Index: {index}");
			return;
		}

		CurrentLevel = TargetLevel.Instantiate<Level>();
		CurrentLevel.LevelIndex = index;
		GameManager.Instance.GameSceneRoot.AddChild(CurrentLevel);
		LevelLoaded.Invoke(CurrentLevel);
	}

	public void UnloadCurrentLevel()
	{
		if (CurrentLevel == null) return;

		LevelUnloaded.Invoke(CurrentLevel);
		CurrentLevel.QueueFree();
		CurrentLevel = null;
	}

	public void RestartCurrentLevel()
	{
		if (CurrentLevel == null) return;

		int currentIndex = CurrentLevel.LevelIndex;
		Difficulty currentDifficulty = CurrentLevel.Difficulty;

		UnloadCurrentLevel();
		InstantiateLevel(currentDifficulty, currentIndex);
	}
}
