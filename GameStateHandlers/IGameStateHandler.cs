using PongCSharp.Enums;

namespace PongCSharp.GameStateHandlers;

public interface IGameStateHandler
{
    public GameState GameState { get; }

    public void Enter();

    public void Update();

    public void Exit();
}
