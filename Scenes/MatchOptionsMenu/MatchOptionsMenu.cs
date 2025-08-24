using Godot;
using PongCSharp.Autoloads;
using PongCSharp.Enums;

namespace PongCSharp.Scenes;

public partial class MatchOptionsMenu : Control
{
    // Exports
    [Export]
    private TabContainer? _matchOptionsTabContainer;

    [Export]
    private Button? _startMatchButton;

    public override void _Ready()
    {
        base._Ready();
        Subscribe();
    }

    private void Subscribe()
        => _startMatchButton!.ButtonUp += StartMatchButton_OnButtonUp;

    private void StartMatchButton_OnButtonUp()
    {
        var currentTab = _matchOptionsTabContainer!.GetCurrentTabControl();
        var currentTabChildren = currentTab.GetChildren();
        var matchSettings = new MatchSettings
        {
            MatchType = MatchType.ScoreLimit,
            ScoreLimit = currentTabChildren[0].GetNode<TextEdit>("TextEdit").Text.ToInt(),
            TimeLimit = currentTabChildren[1].GetNode<TextEdit>("TextEdit").Text.ToInt()
        };

        MatchTypeManager.Instance!.ChangeMatchType(matchSettings);
        SceneLoader.Instance!.ChangeSceneTo(SceneName.Game);
    }

    public class MatchSettings
    {
        public MatchType MatchType { get; set; }
        public int ScoreLimit { get; set; }
        public int TimeLimit { get; set; }
    }
}