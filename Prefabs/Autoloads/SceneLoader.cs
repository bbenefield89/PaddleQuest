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

    public void ChangeSceneTo(SceneName sceneName)
    {
        var scenePath = sceneName.GetDisplayAttributeOrDefault(EnumDisplayAttribute.Name.ToString());
        var packedScene = ResourceLoader.Load<PackedScene>(scenePath);

        GetTree().ChangeSceneToPacked(packedScene);
    }
}
