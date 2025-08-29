using Godot;
using PongCSharp.Autoloads;
using PongCSharp.Enums;
using PongCSharp.Models;

namespace PongCSharp.Scenes;

public partial class MatchOptionsMenu : Control
{
    // Exports
    [Export]
    private TabContainer? _matchOptionsTabContainer;

    [Export]
    private Button? _startMatchButton;

    // Lifecycles
    public override void _Ready()
    {
        base._Ready();
        Subscribe();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        Unsubscribe();
    }

    // Setup
    private void Subscribe() => _startMatchButton!.ButtonUp += StartMatchButton_OnButtonUp;

    private void Unsubscribe() => _startMatchButton!.ButtonUp -= StartMatchButton_OnButtonUp;

    // Methods
    private void StartMatchButton_OnButtonUp()
    {
        var currentTab = _matchOptionsTabContainer!.GetCurrentTabControl();
        var settingsContainer = currentTab.GetNode<HBoxContainer>("SettingsContainer");
        var scoreLimitInput = settingsContainer.GetNode<SpinBox>("ScoreLimitInput");
        var timeLimitInput = settingsContainer.GetNode<SpinBox>("TimeLimitInput");

        var matchSettings = new MatchSettings
        {
            MatchType = MatchType.ScoreLimit,
            ScoreLimit = (int)scoreLimitInput.Value,
            TimeLimit = (int)timeLimitInput.Value
        };

        MatchTypeManager.Instance!.LoadMatchType(matchSettings);
        SceneLoader.Instance!.ChangeSceneTo(SceneName.Game);
    }
}