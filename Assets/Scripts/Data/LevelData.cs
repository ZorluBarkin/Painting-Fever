using Godot;

public partial class LevelData : Resource
{
    [Export] public Godot.Collections.Array<PackedScene> EasyLevelScenes {get; private set; }
    [Export] public Godot.Collections.Array<PackedScene> MediumLevelScenes { get; private set; }
    [Export] public Godot.Collections.Array<PackedScene> HardLevelScenes { get; private set; }

    #if DEBUG
    [Export] public Godot.Collections.Array<PackedScene> DevLevels { get; private set; }
    #endif

    public LevelData() { }
}
