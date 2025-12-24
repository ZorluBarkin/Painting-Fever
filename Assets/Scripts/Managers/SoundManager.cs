using Godot;
using System;

public partial class SoundManager : Node
{
    public static SoundManager Instance { get; private set; }
    [Export] public AudioStreamPlayer MenuMusicPlayer { get; private set; }

    /// <summary>
    /// Player for music in the levels.
    /// </summary>
    [Export] public AudioStreamPlayer LevelMusicPlayer { get; private set; }
    
    /// <summary>
    /// Player for sound effects, effects that include footsteps, explosions, and other in-game sounds.
    /// </summary>
    [Export] public AudioStreamPlayer EffectsPlayer { get; private set; }

    /// <summary>
    /// Master volume that affects all sounds. 0-100 range.
    /// </summary>
    public float MasterVolume { get; private set; }
    public float MusicVolume { get; private set; }
    public float SfxVolume { get; private set; }

    public SoundManager() { Instance = this; }
    public override void _Ready()
    {
        SetVolumes();
    }

    public void SetVolumes()
    {
        float masterEffect = MasterVolume / 100f;
        MenuMusicPlayer.VolumeDb = Mathf.LinearToDb(masterEffect * MusicVolume);
        LevelMusicPlayer.VolumeDb = Mathf.LinearToDb(masterEffect * MusicVolume);
        EffectsPlayer.VolumeDb = Mathf.LinearToDb(masterEffect * SfxVolume);
    }

    public void PlayLevelMusic(AudioStream music)
    {
        if (MenuMusicPlayer.Playing)
            MenuMusicPlayer.Stop();

        LevelMusicPlayer.Stream = music;
        LevelMusicPlayer.Play();
    }

    public void PlaySfx(AudioStream sfx)
    {
        if (EffectsPlayer.Playing)
            EffectsPlayer.Stop();

        EffectsPlayer.Stream = sfx;
        EffectsPlayer.Play();
    }
}