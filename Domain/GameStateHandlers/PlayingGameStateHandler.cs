using Godot;
using PongCSharp.Enums;
using PongCSharp.GameStateHandlers;

namespace PongCSharp.Domain.GameStateHandlers;

public class PlayingGameStateHandler : IGameStateHandler
{
    public GameState GameState => GameState.Playing;

    public void Enter()
        => Input.SetMouseMode(Input.MouseModeEnum.Hidden);

    public void Exit() { }

    public void Update() { }
}