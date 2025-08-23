using Godot;
using PongCSharp.Enums;
using PongCSharp.Autoloads;
using PongCSharp.Game.Paddle;

namespace PongCSharp.Game.Ball;

public partial class Ball : CharacterBody2D
{
    // Fields
    private int _currentBallSpeed;

    private Vector2 _direction;

    private readonly RandomNumberGenerator _rng = new();

    private float _timer = 0f;

    // Exports
    [Export]
    private int _ballSpeedIncreaseBy = 50;

    [Export]
    private int _baseBallSpeed = 300;

    [Export]
    private int _increaseBallSpeedEveryXSeconds = 1;

    // Lifecycles
    public override void _Ready()
    {
        base._Ready();
        _rng.Randomize();
        Reset();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        SpeedUpBall(delta);
        var collision = MoveBallAndCollide(delta);
        BounceBallOnCollision(collision);
    }

    // Setup
    public void Reset()
    {
        ResetPosition();
        ResetBallSpeed();
        ChooseRandomDirection();
    }

    private void ResetPosition()
    {
        var gameScreenSize = GetViewportRect().Size;
        Position = new Vector2
            (
                gameScreenSize.X / 2,
                gameScreenSize.Y / 2
            );
    }

    private void ChooseRandomDirection()
    {
        float randomDirection() => _rng.RandfRange(-1, 1);
        var randomizedDirection = new Vector2(randomDirection(), randomDirection());
        _direction = randomizedDirection.Normalized();
    }

    // Ball Speed
    private void SpeedUpBall(double delta)
    {
        if (_timer > _increaseBallSpeedEveryXSeconds)
        {
            _timer = 0f;
            _currentBallSpeed += _ballSpeedIncreaseBy;
            return;
        }

        _timer += (float)delta;
    }

    private void ResetBallSpeed()
    {
        _currentBallSpeed = _baseBallSpeed;
    }

    // Methods
    private KinematicCollision2D? MoveBallAndCollide(double delta)
    {
        var motionCalculation = _direction * _currentBallSpeed * (float)delta;
        var collision = MoveAndCollide(motionCalculation);
        return collision;
    }

    private void BounceBallOnCollision(KinematicCollision2D? collision)
    {
        if (collision is null)
            return;

        var collider = collision.GetCollider();

        if (collider is PaddleBase paddle)
        {
            // 1. Get relative hit position (Y-axis, assuming vertical paddle)
            var ballY = GlobalPosition.Y;
            var paddleY = paddle.GlobalPosition.Y;
            var paddleHeight = paddle.GetNode<CollisionShape2D>("CollisionShape2D").Shape.GetRect().Size.Y;

            // 2. Offset from center: -1 (top) to +1 (bottom)
            var offset = Mathf.Clamp((ballY - paddleY) / (paddleHeight / 2), -1f, 1f);

            // 3. Bounce and modify angle based on offset
            var normal = collision.GetNormal();
            var newDirection = _direction.Bounce(normal).Normalized();

            // 4. Modify X and Y to steer based on offset
            newDirection.Y += offset * 0.75f; // adjust 0.75f for sensitivity
            newDirection = newDirection.Normalized();

            // 5. Add small random jitter to break symmetry
            newDirection.Y += (float)GD.RandRange(-0.05f, 0.05f);

            _direction = newDirection.Normalized();

            return;
        }

        // Wall bounce or something else
        _direction = _direction.Bounce(collision.GetNormal()).Normalized();
    }
}
