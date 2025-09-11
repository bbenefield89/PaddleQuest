using PongCSharp.Constants;
using System.ComponentModel.DataAnnotations;

namespace PongCSharp.Enums;

public enum InputAction
{
    None,

    // Misc Actions
    [Display(Name = InputActions.PauseGame)]
    PauseGame,

    // Player One Actions
    [Display(Name = InputActions.PlayerOneMoveUp)]
    PlayerOneMoveUp,

    [Display(Name = InputActions.PlayerOneMoveDown)]
    PlayerOneMoveDown,

    // Player Two Actions
    [Display(Name = InputActions.PlayerTwoMoveUp)]
    PlayerTwoMoveUp,

    [Display(Name = InputActions.PlayerTwoMoveDown)]
    PlayerTwoMoveDown,
}