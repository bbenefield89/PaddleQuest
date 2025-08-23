using Godot;
using PongCSharp.Enums;
using System.Diagnostics;

namespace PongCSharp.GameStateHandlers;

public class PausedGameStateHandler : IGameStateHandler
{
    // Fields
    private readonly SceneTree _sceneTree;

    // Props
    public GameState GameState {  get; } = GameState.Paused;

    // Constructors
    public PausedGameStateHandler(SceneTree sceneTree)
    {
        _sceneTree = sceneTree;
    }

    // Methods
    public void Enter()
    {
        _sceneTree?.Paused = true;
    }

    public void Update()
    {
        Debugger.Break();
    }

    public void Exit()
    {
        _sceneTree?.Paused = false;
    }
}
