using Godot;
using PongCSharp.Autoloads;
using PongCSharp.Enums;
using System.Collections.Generic;

namespace PongCSharp.Game;

public partial class PlayerScoreManager : Node
{
    // Fields
    private Dictionary<GoalSide, int> _goalSideToPlayerScore = new()
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
        => GlobalEventBus.Instance!.RaisePlayerScoresFinalized(_goalSideToPlayerScore);

    // Methods
    public void IncreasePlayerScore(GoalSide sideScoredUpon)
    {
        var scoringSide = DetermineWhichPlayerScored(sideScoredUpon);
        AddScoreToScoringSide(scoringSide);
        GlobalEventBus.Instance!.RaisePlayerScoresUpdated(_goalSideToPlayerScore);
    }

    private GoalSide DetermineWhichPlayerScored(GoalSide goalSide)
    {
        var scoringSide = goalSide == GoalSide.Left
            ? GoalSide.Right
            : GoalSide.Left;

        return scoringSide;
    }

    private void AddScoreToScoringSide(GoalSide scoringSide)
        => _goalSideToPlayerScore[scoringSide] += 1;

    private void ResetPlayerScores()
    {
        foreach (var goalSide in _goalSideToPlayerScore.Keys)
            _goalSideToPlayerScore[goalSide] = 0;
    }
}