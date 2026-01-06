using Godot;

public partial class SettingsManager : Node
{
    public static SettingsManager Instance { get; private set; }
    [Export] public SettingsData SettingsData { get; private set; }

    public delegate void SettingsChangedEventHandler();
    public event SettingsChangedEventHandler SettingsChanged;

    public override void _Ready()
    {
        Instance = this;
        SettingsData.LoadSettings();
        base._Ready();
    }
    
    public void SaveSettings()
    {   
        SettingsData.SaveSettings();
        // can never be null
        SettingsChanged.Invoke();
    }

    public static string GetIndexFromWindowMode(DisplayServer.WindowMode mode)
    {
        return mode switch
        {
            DisplayServer.WindowMode.ExclusiveFullscreen => "0",
            DisplayServer.WindowMode.Fullscreen => "1",
            DisplayServer.WindowMode.Windowed => "2",
            _ => "0",
        };
    }

    public static DisplayServer.WindowMode GetWindowModeFromIndex(string index)
    {
        return index switch
        {
            "0" => DisplayServer.WindowMode.ExclusiveFullscreen,
            "1" => DisplayServer.WindowMode.Fullscreen,
            "2" => DisplayServer.WindowMode.Windowed,
            _ => DisplayServer.WindowMode.ExclusiveFullscreen,
        };
    }

    public void SetColorblindModeShader(ColorBlindMode mode)
    {
        // choose shader based on mode
    }
}
