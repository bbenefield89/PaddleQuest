using Godot;
using PongCSharp.Constants;
using PongCSharp.Enums;
using GameStateManager = PongCSharp.Prefabs.Autoloads.GameStateManager;

namespace PongCSharp.Prefabs.UserInterface;

public partial class PauseMenu : PanelContainer
{
    [Export]
    private ResumeGameButton? ResumeGameButton { get; set; }

    public override void _Ready()
    {
        base._Ready();
        Visible = false;

        ResumeGameButton?.ResumeButtonClickedAction += PauseMenu_ResumeButtonClicked;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed(InputActions.PauseGame))
            TogglePauseMenu();
    }

    private void PauseMenu_ResumeButtonClicked()
    {
        TogglePauseMenu();
    }

    private void TogglePauseMenu()
    {
        var currentGameState = GameStateManager.Instance?.CurrentGameState?.GameState;
        var nextGameState = currentGameState == GameState.Playing
            ? GameState.Paused
            : GameState.Playing;

        GameStateManager.Instance?.ChangeState(nextGameState);
        Visible = !Visible;
    }
}