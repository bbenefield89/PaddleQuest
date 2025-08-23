using Godot;
using PongCSharp.Enums;
using PongCSharp.Autoloads;

namespace PongCSharp.Game;

public partial class PlayerScoreManager : Node
{
    // Fields
    private int[] _playerScores = new int[2];

    // Lifecycles
    public override void _EnterTree()
    {
        base._EnterTree();
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
        GlobalEventBus.Instance?.BallEnteredGoal += GlobalEventBus_BallEnteredGoal;
        GlobalEventBus.Instance?.GameReset += GlobalEventBus_GameReset;
    }

    private void Unsubscribe()
    {
        GlobalEventBus.Instance?.BallEnteredGoal -= GlobalEventBus_BallEnteredGoal;
        GlobalEventBus.Instance?.GameReset -= GlobalEventBus_GameReset;
    }

    // Event Handlers
    private void GlobalEventBus_BallEnteredGoal(GoalSide goalSide)
    {
        var scoringPlayerId = DetermineWhichPlayerScored(goalSide);
        AddScoreToPlayerByPlayerId(scoringPlayerId);
    }

    private void GlobalEventBus_GameReset()
    {
        ResetPlayerScores();
    }

    // Methods
    private int DetermineWhichPlayerScored(GoalSide goalSide)
    {
        var playerId = goalSide == GoalSide.Left ? 1 : 0;
        return playerId;
    }

    private void AddScoreToPlayerByPlayerId(int scoringPlayerId)
    {
        _playerScores[scoringPlayerId] += 1;
        GlobalEventBus.Instance?.RaisePlayerScoreChanged(scoringPlayerId, _playerScores[scoringPlayerId]);
    }

    private void ResetPlayerScores()
    {
        _playerScores = new int[2];
    }
}