using Godot;
using System.Threading.Tasks;

public partial class Intro : CanvasLayer
{
    [Export] private VBoxContainer godotVboxContainer;
    private const float DISPLAY_DURATION = 2.0f;
    private const float FADE_DURATION = 1f;

    public override void _Ready()
    {
        PlayIntroSequence();
    }

    private async void PlayIntroSequence()
    {
        await FadeInGodotLogo();
        await ToSignal(GetTree().CreateTimer(DISPLAY_DURATION), SceneTreeTimer.SignalName.Timeout);
        await FadeOutGodotLogo();
    }

    private async Task FadeInGodotLogo()
    {
        godotVboxContainer.Modulate = new Color(1, 1, 1, 0);
        godotVboxContainer.Visible = true;

        Tween tween = CreateTween();
        tween.TweenProperty(godotVboxContainer, "modulate:a", 1.0f, FADE_DURATION);
        await ToSignal(tween, Tween.SignalName.Finished);
    }

    private async Task FadeOutGodotLogo()
    {
        Tween tween = CreateTween();
        tween.TweenProperty(godotVboxContainer, "modulate:a", 0.0f, FADE_DURATION);
        await ToSignal(tween, Tween.SignalName.Finished);
        godotVboxContainer.Visible = false;
    }
}
