using Godot;

namespace PongCSharp.UserInterface;

public abstract partial class MenuButtonBase : Button
{
    [Export]
    private AudioStreamPlayer2D? _clickSoundAudioPlayer;

    public override void _Ready()
    {
        base._Ready();
        _clickSoundAudioPlayer = GetNode<AudioStreamPlayer2D>("ClickSound");

        ButtonUp += MenuButtonBase_OnButtonUp;
        _clickSoundAudioPlayer.Finished += ClickSound_OnFinished;
    }

    private void MenuButtonBase_OnButtonUp() => _clickSoundAudioPlayer?.Play();

    private void ClickSound_OnFinished() => HandleButtonUp();

    public abstract void HandleButtonUp();
}