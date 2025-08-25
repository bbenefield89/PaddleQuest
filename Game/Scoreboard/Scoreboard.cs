using Godot;
using PongCSharp.Enums;
using PongCSharp.Autoloads;
using System.Collections.Generic;
using System.Diagnostics;

namespace PongCSharp.Game.Scoreboard;

public partial class Scoreboard : Control
{
    // Exports
    [Export]
    private Label?[] _playerScoreLabels = [];

    // Fields
    private readonly Dictionary<GoalSide, int> _goalSideToPlayerScoreLabelIndex = new()
    {
        { GoalSide.Left, 0 },
        { GoalSide.Right, 1 }
    };

    // Lifecycles
    public override void _Ready()
    {
        base._Ready();
        Subscribe();
        Validate();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        Unsubscribe();
    }

    // Setup
    private void Subscribe()
        => GlobalEventBus.Instance?.GameReset += GlobalEventBus_GameReset;

    private void Unsubscribe()
        => GlobalEventBus.Instance?.GameReset -= GlobalEventBus_GameReset;

    // Event Handlers
    private void GlobalEventBus_GameReset() => ResetPlayerScores();

    // Methods
    public void UpdateScores(Dictionary<GoalSide, int> goalSideToPlayerScore)
    {
        foreach (var (goalSide, playerScore) in goalSideToPlayerScore)
            if (_goalSideToPlayerScoreLabelIndex.TryGetValue(goalSide, out var index))
                _playerScoreLabels[index]!.Text = playerScore.ToString();
    }

    private void ResetPlayerScores()
    {
        foreach (var label in _playerScoreLabels)
            label!.Text = 0.ToString();
    }

    // Validation
    private void Validate()
    {
        var isInvalid = false;

        if (_playerScoreLabels.Length < 2)
        {
            // _playerScoreLabels does not have enough players
            isInvalid = true;
            Debugger.Break();
            GD.PushError($"{_playerScoreLabels} does not have enough players\nPlayerCount: {_playerScoreLabels.Length}");
        }

        if (isInvalid)
            GetTree().Quit();
    }
}