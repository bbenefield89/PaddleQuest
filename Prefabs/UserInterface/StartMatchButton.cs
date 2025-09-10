using PongCSharp.Autoloads;

namespace PongCSharp.UserInterface;

public partial class StartMatchButton : MenuButtonBase
{
    public override void HandleButtonUp() => GlobalEventBus.Instance!.RaiseStartMatchButtonUp();
}