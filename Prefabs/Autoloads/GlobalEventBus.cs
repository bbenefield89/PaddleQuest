using Godot;
using PongCSharp.Enums;
using System;
using System.Collections.Generic;

namespace PongCSharp.Autoloads;

public partial class GlobalEventBus : Node
{
    // Static Props
    public static GlobalEventBus? Instance { get; private set; }

    // BallEnteredGoal
    public event Action<GoalSide>? BallEnteredGoal;
    public void RaiseBallEnteredGoal(GoalSide goalSide)
        => BallEnteredGoal?.Invoke(goalSide);

    // GameReset
    public event Action? GameReset;
    public void RaiseGameReset()
        => GameReset?.Invoke();

    // GameStarted
    public event Action? GameStarted;
    public void RaiseGameStarted()
        => GameStarted?.Invoke();

    // MatchTimeLimitReached
    public event Action? MatchTimeLimitReached;
    public void RaiseMatchTimeLimitReached()
        => MatchTimeLimitReached?.Invoke();

    // MatchTimeUpdated
    public event Action<double>? MatchTimeUpdated;
    public void RaiseMatchTimeUpdated(double currentMatchTime)
        => MatchTimeUpdated?.Invoke(currentMatchTime);

    // PlayerScoreChanged
    public event Action<int, int>? PlayerScoreChanged;
    public void RaisePlayerScoreChanged(int playerId, int totalScore)
        => PlayerScoreChanged?.Invoke(playerId, totalScore);

    // PlayerScoresFinalized
    public event Action<Dictionary<GoalSide, int>>? PlayerScoresFinalized;
    public void RaisePlayerScoresFinalized(Dictionary<GoalSide, int> playerScores)
        => PlayerScoresFinalized?.Invoke(playerScores);

    // PlayerScoresUpdated
    public event Action<Dictionary<GoalSide, int>>? PlayerScoresUpdated;
    public void RaisePlayerScoresUpdated(Dictionary<GoalSide, int> playerScores)
        => PlayerScoresUpdated?.Invoke(playerScores);

    // StartMatchButtonUp
    public event Action? StartMatchButtonUp;
    public void RaiseStartMatchButtonUp()
        => StartMatchButtonUp?.Invoke();

    // VictoryConditionAchieved
    public event Action<GoalSide>? VictoryConditionAchieved;
    public void RaiseVictoryConditionAchieved(GoalSide playerId)
        => VictoryConditionAchieved?.Invoke(playerId);

    // Constructors
    public GlobalEventBus()
        => Instance = this;
}