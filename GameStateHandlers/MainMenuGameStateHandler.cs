using PongCSharp.Enums;

namespace PongCSharp.GameStateHandlers;

public class MainMenuGameStateHandler : IGameStateHandler
{
    public GameState GameState { get; } = GameState.MainMenu;

    public void Enter() { }

    public void Exit() { }

    public void Update() { }
}
