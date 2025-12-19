using Godot;
using System.Threading.Tasks;

public partial class Intro : CanvasLayer
{
    private const float DISPLAY_DURATION = 2.0f;
    private const float FADE_DURATION = 1f;

    [Export] private TextureRect godotEngineImage;
    [Export] private VBoxContainer gameVboxContainer;

    private Tween tween;

    public override void _EnterTree()
    {
        godotEngineImage.Visible = false;
        gameVboxContainer.Visible = false;
        base._EnterTree();
    }

    public override void _Ready()
    {
        PlayIntroSequence();
    }

    private async void PlayIntroSequence()
    {
        await FadeInGodotLogo();
        await ToSignal(GetTree().CreateTimer(DISPLAY_DURATION), SceneTreeTimer.SignalName.Timeout);
        await FadeOutGodotLogo();
        await FadeInGameLogo();
        await ToSignal(GetTree().CreateTimer(DISPLAY_DURATION), SceneTreeTimer.SignalName.Timeout);
        await FadeOutGameLogo();

        GameManager.ChangeGameState(GameState.Start, GameState.Menu);
        this.QueueFree();
    }

    private async Task FadeInGodotLogo()
    {
        godotEngineImage.Modulate = new Color(1, 1, 1, 0);
        godotEngineImage.Visible = true;

        tween = CreateTween();
        tween.TweenProperty(godotEngineImage, "modulate:a", 1.0f, FADE_DURATION);
        await ToSignal(tween, Tween.SignalName.Finished);
    }

    private async Task FadeOutGodotLogo()
    {
        tween = CreateTween();
        tween.TweenProperty(godotEngineImage, "modulate:a", 0.0f, FADE_DURATION);
        await ToSignal(tween, Tween.SignalName.Finished);
        godotEngineImage.Visible = false;
    }
    
    private async Task FadeInGameLogo()
    {
        gameVboxContainer.Modulate = new Color(1, 1, 1, 0);
        gameVboxContainer.Visible = true;

        tween = CreateTween();
        tween.TweenProperty(gameVboxContainer, "modulate:a", 1.0f, FADE_DURATION);
        await ToSignal(tween, Tween.SignalName.Finished);
    }

    private async Task FadeOutGameLogo()
    {
        tween = CreateTween();
        tween.TweenProperty(gameVboxContainer, "modulate:a", 0.0f, FADE_DURATION);
        await ToSignal(tween, Tween.SignalName.Finished);
        gameVboxContainer.Visible = false;
    }
}
