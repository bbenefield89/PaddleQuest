using Godot;
using PongCSharp.Autoloads;
using PongCSharp.Constants;
using PongCSharp.Enums;
using System.Collections.Generic;
using System.Diagnostics;

namespace PongCSharp.Game;

public partial class Goal : Area2D
{
    [Export]
    private GoalSide _goalSide;

    public override void _Ready()
    {
        base._Ready();
        Validate();
        BodyEntered += Goal_BodyEntered;
    }

    private void Goal_BodyEntered(Node2D body)
    {
        if (body.IsInGroup(NodeGroupNames.Balls))
            GlobalEventBus.Instance?.RaiseBallEnteredGoal(_goalSide);
    }

    private void Validate()
    {
        var errorMessages = new List<string>();

        if (_goalSide == GoalSide.Unknown)
            errorMessages.Add($"{nameof(Goal)} at index {GetIndex()} cannot be {GoalSide.Unknown}.");

        if (errorMessages.Count == 0)
            return;

        Debugger.Break();
        GetTree().Quit();
    }
}
