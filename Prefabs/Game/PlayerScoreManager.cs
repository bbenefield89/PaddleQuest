using PongCSharp.Autoloads;
using PongCSharp.Enums;
using PongCSharp.Models;
using System.Collections.Generic;
using System.Linq;

namespace PongCSharp.Game.Managers;

public partial class PlayerScoreManager(MatchSettings matchSettings)
{
    // Fields
    private readonly Dictionary<GoalSide, int> _goalSideToPlayerScore = new()
    {
        [GoalSide.Left] = 0,
        [GoalSide.Right] = 0,
    };

    // Lifecycles
    public void Ready() => Subscribe();

    public void Exit() => Unsubscribe();

    // Setup
    private void Subscribe()
    {
        GlobalEventBus.Instance!.GameReset += GlobalEventBus_OnGameReset;
        GlobalEventBus.Instance!.BallEnteredGoal += Goal_OnBallEnteredGoal;
        GlobalEventBus.Instance!.MatchTimeLimitReached += MatchTimeManager_OnMatchTimeLimitReached;
    }

    private void Unsubscribe()
    {
        GlobalEventBus.Instance!.GameReset -= GlobalEventBus_OnGameReset;
        GlobalEventBus.Instance!.BallEnteredGoal += Goal_OnBallEnteredGoal;
        GlobalEventBus.Instance!.MatchTimeLimitReached -= MatchTimeManager_OnMatchTimeLimitReached;
    }

    // Event Handlers
    private void GlobalEventBus_OnGameReset() => ResetPlayerScores();

    private void Goal_OnBallEnteredGoal(GoalSide scoredOnGoalSide)
    {
        IncreasePlayerScore(scoredOnGoalSide);
        CheckForVictoryAchieved();
    }

    private void MatchTimeManager_OnMatchTimeLimitReached()
        => GlobalEventBus.Instance!.RaisePlayerScoresFinalized(_goalSideToPlayerScore);

    // Methods
    public void IncreasePlayerScore(GoalSide sideScoredUpon)
    {
        var scoringSide = DetermineWhichPlayerScored(sideScoredUpon);
        AddScoreToScoringSide(scoringSide);
        GlobalEventBus.Instance!.RaisePlayerScoresUpdated(_goalSideToPlayerScore);
    }

    private void CheckForVictoryAchieved()
    {
        var winningSide = _goalSideToPlayerScore.FirstOrDefault(kv => kv.Value >= matchSettings.ScoreLimit);
        var hasGoalSideWon = winningSide.Key != GoalSide.Unknown;

        if (!hasGoalSideWon)
            return;

        HandleVictoryAchieved(winningSide);
    }

    private static void HandleVictoryAchieved(KeyValuePair<GoalSide, int> winningSide)
    {
        GlobalEventBus.Instance!.RaiseVictoryConditionAchieved(winningSide.Key);
        GameStateManager.Instance!.ChangeState(GameState.Paused);
    }

    private GoalSide DetermineWhichPlayerScored(GoalSide goalSide)
    {
        var scoringSide = goalSide == GoalSide.Left
            ? GoalSide.Right
            : GoalSide.Left;

        return scoringSide;
    }

    private void AddScoreToScoringSide(GoalSide scoringSide) => _goalSideToPlayerScore[scoringSide] += 1;

    private void ResetPlayerScores()
    {
        foreach (var goalSide in _goalSideToPlayerScore.Keys)
            _goalSideToPlayerScore[goalSide] = 0;
    }
}
