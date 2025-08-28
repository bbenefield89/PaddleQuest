using Godot;
using PongCSharp.Autoloads;
using PongCSharp.Enums;
using PongCSharp.Game;
using PongCSharp.Game.Ball;
using PongCSharp.Game.Scoreboard;
using PongCSharp.Game.VictoryAchievedMenu;
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

    [Export]
    private VictoryAchievedMenu? _victoryAchievedMenu;

    // Lifecycles
    public override void _Ready()
    {
        base._Ready();
        Validate();
        Subscribe();
        GlobalEventBus.Instance!.RaiseGameStarted();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        Unsubscribe();
    }

    // Setup
    private void Subscribe()
    {
        GlobalEventBus.Instance!.BallEnteredGoal += Goal_OnBallEnteredGoal;
        GlobalEventBus.Instance!.PlayerScoresUpdated += PlayerScoreManager_OnPlayerScoresUpdated;
        GlobalEventBus.Instance!.VictoryConditionAchieved += MatchTypeHandler_OnVictoryConditionAchieved;
        GlobalEventBus.Instance!.GameReset += PlayAgainButton_GameReset;
    }

    private void PlayAgainButton_GameReset()
    {
        _ball?.Reset();
    }

    private void Unsubscribe()
    {
        GlobalEventBus.Instance!.BallEnteredGoal -= Goal_OnBallEnteredGoal;
        GlobalEventBus.Instance!.PlayerScoresUpdated -= PlayerScoreManager_OnPlayerScoresUpdated;
        GlobalEventBus.Instance!.VictoryConditionAchieved -= MatchTypeHandler_OnVictoryConditionAchieved;
        GlobalEventBus.Instance!.GameReset -= PlayAgainButton_GameReset;
    }

    // Event Handlers
    private void Goal_OnBallEnteredGoal(GoalSide goalSide) => _ball!.Reset();

    private void PlayerScoreManager_OnPlayerScoresUpdated(
        Dictionary<GoalSide, int> goalSideToScore
    )
        => _scoreboard!.UpdateScores(goalSideToScore);

    public void MatchTypeHandler_OnVictoryConditionAchieved(GoalSide winningGoalSide)
        => _victoryAchievedMenu!.Show(winningGoalSide);

    // Validation
    private void Validate()
    {
        var errors = new List<string>();

        if (_ball is null)
            errors.Add($"{nameof(_ball)} is null");

        if (_scoreboard is null)
            errors.Add($"{nameof(_scoreboard)} is null");

        if (errors.Count == 0)
            return;

        Debugger.Break();
        GD.PushError(string.Join("\n", errors));
        GetTree().Quit();
    }
}
