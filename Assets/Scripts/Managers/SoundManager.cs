using Godot;
using System;

public partial class SoundManager : Node
{
    public static SoundManager Instance { get; private set; }
    
    [Export] public AudioStreamPlayer2D MusicPlayer { get; private set; }
    [Export] public AudioStreamPlayer2D SfxPlayer { get; private set; }

    public float MusicVolume { get; private set; }
    public float SfxVolume { get; private set; }

    public SoundManager() { Instance = this; }
    public override void _Ready()
    {
        SetVolumes();
    }

    public void SetVolumes()
    {
        MusicPlayer.VolumeDb = Mathf.LinearToDb(MusicVolume);
        SfxPlayer.VolumeDb = Mathf.LinearToDb(SfxVolume);
    }

    public void PlayMusic(AudioStream music)
    {
        MusicPlayer.Stream = music;
        MusicPlayer.Play();
    }

    public void PlaySfx(AudioStream sfx)
    {
        SfxPlayer.Stream = sfx;
        SfxPlayer.Play();
    }
}
