using Godot;
using System;

public partial class GUI : CanvasLayer
{
    public static GUI Instance { get; private set; }

    [Export] public MainMenu MainMenu { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }
}
