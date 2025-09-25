using Godot;
using PongCSharp.Autoloads;
using PongCSharp.Domain.GameStateHandlers;
using PongCSharp.Enums;
using PongCSharp.GameStateHandlers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PongCSharp.Prefabs.Autoloads;

public partial class GameStateManager : Node
{
    // Fields
    private ReadOnlyDictionary<GameState, IGameStateHandler>? _gameStates;    

    // Properties
    public IGameStateHandler? CurrentGameState { get; private set; }

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

        if (_gameStates is null || !_gameStates.TryGetValue(gameState, out var nextGameStateHandler))
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
        var gameStates = new Dictionary<GameState, IGameStateHandler>
        {
            [GameState.MainMenu] = new MainMenuGameStateHandler(),
            [GameState.Playing] = new PlayingGameStateHandler(),
            [GameState.Paused] = new PausedGameStateHandler()
        };

        _gameStates = gameStates.AsReadOnly();
    }

    private static void Subscribe()
    {
        GlobalEventBus.Instance?.GameStarted += GlobalEventBus_GameStarted;
        GlobalEventBus.Instance?.GameReset += GlobalEventBus_GameReset;
    }

    private static void Unsubscribe()
    {
        GlobalEventBus.Instance?.GameStarted -= GlobalEventBus_GameStarted;
        GlobalEventBus.Instance?.GameReset -= GlobalEventBus_GameReset;
    }

    // Event Handlers
    private static void GlobalEventBus_GameStarted()
        => Instance?.ChangeState(GameState.Playing);

    private static void GlobalEventBus_GameReset()
        => Instance?.ChangeState(GameState.Playing);
}