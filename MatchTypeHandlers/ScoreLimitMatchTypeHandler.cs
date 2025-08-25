using PongCSharp.Autoloads;
using PongCSharp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using static PongCSharp.Scenes.MatchOptionsMenu;

namespace PongCSharp.MatchTypeHandlers;

public class ScoreLimitMatchTypeHandler(MatchSettings matchSettings) : IMatchTypeHandler
{
    private DateTime _matchEndTime;

    // Lifecycles
    public void Enter()
    {
        Subscribe();
        _matchEndTime = DateTime.UtcNow.AddSeconds(matchSettings.TimeLimit);
    }

    public void Process()
    {
        if (DateTime.UtcNow >= _matchEndTime)
            GlobalEventBus.Instance?.RaiseMatchTimeLimitReached();
    }

    public void Exit()
        => Unsubscribe();

    // Setup
    private void Subscribe()
    {
        GlobalEventBus.Instance!.PlayerScoresUpdated += GlobalEventBus_PlayerScoresUpdated;
        GlobalEventBus.Instance!.PlayerScoresFinalized += PlayerScoreManager_PlayerScoresFinalized;
    }

    private void Unsubscribe()
    {
        GlobalEventBus.Instance!.PlayerScoresUpdated -= GlobalEventBus_PlayerScoresUpdated;
        GlobalEventBus.Instance!.PlayerScoresFinalized -= PlayerScoreManager_PlayerScoresFinalized;
    }

    // Event Handlers
    private void GlobalEventBus_PlayerScoresUpdated(
        Dictionary<GoalSide, int> goalSideToPlayerScore
    )
    {
        if (goalSideToPlayerScore.Any(kv => kv.Value >= matchSettings.ScoreLimit))
            DetermineMatchWinnerByHighestScore(goalSideToPlayerScore);
    }

    private void PlayerScoreManager_PlayerScoresFinalized(
        Dictionary<GoalSide, int> goalSideToPlayerScore
    )
        => DetermineMatchWinnerByHighestScore(goalSideToPlayerScore);

    // Methods
    private void DetermineMatchWinnerByHighestScore(
        Dictionary<GoalSide, int> goalSideToPlayerScore
    )
    {
        var highestScoringGoalSide = goalSideToPlayerScore
            .OrderByDescending(kv => kv.Value)
            .FirstOrDefault();

        GlobalEventBus.Instance?
            .RaiseVictoryConditionAchieved(highestScoringGoalSide.Key);

        GameStateManager.Instance?.ChangeState(GameState.Paused);
    }
}
