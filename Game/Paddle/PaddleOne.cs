using PongCSharp.Constants;

namespace PongCSharp.Game.Paddle;

public partial class PaddleOne : PaddleBase
{
    public override void _Process(double delta)
    {
        base._Process(delta);
        MovePaddle(
            delta,
            InputActionsContants.PlayerOneMoveUp,
            InputActionsContants.PlayerOneMoveDown);
    }
}
