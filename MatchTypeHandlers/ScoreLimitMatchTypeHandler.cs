using Godot.Collections;
using PongCSharp.Autoloads;
using System;
using System.Linq;
using static PongCSharp.Scenes.MatchOptionsMenu;

namespace PongCSharp.MatchTypeHandlers;

public class ScoreLimitMatchTypeHandler(MatchSettings matchSettings) : IMatchTypeHandler
{
    private DateTime _matchEndTime;

    private Dictionary<int, int> _playerScoreManager = [];

    // Lifecycles
    public void Enter()
    {
        Subscribe();
        _matchEndTime = DateTime.UtcNow.AddSeconds(matchSettings.TimeLimit);
    }

    public void Process()
    {
        if (DateTime.UtcNow >= _matchEndTime)
            DetermineMatchWinnerByHighestScore();
    }

    public void Exit()
        => Unsubscribe();

    // Setup
    private void Subscribe()
        => GlobalEventBus.Instance?.PlayerScoreChanged += GlobalEventBus_PlayerScoreChanged;

    private void Unsubscribe()
        => GlobalEventBus.Instance?.PlayerScoreChanged -= GlobalEventBus_PlayerScoreChanged;

    // Event Handlers
    private void GlobalEventBus_PlayerScoreChanged(int playerId, int totalScore)
    {
        _playerScoreManager[playerId] = totalScore;

        if (totalScore >= matchSettings.ScoreLimit)
            DetermineMatchWinnerByHighestScore();
    }

    // Methods
    private void DetermineMatchWinnerByHighestScore()
    {
        var highestScoringPlayerId = _playerScoreManager
            .OrderByDescending(kv => kv.Value)
            .FirstOrDefault()
            .Key;

        GlobalEventBus.Instance?.RaiseVictoryConditionAchieved(highestScoringPlayerId + 1);
    }
}
