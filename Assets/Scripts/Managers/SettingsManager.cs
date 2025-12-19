using Godot;
using System;

public partial class SettingsManager : Node, IEventSubscriber
{
    public static SettingsManager Instance { get; private set; }

    //[Export] public AudioSettings AudioSettings { get; private set; }
    //[Export] public VideoSettings VideoSettings { get; private set; }
    //[Export] public ControlSettings Contro    lSettings { get; private set; }

    public override void _Ready()
    {
        Instance = this;
        ((IEventSubscriber)this).SubscribeToEvents();
    }

    public override void _ExitTree()
    {
        ((IEventSubscriber)this).UnsubscribeFromEvents();
    }

    void IEventSubscriber.SubscribeToEvents()
    {
    }
    
    void IEventSubscriber.UnsubscribeFromEvents()
    {
    }
}
