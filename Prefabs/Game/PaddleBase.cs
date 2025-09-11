using Godot;
using PongCSharp.Autoloads;
using System.Collections.Generic;
using System.Diagnostics;

namespace PongCSharp.Game.Paddle;

public abstract partial class PaddleBase : CharacterBody2D
{
    // Exports
    [Export]
    private CollisionShape2D? _collisionShape;

    [Export]
    private int _paddleSpeed = 300;

    // Props
    protected Vector2 PaddleDimensions { get; private set; }

    // Lifecycles
    public override void _Ready()
    {
        base._Ready();

        PaddleDimensions = GetPaddleDimensions();

        Subscribe();
        Reset();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        MovePaddle(delta);
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

    // Event handlers
    private void GlobalEventBus_GameReset()
        => Reset();

    // Virtuals
    protected virtual void MovePaddle(double delta)
    {
        var yAxis = GetMovementAxis();
        var direction = new Vector2(0f, yAxis).Normalized();

        MoveAndCollide(direction * _paddleSpeed * (float)delta);
    }

    protected abstract float GetMovementAxis();

    // Methods
    private void Reset()
        => ResetPosition();

    private void ResetPosition()
    {
        var centerOfScreenYAxis = GetViewportRect().Size.Y / 2;

        Position = new Vector2(
            Position.X,
            centerOfScreenYAxis - (PaddleDimensions.Y / 2));
    }

    private Vector2 GetPaddleDimensions()
    {
        var rectangleShape2D = (RectangleShape2D)_collisionShape!.Shape;
        return rectangleShape2D.Size;
    }

    // Validation
    protected virtual void Validate()
    {
        var errorMessages = new List<string>();

        if (_collisionShape is null)
            errorMessages.Add("_collisionShape was never set");

        if (errorMessages.Count > 0)
        {
            GD.PushError(string.Join("\n", errorMessages));
            Debugger.Break();
            GetTree().Quit();
        }
    }
}
