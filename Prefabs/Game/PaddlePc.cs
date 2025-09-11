using Godot;
using PongCSharp.Enums;
using PongCSharp.Extensions;
using PongCSharp.Game.Paddle;
using System.Collections.Generic;
using System.Diagnostics;

namespace PongCSharp.Prefabs.Game;

public partial class PaddlePc : PaddleBase
{
    // Exports
    [Export]
    private InputAction _upAction;

    [Export]
    private InputAction _downAction;

    // Lifecycles
    public override void _Ready()
    {
        base._Ready();
        Validate();
    }

    // Methods
    protected override float GetMovementAxis()
    {
        var upActionName = _upAction.GetDisplayAttributeOrDefault("Name");
        var downActionName = _downAction.GetDisplayAttributeOrDefault("Name");

        return Input.GetAxis(upActionName, downActionName);
    }

    // Validation
    protected override void Validate()
    {
        base.Validate();

        var errorMessages = new List<string>();

        if (_upAction == InputAction.None)
            errorMessages.Add($"{nameof(_upAction)} is not set.");

        if (_downAction == InputAction.None)
            errorMessages.Add($"{nameof(_downAction)} is not set.");

        if (errorMessages.Count > 0)
        {
            GD.PushError(string.Join("\n", errorMessages));
            Debugger.Break();
            GetTree().Quit();
        }
    }
}