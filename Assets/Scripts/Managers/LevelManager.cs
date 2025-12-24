using Godot;

public partial class LevelManager : Node
{
    public static LevelManager Instance { get; private set; }

    public Level CurrentLevel { get; private set; }
    public PackedScene TargetLevel { get; private set; }

    [Export] public LevelData LevelData { get; private set; }
    [Export] public ColorData ColorData { get; private set; }

    public LevelManager() { Instance = this; }

    public override void _Ready()
    {
        
    }
}
