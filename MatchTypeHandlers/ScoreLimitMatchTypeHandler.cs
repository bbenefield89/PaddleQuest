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
    private void GlobalEventBus_PlayerScoresUpdated(Dictionary<GoalSide, int> playerScores)
        => DetermineMatchWinnerByHighestScore(playerScores);

    private void PlayerScoreManager_PlayerScoresFinalized(Dictionary<GoalSide, int> playerScores)
        => DetermineMatchWinnerByHighestScore(playerScores);

    // Methods
    private void DetermineMatchWinnerByHighestScore(Dictionary<GoalSide, int> playerScores)
    {
        var playerIdToScore = playerScores
            .OrderByDescending(kv => kv.Value)
            .FirstOrDefault();

        if (playerIdToScore.Value >= matchSettings.ScoreLimit)
            GlobalEventBus.Instance?.RaiseVictoryConditionAchieved(playerIdToScore.Key);
    }
}
