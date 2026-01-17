using Godot;
using System;

public partial class SoundManager : Node, IEventSubscriber
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
	public float MasterVolume { get; private set; } = 50f;
	public float MusicVolume { get; private set; } = 100f;
	public float SfxVolume { get; private set; } = 100f;

	public SoundManager() { Instance = this; }
	public override void _Ready()
	{
		LoadSettings();
		((IEventSubscriber)this).SubscribeToEvents();
	}

	public override void _ExitTree()
	{
		((IEventSubscriber)this).UnsubscribeFromEvents();
	}

	void IEventSubscriber.SubscribeToEvents()
	{
		GameManager.GameStateChanged += OnGameStateChanged;
		SettingsManager.Instance.SettingsChanged += LoadSettings;
	}

	void IEventSubscriber.UnsubscribeFromEvents()
	{
		GameManager.GameStateChanged -= OnGameStateChanged;
		SettingsManager.Instance.SettingsChanged -= LoadSettings;
	}

	private void LoadSettings()
	{
		SettingsData data = SettingsManager.Instance.SettingsData;
		MasterVolume = data.MasterVolume;
		MusicVolume = data.MusicVolume;
		SfxVolume = data.EffectsVolume;
		SetVolumes();
	}

	private void SetVolumes()
	{
		float masterMult = MasterVolume / 100f;
		MenuMusicPlayer.VolumeDb = Mathf.LinearToDb(masterMult * MusicVolume / 100f);
		LevelMusicPlayer.VolumeDb = Mathf.LinearToDb(masterMult * MusicVolume / 100f);
		EffectsPlayer.VolumeDb = Mathf.LinearToDb(masterMult * SfxVolume / 100f);
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

	public void PlayMenuMusic()
	{
		if (LevelMusicPlayer.Playing)
			LevelMusicPlayer.Stop();

		MenuMusicPlayer.Play();
	}

	public void SetMasterVolume(float volume)
	{
		MasterVolume = volume;
		SetVolumes();
	}

	public void SetMusicVolume(float volume)
	{
		MusicVolume = volume;
		SetVolumes();
	}

	public void SetSfxVolume(float volume)
	{
		SfxVolume = volume;
		SetVolumes();
	}

	private void OnGameStateChanged(GameState oldState, GameState newState)
	{
		switch (newState)
		{
			case GameState.Menu:
				GD.Print("Switching to menu music");
				PlayMenuMusic();
				break;
		}
	}
}
