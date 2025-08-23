using Godot;
using PongCSharp.Autoloads;
using PongCSharp.Enums;

namespace PongCSharp.Scenes;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        base._Ready();
        GameStateManager.Instance?.ChangeState(GameState.MainMenu);
    }
}
