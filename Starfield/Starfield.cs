using Godot;
using System.Collections.Generic;

namespace PongCSharp;

public partial class Starfield : Node2D
{
    // Exports
    [Export] public int StarCount = 150;

    [Export] public float STAR_SIZE_MIN = 1.0f;

    [Export] public float STAR_SIZE_MAX = 3.0f;

    [Export] public float STAR_BRIGHTNESS_MIN = 0.2f;

    [Export] public float STAR_BRIGHTNESS_MAX = 0.8f;

    // Fields
    private readonly List<Star> _stars = [];

    private float _elapsedTime = 0f;

    // Lifecycles
    public override void _Ready()
    {
        base._Ready();
        GenerateStars();
    }

    public override void _Draw()
    {
        base._Draw();

        var screenSize = GetViewportRect().Size;
        DrawRect(new Rect2(Vector2.Zero, screenSize), new Color("#0B0C10"));

        foreach (var star in _stars)
        {
            var color = new Color(1f, 1f, 1f, star.Brightness);
            DrawCircle(star.Position, star.Size, color);
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        _elapsedTime += (float)delta;

        foreach (var star in _stars)
            star.UpdatePulse(_elapsedTime);

        QueueRedraw();
    }

    // Methods
    private void GenerateStars()
    {
        var viewportSize = GetViewportRect().Size;

        for (int i = 0; i < StarCount; i++)
        {
            var position = new Vector2(
                (float)GD.RandRange(0, viewportSize.X),
                (float)GD.RandRange(0, viewportSize.Y));

            var size = (float)GD.RandRange(STAR_SIZE_MIN, STAR_SIZE_MAX);
            var baseBrightness = (float)GD.RandRange(STAR_BRIGHTNESS_MIN, STAR_BRIGHTNESS_MAX);
            var offset = (float)GD.RandRange(0, Mathf.Tau);

            _stars.Add(new Star(position, baseBrightness, size, offset));
        }
    }
}

public class Star(Vector2 position, float baseBrightness, float size, float offset)
{
    public Vector2 Position = position;
    public float BaseBrightness = baseBrightness;
    public float Brightness = baseBrightness;
    public float PulseOffset = offset;
    public float Size = size;

    public void UpdatePulse(float time)
        => Brightness = BaseBrightness * (1.0f + 0.5f * Mathf.Sin(time + PulseOffset));
}

