using System.ComponentModel.DataAnnotations;

namespace PongCSharp.Enums;

public enum MatchType
{
    [Display(Name = "Unknown", Description = "Unspecified or invalid match type.")]
    Unknown,

    [Display(Name = "Score Limit", Description = "First player to reach a set score wins.")]
    ScoreLimit,

    [Display(Name = "Time Limit", Description = "Game ends after a set time; highest score wins.")]
    TimeLimit,

    [Display(Name = "Sudden Death", Description = "After a tie, the first player to score wins.")]
    SuddenDeath,

    [Display(Name = "Survival", Description = "One ball only; the first player to miss loses.")]
    Survival,

    [Display(Name = "Lives System", Description = "Each player has a set number of lives; last standing wins.")]
    LivesSystem,

    [Display(Name = "Win By Margin", Description = "A player must lead by at least two points to win.")]
    WinByMargin,

    [Display(Name = "Combo Streak", Description = "First player to score a set number of consecutive points wins.")]
    ComboStreak,

    [Display(Name = "Score Race with Obstacles", Description = "Reach the target score while dodging hazards or using power-ups.")]
    ScoreRaceWithObstacles,

    [Display(Name = "King of the Hill", Description = "Maintain possession of the ball the longest to win.")]
    KingOfTheHill,

    [Display(Name = "Target Based", Description = "Score by hitting special targets; first to reach the target score wins.")]
    TargetBased,

    [Display(Name = "Resource Mode", Description = "Score to generate energy; fill your resource bar first to win.")]
    ResourceMode,

    [Display(Name = "Territory Control", Description = "Push your opponent’s paddle line back until they have no space left.")]
    TerritoryControl
}
