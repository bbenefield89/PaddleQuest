using System;

namespace PongCSharp.MatchTypeHandlers;

public interface IMatchTypeHandler
{
    public void Enter();

    public void Process();

    public void Exit();
}
