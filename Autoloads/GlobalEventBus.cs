using Godot;
using PongCSharp.Enums;
using System;

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

    // PlayerScoreChanged
    public event Action<int, int>? PlayerScoreChanged;
    public void RaisePlayerScoreChanged(int playerId, int totalScore)
        => PlayerScoreChanged?.Invoke(playerId, totalScore);

    // VictoryConditionAchieved
    public event Action<int>? VictoryConditionAchieved;
    public void RaiseVictoryConditionAchieved(int playerId)
        => VictoryConditionAchieved?.Invoke(playerId);

    // Constructors
    public GlobalEventBus()
        => Instance = this;
}