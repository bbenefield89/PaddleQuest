using Godot;
using PongCSharp.Enums;
using PongCSharp.Extensions;

public partial class SceneLoader : Node
{
    public static SceneLoader? Instance { get; private set; }

    public SceneLoader()
    {
        Instance = this;
    }

    public void SwitchToScene(SceneName sceneName)
    {
        var scenePath = sceneName.GetDisplayNameOrDefault();
        var packedScene = ResourceLoader.Load<PackedScene>(scenePath);

        GetTree().ChangeSceneToPacked(packedScene);
    }
}
