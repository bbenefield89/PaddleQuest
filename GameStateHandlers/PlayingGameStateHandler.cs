using PongCSharp.Enums;

namespace PongCSharp.GameStateHandlers;

public class PlayingGameStateHandler : IGameStateHandler
{
    public GameState GameState { get; } = GameState.Playing;

    public void Enter() { }

    public void Exit() { }

    public void Update() { }
}