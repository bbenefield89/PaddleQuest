using System;

namespace PongCSharp.VictoryConditionHandlers;

public interface IVictoryConditionHandler
{
    public void Enter();

    public void Exit();
}
