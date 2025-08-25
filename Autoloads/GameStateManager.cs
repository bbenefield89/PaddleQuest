using Godot;
using PongCSharp.Enums;
using PongCSharp.Autoloads;
using PongCSharp.GameStateHandlers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PongCSharp.Autoloads;

public partial class GameStateManager : Node
{
    // Fields
    public IGameStateHandler? CurrentGameState { get; private set; }

    private ReadOnlyDictionary<GameState, IGameStateHandler>? _gameStates;

    // Properties
    public static GameStateManager? Instance { get; private set; }

    // Lifecycle Methods
    public override void _EnterTree()
    {
        base._EnterTree();
        InitializeGameStates();
        Subscribe();
    }

    public override void _Ready()
    {
        base._Ready();
        Instance = this;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        Unsubscribe();
    }

    // State Management
    public void ChangeState(GameState gameState)
    {
        CurrentGameState?.Exit();

        IGameStateHandler? nextGameStateHandler;
        if (_gameStates is null || !_gameStates.TryGetValue(gameState, out nextGameStateHandler))
        {
            Debugger.Break();
            return;
        }

        CurrentGameState = nextGameStateHandler;
        CurrentGameState.Enter();
    }

    // Setup Methods
    private void InitializeGameStates()
    {
        var gameStates = new Dictionary<GameState, IGameStateHandler>()
        {
            [GameState.MainMenu] = new MainMenuGameStateHandler(),
            [GameState.Playing] = new PlayingGameStateHandler(),
            [GameState.Paused] = new PausedGameStateHandler(GetTree())
        };

        _gameStates = gameStates.AsReadOnly();
    }

    private void Subscribe()
    {
        GlobalEventBus.Instance?.GameStarted += GlobalEventBus_GameStarted;
        GlobalEventBus.Instance?.GameReset += GlobalEventBus_GameReset;
        GlobalEventBus.Instance?.VictoryConditionAchieved += GlobalEventBus_VictoryConditionAchieved;
    }

    private void Unsubscribe()
    {
        GlobalEventBus.Instance?.GameStarted -= GlobalEventBus_GameStarted;
        GlobalEventBus.Instance?.GameReset -= GlobalEventBus_GameReset;
        GlobalEventBus.Instance?.VictoryConditionAchieved -= GlobalEventBus_VictoryConditionAchieved;
    }

    // Event Handlers
    private void GlobalEventBus_GameStarted()
        => Instance?.ChangeState(GameState.Playing);

    private void GlobalEventBus_GameReset()
        => Instance?.ChangeState(GameState.Playing);

    private void GlobalEventBus_VictoryConditionAchieved(GoalSide _)
        => Instance?.ChangeState(GameState.Paused);
}
