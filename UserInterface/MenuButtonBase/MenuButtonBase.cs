using Godot;

namespace PongCSharp.UserInterface;

public abstract partial class MenuButtonBase : Button
{
    public override void _Ready()
    {
        base._Ready();
        ButtonUp += HandleButtonUp;
    }

    public abstract void HandleButtonUp();
}
