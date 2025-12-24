using System;
using Godot;

public partial class MusicData : Node
{
    [Export] public Godot.Collections.Dictionary<int, AudioStream> SlowSongs;
    [Export] public Godot.Collections.Dictionary<int, AudioStream> MediumSongs;
    [Export] public Godot.Collections.Dictionary<int, AudioStream> FastSongs;
    private RandomNumberGenerator rng = new();

    public AudioStream GetRandomSong(int musicIndex, MusicSpeed speed)
    {
        return speed switch
        {
            MusicSpeed.Slow => SlowSongs[musicIndex],
            MusicSpeed.Medium => MediumSongs[musicIndex],
            MusicSpeed.Fast => FastSongs[musicIndex],
            _ => null,
        };
    }
}
