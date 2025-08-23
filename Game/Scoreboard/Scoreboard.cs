using Godot;
using PongCSharp.Enums;
using PongCSharp.Autoloads;
using System.Collections.Generic;
using System.Diagnostics;

namespace PongCSharp.Game.Scoreboard;

public partial class Scoreboard : Control
{
    private readonly List<Label> _playerScoreLabels = [];

    private int[] _playerScores = new int[2];

    // Lifecycles
    public override void _EnterTree()
    {
        base._EnterTree();
        Subscribe();
        InitializePlayerScoreLabels();
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
    private void InitializePlayerScoreLabels()
    {
        foreach (var child in GetChildren())
        {
            if (child is not Label)
            {
                // Child is is not a Label type
                Debugger.Break();
            }

            _playerScoreLabels.Add((Label)child);
        }
    }

    private void Subscribe()
    {
        GlobalEventBus.Instance?.GameReset += GlobalEventBus_GameReset;
    }

    private void Unsubscribe()
    {
        GlobalEventBus.Instance?.GameReset -= GlobalEventBus_GameReset;
    }

    // Event Handlers
    private void GlobalEventBus_GameReset()
    {
        ResetPlayerScores();
    }

    // Methods
    public void IncreasePlayerScore(GoalSide goalSide)
    {
        var scoringPlayerId = DetermineWhichPlayerScored(goalSide);
        AddScoreToPlayer(scoringPlayerId);
    }

    private int DetermineWhichPlayerScored(GoalSide goalSide)
    {
        var playerId = goalSide == GoalSide.Left ? 1 : 0;
        return playerId;
    }

    private void AddScoreToPlayer(int scoringPlayerId)
    {
        _playerScores[scoringPlayerId] += 1;
        _playerScoreLabels[scoringPlayerId].Text = _playerScores[scoringPlayerId].ToString();
        GlobalEventBus.Instance?.RaisePlayerScoreChanged(scoringPlayerId, _playerScores[scoringPlayerId]);
    }

    private void ResetPlayerScores()
    {
        _playerScores = new int[2];
        
        foreach (var label in _playerScoreLabels)
        {
            label.Text = 0.ToString();
        }
    }

    // Validation
    private void Validate()
    {
        var isInvalid = false;

        if (_playerScoreLabels.Count < 2)
        {
            // _playerScoreLabels does not have enough players
            isInvalid = true;
            Debugger.Break();
            GD.PushError($"{_playerScoreLabels} does not have enough players\nPlayerCount: {_playerScoreLabels.Count}");
        }

        if (isInvalid)
        {
            GetTree().Quit();
        }
    }
}