using PongCSharp.Autoloads;
using PongCSharp.Enums;

namespace PongCSharp.UserInterface;

public partial class MainMenuButton : MenuButtonBase
{
    public override void HandleButtonUp()
    {
        GameStateManager.Instance!.ChangeState(GameState.MainMenu);
        SceneLoader.Instance!.ChangeSceneTo(SceneName.MainMenu);
        MatchTypeManager.Instance!.UnloadMatchType();
    }
}
