using Godot;
using PongCSharp.Autoloads;
using System.Diagnostics;

namespace PongCSharp.Game.Paddle;

public abstract partial class PaddleBase : CharacterBody2D
{
    // Exports
    [Export]
    private int _paddleSpeed = 300;

    [Export]
    private CollisionShape2D? _collisionShape;

    // Lifecycles
    public override void _EnterTree()
    {
        base._EnterTree();
        Subscribe();
    }

    public override void _Ready()
    {
        base._Ready();
        Validate();
        Reset();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        Unsubscribe();
    }

    // Setup
    private void Subscribe()
    {
        GlobalEventBus.Instance?.GameReset += GlobalEventBus_GameReset;
    }

    private void Unsubscribe()
    {
        GlobalEventBus.Instance?.GameReset -= GlobalEventBus_GameReset;
    }

    // Event handlers
    private void GlobalEventBus_GameReset()
    {
        Reset();
    }

    // Virtuals
    protected virtual void MovePaddle(double delta, string upAction, string downAction)
    {
        var yAxis = Input.GetAxis(upAction, downAction);
        var direction = new Vector2(0f, yAxis).Normalized();

        MoveAndCollide(direction * _paddleSpeed * (float)delta);
    }

    // Methods
    private void Reset()
    {
        ResetPosition();
    }

    private void ResetPosition()
    {
        var centerOfScreenYAxis = GetViewportRect().Size.Y / 2;
        var paddleDimensions = GetPaddleDimensions();

        Position = new Vector2(
            Position.X,
            centerOfScreenYAxis - (paddleDimensions.Y / 2));
    }

    private Vector2 GetPaddleDimensions()
    {
        if (_collisionShape is null)
        {
            // _collisionShape was never set
            Debugger.Break();
            GD.PushError("_collisionShape was never set");
            GetTree().Quit();
            return Vector2.Zero;
        }

        var rectangleShape2D = (RectangleShape2D)_collisionShape.Shape;
        return rectangleShape2D.Size;
    }

    // Validation
    private void Validate()
    {
        var isValid = true;

        if (_collisionShape is null)
        {
            // _collisionShape was never set
            Debugger.Break();
            GD.PushError("_collisionShape was never set");
            isValid = false;
        }

        if (!isValid)
        {
            GetTree().Quit();
        }
    }
}
