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

    [ExportCategory("Paused Time Scale")]
    [Export] private float pausedTimeScale = 0.25f;

    private RegEx regExNumeric = new();

    public override void _Ready()
    {
        regExNumeric.Compile("[^0-9]");
        InitializeSettings();
    }

    private void InitializeSettings()
    {
        // TODO: Add Config manager then connect settings to it
    }

    private void OnVisibilityChanged()
    {
        if (Visible)
            Engine.TimeScale = pausedTimeScale;
        else
            Engine.TimeScale = 1f;

        GD.Print("Pause Menu Visibility Changed: " + Engine.TimeScale);
    }

    private void OnContinueButtonPressed()
    {
        Visible = false;
        GD.Print("Continue Button Pressed");
    }

    private void OnRestartButtonPressed()
    {
        Visible = false;
        LevelManager.Instance.RestartCurrentLevel();
        GD.Print("Restart Button Pressed");
    }

    private void OnAudioToggle(bool toggled)
    {
        GD.Print("Audio Toggled ", toggled);
    }

    private void OnMasterSliderValueChanged(float value)
    {
        masterVolumeLineEdit.Text = value.ToString();
        GD.Print("Master Slider Value Changed: " + value);
    }

    private void OnMasterEditTextSubmitted(string newText)
    {
        newText = regExNumeric.Sub(newText, "", true);
        if (!string.IsNullOrEmpty(newText))
        {     
            float clampedValue = Mathf.Clamp(float.Parse(newText), MasterVolumeMin, 100f);
            masterVolumeLineEdit.Text = clampedValue.ToString();
            masterVolumeSlider.Value = clampedValue;
            GD.Print("Master Audio Edit Text Changed: " + clampedValue);
        }

    }

    private void OnMusicSliderValueChanged(float value)
    {
        musicVolumeLineEdit.Text = value.ToString();
        GD.Print("Music Slider Value Changed: " + value);
    }

    private void OnMusicEditTextSubmitted(string newText)
    {
        newText = regExNumeric.Sub(newText, "", true);
        if (!string.IsNullOrEmpty(newText))
        {
            float clampedValue = Mathf.Clamp(float.Parse(newText), MusicVolumeMin, 100f);
            musicVolumeLineEdit.Text = clampedValue.ToString();
            musicVolumeSlider.Value = clampedValue;
            GD.Print("Music Audio Edit Text Changed: " + clampedValue);
        }
    }

    private void OnSFXSliderValueChanged(float value)
    {
        sfxVolumeLineEdit.Text = value.ToString();
        GD.Print("SFX Slider Value Changed: " + value);
    }

    private void OnSFXEditTextSubmitted(string newText)
    {
        newText = regExNumeric.Sub(newText, "", true);
        if (!string.IsNullOrEmpty(newText))
        {
            float clampedValue = Mathf.Clamp(float.Parse(newText), SFXVolumeMin, 100f);
            sfxVolumeLineEdit.Text = clampedValue.ToString();
            sfxVolumeSlider.Value = clampedValue;
            GD.Print("SFX Audio Edit Text Changed: " + clampedValue);
        }
    }

    private void OnUpLineEditTextSubmitted(string newText)
    {
        upKeyLineEdit.Text = newText;
        GD.Print("Line Edit Text Changed: " + newText);
    }

    private void OnDownLineEditTextSubmitted(string newText)
    {
        downKeyLineEdit.Text = newText;
        GD.Print("Line Edit Text Changed: " + newText);
    }

    private void OnPaint1LineEditTextSubmitted(string newText)
    {
        paint1KeyLineEdit.Text = newText;
        GD.Print("Line Edit Text Changed: " + newText);
    }

    private void OnPaint2LineEditTextSubmitted(string newText)
    {
        paint2KeyLineEdit.Text = newText;
        GD.Print("Line Edit Text Changed: " + newText);
    }

    private void OnPaint3LineEditTextSubmitted(string newText)
    {
        paint3KeyLineEdit.Text = newText;
        GD.Print("Line Edit Text Changed: " + newText);
    }
}
