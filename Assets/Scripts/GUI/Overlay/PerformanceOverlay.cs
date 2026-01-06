using Godot;

public partial class PerformanceOverlay : CenterContainer, IEventSubscriber
{
    [Export] private Label gameVersionLabel;
    [Export] private Label fpsValueLabel;
    [Export] private Label frameTimeValueLabel;

    public override void _Ready()
    {
        // TODO: change when version control is added.
        gameVersionLabel.Text = "Alpha " + "0.1.0";
        ((IEventSubscriber)this).SubscribeToEvents();
        Visible = SettingsManager.Instance.SettingsData.ShowPerformanceOverlay;
    }

    public override void _Process(double delta)
    {
        if (Visible) // idea: can make it run always but only update when visible?
        {
            // need max performance for accurate readings, so "...ms or FPS: " are added through scenes.
            fpsValueLabel.Text = Engine.GetFramesPerSecond().ToString("F2");
            frameTimeValueLabel.Text = (delta * 1000f).ToString("F2");
        }
    }

    public override void _ExitTree()
    {
        ((IEventSubscriber)this).UnsubscribeFromEvents();
    }

    void IEventSubscriber.SubscribeToEvents()
    {
        SettingsManager.Instance.SettingsChanged += OnSettingsChanged;
    }

    void IEventSubscriber.UnsubscribeFromEvents()
    {
        SettingsManager.Instance.SettingsChanged -= OnSettingsChanged;
    }

    private void OnSettingsChanged()
    {
        Visible = SettingsManager.Instance.SettingsData.ShowPerformanceOverlay;
    }
}
