using Godot;
using PongCSharp.Autoloads;
using PongCSharp.Enums;
using PongCSharp.Game;
using PongCSharp.Game.Ball;
using PongCSharp.Game.Scoreboard;
using System;
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

    // Services
    [Export]
    private PlayerScoreManager? _playerScoreManager;

    // Lifecycles
    public override void _Ready()
    {
        base._Ready();
        Validate();

        GlobalEventBus.Instance!.BallEnteredGoal += Goal_OnBallEnteredGoal;
        GlobalEventBus.Instance!.PlayerScoresUpdated += PlayerScoreManager_OnPlayerScoresUpdated;

        GlobalEventBus.Instance!.RaiseGameStarted();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        GlobalEventBus.Instance!.BallEnteredGoal -= Goal_OnBallEnteredGoal;
        GlobalEventBus.Instance!.PlayerScoresUpdated -= PlayerScoreManager_OnPlayerScoresUpdated;
    }

    // Event Handlers
    private void Goal_OnBallEnteredGoal(GoalSide goalSide)
    {
        _ball!.Reset();
        _playerScoreManager!.IncreasePlayerScore(goalSide);
    }

    private void PlayerScoreManager_OnPlayerScoresUpdated(Dictionary<GoalSide, int> playerScores)
    {
        _scoreboard!.UpdateScores(playerScores);
    }

    // Validation
    private void Validate()
    {
        var errors = new List<string>();

        if (_ball is null)
            errors.Add($"{nameof(_ball)} is null");

        if (_scoreboard is null)
            errors.Add($"{nameof(_scoreboard)} is null");

        if (_playerScoreManager is null)
            errors.Add($"{nameof(_playerScoreManager)} is null");

        if (errors.Count > 0)
        {
            Debugger.Break();
            GD.PushError(string.Join("\n", errors));
            GetTree().Quit();
        }
    }
}
