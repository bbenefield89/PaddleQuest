using PongCSharp.Constants;

namespace PongCSharp.Game.Paddle;

public partial class PaddleTwo : PaddleBase
{
    public override void _Process(double delta)
    {
        base._Process(delta);
        MovePaddle(
            delta,
            InputActionsContants.PlayerTwoMoveUp,
            InputActionsContants.PlayerTwoMoveDown);
    }
}
