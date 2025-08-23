using Godot;
using PongCSharp.Autoloads;
using PongCSharp.Enums;
using PongCSharp.Game.Ball;
using PongCSharp.Game.Scoreboard;
using System.Collections.Generic;
using System.Diagnostics;

namespace PongCSharp.Scenes;

public partial class Game : Node
{
    // Exports
    [Export]
    private Ball? _ball;

    [Export]
    private Scoreboard? _scoreboard;

    // Lifecycles
    public override void _Ready()
    {
        base._Ready();
        Validate();
        GlobalEventBus.Instance!.BallEnteredGoal += Goal_OnBallEnteredGoal;
        GlobalEventBus.Instance!.RaiseGameStarted();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        GlobalEventBus.Instance!.BallEnteredGoal -= Goal_OnBallEnteredGoal;
    }

    // Event Handlers
    private void Goal_OnBallEnteredGoal(GoalSide goalSide)
    {
        _ball!.Reset();
        _scoreboard!.IncreasePlayerScore(goalSide);
    }

    // Validation
    private void Validate()
    {
        var errors = new List<string>();

        if (_ball is null)
            errors.Add($"{nameof(_ball)} is null");

        if (_scoreboard is null)
            errors.Add($"{nameof(_scoreboard)} is null");

        if (errors.Count > 0)
        {
            Debugger.Break();
            GD.PushError(string.Join("\n", errors));
            GetTree().Quit();
        }
    }
}
