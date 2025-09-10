using Godot;

namespace PongCSharp.UserInterface;

public partial class OptionsMenuButton : MenuButtonBase
{
    public override void _Ready()
    {
        base._Ready();
        Disabled = true; // Disabled until we actually have options to care about
    }

    public override void HandleButtonUp()
    {
        GD.Print($"{Name} clicked");
    }
}
