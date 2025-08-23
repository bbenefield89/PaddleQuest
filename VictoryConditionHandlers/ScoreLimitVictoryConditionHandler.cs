using PongCSharp.Autoloads;

namespace PongCSharp.VictoryConditionHandlers;

public class ScoreLimitVictoryConditionHandler : IVictoryConditionHandler
{
    // Consts
    private const int SCORE_LIMIT = 1;

    // Lifecycles
    public void Enter()
    {
        Subscribe();
    }

    public void Exit()
    {
        Unsubscribe();
    }

    // Setup
    private void Subscribe()
    {
        GlobalEventBus.Instance?.PlayerScoreChanged += GlobalEventBus_PlayerScoreChanged;
    }

    private void Unsubscribe()
    {
        GlobalEventBus.Instance?.PlayerScoreChanged -= GlobalEventBus_PlayerScoreChanged;
    }

    // Event Handlers
    private void GlobalEventBus_PlayerScoreChanged(int playerId, int totalScore)
    {
        if (totalScore < SCORE_LIMIT)
        {
            return;
        }

        GlobalEventBus.Instance?.RaiseVictoryConditionAchieved(playerId + 1);
    }
}
