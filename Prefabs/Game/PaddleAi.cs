using Godot;
using PongCSharp.Game.Ball;
using PongCSharp.Game.Paddle;
using System.Collections.Generic;
using System.Diagnostics;

namespace PongCSharp.Prefabs.Game;

public partial class PaddleAi : PaddleBase
{
    // Exports
    [Export]
    private Ball? _ball;

    // Props
    private float PaddleCenterPositionY => Position.Y + (PaddleDimensions.Y / 2);

    // Lifecycles
    public override void _Ready()
    {
        base._Ready();
        Validate();
    }

    // Methods
    protected override float GetMovementAxis()
    {
        if (_ball!.Position.Y > PaddleCenterPositionY)
            return 1f;

        if (_ball.Position.Y < PaddleCenterPositionY)
            return -1f;

        return 0f;
    }

    // Validation
    protected override void Validate()
    {
        base.Validate();

        var errorMessages = new List<string>();

        if (_ball == null)
            errorMessages.Add($"{nameof(_ball)} is not set.");

        if (errorMessages.Count > 0)
        {
            GD.PushError(string.Join("\n", errorMessages));
            Debugger.Break();
            GetTree().Quit();
        }
    }
}