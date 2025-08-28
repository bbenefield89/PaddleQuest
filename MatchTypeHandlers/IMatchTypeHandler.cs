using System;

namespace PongCSharp.MatchTypeHandlers;

public interface IMatchTypeHandler
{
    public void Enter();

    public void Process(double delta);

    public void Exit();
}
