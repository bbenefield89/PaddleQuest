using Godot;
using PongCSharp.Enums;
using PongCSharp.MatchTypeHandlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static PongCSharp.Scenes.MatchOptionsMenu;

namespace PongCSharp.Autoloads;

public partial class MatchTypeManager : Node
{
    // Fields
    private IMatchTypeHandler? _matchTypeHandler;

    private Dictionary<MatchType, Func<MatchSettings, IMatchTypeHandler>> _matchTypeToHandler = new()
    {
        [MatchType.ScoreLimit] = matchSettings => new ScoreLimitMatchTypeHandler(matchSettings)
    };

    // Props
    public static MatchTypeManager? Instance { get; private set; }

    // Constructors
    public MatchTypeManager() => Instance = this;

    // Lifecycles
    public override void _Process(double delta)
    {
        base._Process(delta);
        _matchTypeHandler?.Process();
    }

    // Methods
    public void ChangeMatchType(MatchSettings matchSettings)
    {
        _matchTypeHandler?.Exit();

        if (!_matchTypeToHandler.TryGetValue(matchSettings.MatchType, out var matchTypeHandler))
        {
            // Victory condition doesnt exist
            Debugger.Break();
            return;
        }

        _matchTypeHandler = matchTypeHandler(matchSettings);
        _matchTypeHandler?.Enter();
    }
}
