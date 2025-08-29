using Godot;
using PongCSharp.Autoloads;
using PongCSharp.Enums;
using PongCSharp.Models;
using System.Collections.Generic;
using System.Diagnostics;

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
        Validate();
        Subscribe();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        Unsubscribe();
    }

    // Setup
    private void Subscribe()
        => GlobalEventBus.Instance!.StartMatchButtonUp += StartMatchButton_OnStartMatchButtonUp;

    private void Unsubscribe()
        => GlobalEventBus.Instance!.StartMatchButtonUp -= StartMatchButton_OnStartMatchButtonUp;

    // Methods
    private void StartMatchButton_OnStartMatchButtonUp()
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

    // Validation
    private void Validate()
    {
        var errorMessages = new List<string>();

        if (_matchOptionsTabContainer is null)
            errorMessages.Add("");

        if (_startMatchButton is null)
            errorMessages.Add("");

        if (errorMessages.Count == 0)
            return;

        Debugger.Break();
        GD.PushError(string.Join("\n", errorMessages));
        GetTree().Quit();
    }
}