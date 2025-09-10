namespace PongCSharp.UserInterface;

public partial class QuitGameButton : MenuButtonBase
{
    public override void HandleButtonUp()
    {
        GetTree().Quit();
    }
}
