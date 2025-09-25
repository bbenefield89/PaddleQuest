using Godot;
using PongCSharp.Enums;
using PongCSharp.GameStateHandlers;

namespace PongCSharp.Domain.GameStateHandlers;

public class PausedGameStateHandler : IGameStateHandler
{
    // Fields
    private readonly SceneTree? _sceneTree = Engine.GetMainLoop() as SceneTree;

    // Props
    public GameState GameState => GameState.Paused;

    // Methods
    public void Enter()
    {
        _sceneTree?.Paused = true;
        Input.SetMouseMode(Input.MouseModeEnum.Visible);
    }

    public void Update() { }

    public void Exit()
        => _sceneTree?.Paused = false;
}