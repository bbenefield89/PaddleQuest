using PongCSharp.Autoloads;

namespace PongCSharp.UserInterface;

public partial class PlayAgainButton : MenuButtonBase
{
    public override void HandleButtonUp()
    {
        GlobalEventBus.Instance?.RaiseGameReset();
    }
}