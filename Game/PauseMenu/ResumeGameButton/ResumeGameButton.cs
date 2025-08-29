using PongCSharp.UserInterface;
using System;

public partial class ResumeGameButton : MenuButtonBase
{
    public event Action? ResumeButtonClickedAction;

    public override void HandleButtonUp()
    {
        OnResumeButtonClicked();
    }

    private void OnResumeButtonClicked()
    {
        ResumeButtonClickedAction?.Invoke();
    }
}
