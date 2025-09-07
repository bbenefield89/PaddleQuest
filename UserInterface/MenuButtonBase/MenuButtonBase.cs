using Godot;

namespace PongCSharp.UserInterface;

public abstract partial class MenuButtonBase : Button
{
    // Fields
    private const string CLICK_SOUND_NODE_NAME = "ClickSound";

    // Exports
    [Export]
    private AudioStreamPlayer2D? _clickSoundAudioPlayer;

    // Lifecycles
    public override void _Ready()
    {
        base._Ready();
        SetExportProps();
        Subscribe();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        Unsubscribe();
    }

    // Setup
    private void SetExportProps()
        => _clickSoundAudioPlayer ??= GetNode<AudioStreamPlayer2D>(CLICK_SOUND_NODE_NAME);

    private void Subscribe()
    {
        ButtonUp += MenuButtonBase_OnButtonUp;
        _clickSoundAudioPlayer!.Finished += ClickSound_OnFinished;
    }

    private void Unsubscribe()
    {
        ButtonUp -= MenuButtonBase_OnButtonUp;
        _clickSoundAudioPlayer!.Finished -= ClickSound_OnFinished;
    }

    private void MenuButtonBase_OnButtonUp() 
        => _clickSoundAudioPlayer?.Play();

    private void ClickSound_OnFinished()
        => HandleButtonUp();

    // Event Handlers
    public abstract void HandleButtonUp();
}