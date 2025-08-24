using Godot;
using PongCSharp.Enums;
using PongCSharp.Autoloads;
using System.Collections.Generic;

namespace PongCSharp.Game;

public partial class PlayerScoreManager : Node
{
    // Fields
    private Dictionary<GoalSide, int> _playerScores = new()
    {
        [GoalSide.Left] = 0,
        [GoalSide.Right] = 0,
    };

    // Lifecycles
    public override void _Ready()
    {
        base._Ready();
        Subscribe();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        Unsubscribe();
    }

    // Setup
    private void Subscribe()
    {
        GlobalEventBus.Instance!.GameReset += GlobalEventBus_GameReset;
        GlobalEventBus.Instance!.MatchTimeLimitReached += ScoreLimitMatchTypeHandler_OnMatchTimeLimitReached;
    }

    private void Unsubscribe()
    {
        GlobalEventBus.Instance!.GameReset -= GlobalEventBus_GameReset;
        GlobalEventBus.Instance!.MatchTimeLimitReached -= ScoreLimitMatchTypeHandler_OnMatchTimeLimitReached;
    }

    // Event Handlers
    private void GlobalEventBus_GameReset() => ResetPlayerScores();

    private void ScoreLimitMatchTypeHandler_OnMatchTimeLimitReached()
        => GlobalEventBus.Instance!.RaisePlayerScoresFinalized(_playerScores);

    // Methods
    public void IncreasePlayerScore(GoalSide goalSide)
    {
        var scoringPlayerId = DetermineWhichPlayerScored(goalSide);
        AddScoreToPlayerByPlayerId(scoringPlayerId);
        GlobalEventBus.Instance!.RaisePlayerScoresUpdated(_playerScores);
    }

    private GoalSide DetermineWhichPlayerScored(GoalSide goalSide)
    {
        var playerId = goalSide == GoalSide.Left
            ? GoalSide.Right
            : GoalSide.Left;

        return playerId;
    }

    private void AddScoreToPlayerByPlayerId(GoalSide goalSide)
        => _playerScores[goalSide] += 1;

    private void ResetPlayerScores()
    {
        foreach (var key in _playerScores.Keys)
            _playerScores[key] = 0;
    }
}