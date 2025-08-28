using PongCSharp.Autoloads;
using PongCSharp.Enums;
using PongCSharp.Game.MatchTimeManager;
using PongCSharp.Models;
using System.Collections.Generic;
using System.Linq;

namespace PongCSharp.MatchTypeHandlers;

public class ScoreLimitMatchTypeHandler(MatchSettings matchSettings) : IMatchTypeHandler
{
    private readonly MatchTimeManager _matchTimeManager = new(matchSettings);

    // Lifecycles
    public void Enter()
    {
        Subscribe();
        _matchTimeManager.Ready();
    }

    public void Process(double delta) => _matchTimeManager.Process(delta);

    public void Exit()
    {
        Unsubscribe();
        _matchTimeManager.Exit();
    }

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
