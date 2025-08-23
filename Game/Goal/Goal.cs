using Godot;
using PongCSharp.Autoloads;
using PongCSharp.Constants;
using PongCSharp.Enums;
using System.Diagnostics;

public partial class Goal : Area2D
{
    [Export]
    private GoalSide _goalSide;

    public override void _Ready()
    {
        Validate();
        base._Ready();
        BodyEntered += Goal_BodyEntered;
    }

    private void Goal_BodyEntered(Node2D body)
    {
        if (body.IsInGroup(NodeGroupNames.Balls))
        {
            GlobalEventBus.Instance?.RaiseBallEnteredGoal(_goalSide);
        }
    }

    private void Validate()
    {
        if (_goalSide == GoalSide.Unknown)
        {
            // _goalSide has not been set
            Debugger.Break();
        }
    }
}
