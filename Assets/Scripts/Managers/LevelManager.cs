using Godot;
using System;

public partial class LevelManager : Node
{
	public static LevelManager Instance { get; private set; }

	public Level CurrentLevel { get; private set; }
	public PackedScene TargetLevel { get; private set; }

	[Export] public LevelData LevelData { get; private set; }
	[Export] public ColorData ColorData { get; private set; }

	public event Action<Level> LevelLoaded;

	public LevelManager() { Instance = this; }

	#if DEBUG
	public override void _Input(InputEvent @event)
	{
		if(@event.IsActionPressed("debug_reload_level"))
		{
			CurrentLevel.Free();
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
		GameManager.Instance.GameSceneRoot.AddChild(CurrentLevel);
		LevelLoaded.Invoke(CurrentLevel);
	}
}
