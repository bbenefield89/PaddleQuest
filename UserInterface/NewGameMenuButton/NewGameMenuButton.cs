using Godot;
using PongCSharp.Autoloads;
using PongCSharp.Enums;
using PongCSharp.Extensions;
using System;
using System.Linq;

namespace PongCSharp.UserInterface;

public partial class NewGameMenuButton : MenuButton
{
    // Lifecycles
    public override void _EnterTree()
    {
        base._EnterTree();
        Subscribe();
        InitializeMenuItems();
    }

    // Setup
    private void Subscribe()
    {
        GetPopup().IdPressed += NewGameMenuButton_IdPressed;
    }

    private void InitializeMenuItems()
    {
        var matchTypes = (VictoryCondition[])Enum.GetValues(typeof(VictoryCondition));
        foreach (var matchType in matchTypes)
        {
            AddMenuItem(matchType);
        }
    }

    // Event Handlers
    private void NewGameMenuButton_IdPressed(long victoryConditionId)
    {
        var victoryCondition = (VictoryCondition)(int)victoryConditionId;
        VictoryConditionManager.Instance?.ChangeVictoryCondition(victoryCondition);
        SceneLoader.Instance?.SwitchToScene(SceneName.Game);
    }

    // Methods
    private void AddMenuItem(VictoryCondition victoryCondition)
    {
        if (!IsAvailableVictoryConditionSelected(victoryCondition))
            return;

        var victoryConditionId = (int)victoryCondition;
        var victoryConditionName = victoryCondition.GetDisplayNameOrDefault();

        GetPopup().AddItem(victoryConditionName, victoryConditionId);
    }

    private bool IsAvailableVictoryConditionSelected(VictoryCondition victoryCondition)
    {
        // A list of VictoryConditions that haven't been implemented yet
        var UnavailableVictoryConditions = new[]
        {
            VictoryCondition.Unknown,
            VictoryCondition.TimeLimit,
            VictoryCondition.SuddenDeath,
            VictoryCondition.Survival,
            VictoryCondition.LivesSystem,
            VictoryCondition.WinByMargin,
            VictoryCondition.ComboStreak,
            VictoryCondition.ScoreRaceWithObstacles,
            VictoryCondition.KingOfTheHill,
            VictoryCondition.TargetBased,
            VictoryCondition.ResourceMode,
            VictoryCondition.TerritoryControl
        };

        var isUnavailableVictoryConditionSelected = UnavailableVictoryConditions.Contains(victoryCondition);
        if (isUnavailableVictoryConditionSelected)
            return false;

        return true;
    }
}