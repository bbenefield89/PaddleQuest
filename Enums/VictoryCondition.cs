using System.ComponentModel;

namespace PongCSharp.Enums;

public enum VictoryCondition
{
    Unknown,

    [Description("First player to reach a set score wins.")]
    ScoreLimit,

    [Description("The game ends after a time limit; highest score wins.")]
    TimeLimit,

    [Description("Sudden death: first to score after a tie wins.")]
    SuddenDeath,

    [Description("One ball only; first player to miss loses.")]
    Survival,

    [Description("Each player has a set number of lives; last standing wins.")]
    LivesSystem,

    [Description("A player must lead by at least two points to win.")]
    WinByMargin,

    [Description("First to reach a streak of consecutive points (e.g., 5 in a row) wins.")]
    ComboStreak,

    [Description("Reach a score before your opponent while hazards or power-ups appear.")]
    ScoreRaceWithObstacles,

    [Description("Keep possession of the ball for the longest cumulative time.")]
    KingOfTheHill,

    [Description("Score points by hitting special targets; first to reach the target wins.")]
    TargetBased,

    [Description("Earn energy by scoring; first to fill a resource bar wins.")]
    ResourceMode,

    [Description("Force your opponent’s paddle line back until they have no space left.")]
    TerritoryControl
}