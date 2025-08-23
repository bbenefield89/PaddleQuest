using Godot;
using PongCSharp.Enums;
using PongCSharp.Autoloads;
using PongCSharp.VictoryConditionHandlers;
using System.Collections.Generic;
using System.Diagnostics;

namespace PongCSharp.Autoloads;

public partial class VictoryConditionManager : Node
{
    // Fields
    private IVictoryConditionHandler? _victoryConditionHandler;

    private Dictionary<VictoryCondition, IVictoryConditionHandler> _victoryConditions = [];

    // Props
    public static VictoryConditionManager? Instance { get; private set; }

    // Constructors
    public VictoryConditionManager()
    {
        Instance = this;
    }

    // Lifecycles
    public override void _EnterTree()
    {
        base._EnterTree();
        InitializeVictoryConditions();
    }

    // Setup
    private void InitializeVictoryConditions()
    {
        _victoryConditions = new()
        {
            [VictoryCondition.ScoreLimit] = new ScoreLimitVictoryConditionHandler()
        };
    }

    // Methods
    public void ChangeVictoryCondition(VictoryCondition victoryCondition)
    {
        _victoryConditionHandler?.Exit();

        IVictoryConditionHandler? victoryConditionHandler;
        if (!_victoryConditions.TryGetValue(victoryCondition, out victoryConditionHandler))
        {
            // Victory condition doesnt exist
            Debugger.Break();
            return;
        }

        _victoryConditionHandler = victoryConditionHandler;
        _victoryConditionHandler?.Enter();
    }
}
