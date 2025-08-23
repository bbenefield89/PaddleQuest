using PongCSharp.Enums;

namespace PongCSharp.UserInterface;

public partial class NewGameButton : MenuButtonBase
{
    public override void HandleButtonUp()
    {
        SceneLoader.Instance?.SwitchToScene(SceneName.Game);
    }
}
