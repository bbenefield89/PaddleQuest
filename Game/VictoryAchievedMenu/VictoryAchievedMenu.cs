using Godot;
using PongCSharp.Autoloads;
using System.Diagnostics;

namespace PongCSharp.Game.VictoryAchievedMenu;

public partial class VictoryAchievedMenu : PanelContainer
{
    // Exports
    [Export]
    private Label? _victoryAchievedMenuLabel;

    // Lifecycles
    public override void _EnterTree()
    {
        base._EnterTree();
        Visible = false;
        Subscribe();
    }

    public override void _Ready()
    {
        base._Ready();
        Validate();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        Unsubscribe();
    }

    // Setup
    private void Subscribe()
    {
        GlobalEventBus.Instance?.VictoryConditionAchieved += GlobalEventBus_VictoryConditionAchieved;
        GlobalEventBus.Instance?.GameReset += GlobalEventBus_GameReset;
    }

    private void Unsubscribe()
    {
        GlobalEventBus.Instance?.VictoryConditionAchieved -= GlobalEventBus_VictoryConditionAchieved;
        GlobalEventBus.Instance?.GameReset -= GlobalEventBus_GameReset;
    }

    // Event Handlers
    private void GlobalEventBus_VictoryConditionAchieved(int playerId)
    {
        Visible = true;
        SetVictoryAchievedLabelText(playerId);
    }

    private void GlobalEventBus_GameReset()
    {
        Visible = false;
        _victoryAchievedMenuLabel?.Text = "Player {0} has won!";
    }

    // Methods
    private void SetVictoryAchievedLabelText(int playerId)
    {
        var winningText = _victoryAchievedMenuLabel?.Text.Replace("{0}", playerId.ToString());
        _victoryAchievedMenuLabel?.Text = winningText;
    }

    // Validation
    private void Validate()
    {
        var isInvalid = false;

        if (Visible)
        {
            // Menu should not be visible from the start
            isInvalid = true;
            Debugger.Break();
            GD.PushError("Menu should not be visible from the start");
        }

        if (_victoryAchievedMenuLabel is null)
        {
            // _victoryAchievedMenuLabel not set
            isInvalid = true;
            Debugger.Break();
            GD.PushError($"{_victoryAchievedMenuLabel} not set");
        }

        if (isInvalid)
        {
            GetTree().Quit();
        }
    }
}