using Godot;

public partial class LevelData : Resource
{
    [Export] public Godot.Collections.Array<PackedScene> EasyLevelScenes {get; private set; }
    [Export] public Godot.Collections.Array<PackedScene> MediumLevelScenes { get; private set; }
    [Export] public Godot.Collections.Array<PackedScene> HardLevelScenes { get; private set; }
    [Export] public Godot.Collections.Array<PackedScene> EasterEggLevelScenes { get; private set; }
    [Export] public Godot.Collections.Dictionary<Difficulty, float> DifficultyToSpeedMap = new()
    {
        { Difficulty.Easy, 300f },
        { Difficulty.Medium, 600f },
        { Difficulty.Hard, 1000f },
        { Difficulty.EasterEgg, 1000f }
    };

    #if DEBUG
    [Export] public Godot.Collections.Array<PackedScene> DevLevels { get; private set; }
    #endif

    public LevelData() { }

    public float GetMoveSpeed(Difficulty difficulty)
    {
        return DifficultyToSpeedMap[difficulty];
    }
}
