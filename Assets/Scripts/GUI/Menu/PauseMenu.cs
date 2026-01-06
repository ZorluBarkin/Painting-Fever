using System.Runtime.InteropServices;
using Godot;

public partial class PauseMenu : CenterContainer
{
    [ExportCategory("Volume Minimums")]
    [Export] private float MasterVolumeMin = 20f;
    [Export] private float MusicVolumeMin = 20f;
    [Export] private float SFXVolumeMin = 0f;

    [ExportCategory("Volume Sliders and LineEdits")]
    [Export] private HSlider masterVolumeSlider;
    [Export] private HSlider musicVolumeSlider;
    [Export] private HSlider sfxVolumeSlider;

    [Export] private LineEdit masterVolumeLineEdit;
    [Export] private LineEdit musicVolumeLineEdit;
    [Export] private LineEdit sfxVolumeLineEdit;

    [ExportCategory("Keybind LineEdits")]
    [Export] private LineEdit upKeyLineEdit;
    [Export] private LineEdit downKeyLineEdit;
    [Export] private LineEdit paint1KeyLineEdit;
    [Export] private LineEdit paint2KeyLineEdit;
    [Export] private LineEdit paint3KeyLineEdit;

    private bool SettingsChanged = false;

    private RegEx regExNumeric = new();

    public override void _Ready()
    {
        regExNumeric.Compile("[^0-9]");
        LoadSettings();
    }

    private void LoadSettings()
    {
        // TODO: Add Config manager then connect settings to it
        SettingsData data = SettingsManager.Instance.SettingsData;
        masterVolumeSlider.Value = data.MasterVolume;
        musicVolumeSlider.Value = data.MusicVolume;
        sfxVolumeSlider.Value = data.EffectsVolume;

        masterVolumeLineEdit.Text = masterVolumeSlider.Value.ToString();
        musicVolumeLineEdit.Text = musicVolumeSlider.Value.ToString();
        sfxVolumeLineEdit.Text = sfxVolumeSlider.Value.ToString();
    }

    private void OnVisibilityChanged()
    {
        if (Visible)
            Engine.TimeScale = LevelManager.Instance.PausedTimeScale;
        else
        {
            if (SettingsChanged)
            {
                SettingsChanged = false;
                SettingsManager.Instance.SettingsData.ChangeAudioSettings((int)masterVolumeSlider.Value, (int)musicVolumeSlider.Value, (int)sfxVolumeSlider.Value);
                SettingsManager.Instance.SaveSettings();
            }
            Engine.TimeScale = 1f;
        }
    }

    private void OnContinueButtonPressed()
    {
        Visible = false;
    }

    private void OnRestartButtonPressed()
    {
        Visible = false;
        LevelManager.Instance.RestartCurrentLevel();
    }

    private void OnAudioToggle(bool toggled)
    {
        SettingsChanged = true;
    }

    private void OnMasterSliderValueChanged(float value)
    {
        masterVolumeLineEdit.Text = value.ToString();
        SettingsChanged = true;
    }

    private void OnMasterEditTextSubmitted(string newText)
    {
        newText = regExNumeric.Sub(newText, "", true);
        if (!string.IsNullOrEmpty(newText))
        {     
            float clampedValue = Mathf.Clamp(float.Parse(newText), MasterVolumeMin, 100f);
            masterVolumeLineEdit.Text = clampedValue.ToString();
            masterVolumeSlider.Value = clampedValue;
            SettingsChanged = true;
        }

    }

    private void OnMusicSliderValueChanged(float value)
    {
        musicVolumeLineEdit.Text = value.ToString();
        SettingsChanged = true;
    }

    private void OnMusicEditTextSubmitted(string newText)
    {
        newText = regExNumeric.Sub(newText, "", true);
        if (!string.IsNullOrEmpty(newText))
        {
            float clampedValue = Mathf.Clamp(float.Parse(newText), MusicVolumeMin, 100f);
            musicVolumeLineEdit.Text = clampedValue.ToString();
            musicVolumeSlider.Value = clampedValue;
            SettingsChanged = true;
        }
    }

    private void OnSFXSliderValueChanged(float value)
    {
        sfxVolumeLineEdit.Text = value.ToString();
        SettingsChanged = true;
    }

    private void OnSFXEditTextSubmitted(string newText)
    {
        newText = regExNumeric.Sub(newText, "", true);
        if (!string.IsNullOrEmpty(newText))
        {
            float clampedValue = Mathf.Clamp(float.Parse(newText), SFXVolumeMin, 100f);
            sfxVolumeLineEdit.Text = clampedValue.ToString();
            sfxVolumeSlider.Value = clampedValue;
            SettingsChanged = true;
        }
    }

    private void OnUpLineEditTextSubmitted(string newText)
    {
        upKeyLineEdit.Text = newText;
        SettingsChanged = true;
    }

    private void OnDownLineEditTextSubmitted(string newText)
    {
        downKeyLineEdit.Text = newText;
        SettingsChanged = true;
    }

    private void OnPaint1LineEditTextSubmitted(string newText)
    {
        paint1KeyLineEdit.Text = newText;
        SettingsChanged = true;
    }

    private void OnPaint2LineEditTextSubmitted(string newText)
    {
        paint2KeyLineEdit.Text = newText;
        SettingsChanged = true;
    }

    private void OnPaint3LineEditTextSubmitted(string newText)
    {
        paint3KeyLineEdit.Text = newText;
        SettingsChanged = true;
    }
}
