using PongCSharp.Constants;
using System.ComponentModel.DataAnnotations;

namespace PongCSharp.Enums;

public enum SceneName
{
    Unknown,

    [Display(Name = SceneNames.MainMenu)]
    MainMenu,

    [Display(Name = SceneNames.Game)]
    Game
}
