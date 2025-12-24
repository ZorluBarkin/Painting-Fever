using Godot;

public partial class ColorData : Resource
{
    // TODO: Manually add later on
    [Export] public Godot.Collections.Dictionary<PlayerColors, Color> PlayerColorMap = new Godot.Collections.Dictionary<PlayerColors, Color>()
    {
        { PlayerColors.Grey, Colors.Gray },
        { PlayerColors.Red, Colors.Red },
        { PlayerColors.Blue, Colors.Blue },
        { PlayerColors.Green, Colors.Green }
    };

    public ColorData() { }
}
