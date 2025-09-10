using PongCSharp.Autoloads;
using PongCSharp.Models;

namespace PongCSharp.Game.Managers;

public class MatchTimeManager(MatchSettings matchSettings)
{
    // Internals
    private double _elapsedMatchTime = 0;

    private readonly double _emitEveryXSeconds = 1;

    private double _secondsSinceMatchTimeUpdated = 0;

    // Lifecycles
    public void Ready() => GlobalEventBus.Instance!.GameReset += SomeButton_GameReset;

    public void Process(double delta)
    {
        _elapsedMatchTime += delta;
        _secondsSinceMatchTimeUpdated += delta;

        RaiseMatchTimeUpdatedWhenNeeded();
        RaiseMatchTimeLimitReachedWhenNeeded();
    }

    public void Exit() => GlobalEventBus.Instance!.GameReset -= SomeButton_GameReset;

    // Event Handlers
    private void SomeButton_GameReset() => Reset();

    // Methods
    private void RaiseMatchTimeUpdatedWhenNeeded()
    {
        if (_secondsSinceMatchTimeUpdated >= _emitEveryXSeconds)
        {
            GlobalEventBus.Instance!.RaiseMatchTimeUpdated(_elapsedMatchTime);
            _secondsSinceMatchTimeUpdated = 0;
        }
    }

    private void RaiseMatchTimeLimitReachedWhenNeeded()
    {
        if (_elapsedMatchTime >= matchSettings.TimeLimit)
        {
            GlobalEventBus.Instance!.RaiseMatchTimeLimitReached();
        }
    }

    private void Reset()
    {
        _elapsedMatchTime = 0;
        _secondsSinceMatchTimeUpdated = 0;
    }
}
