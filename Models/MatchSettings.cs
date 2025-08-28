using PongCSharp.Enums;

namespace PongCSharp.Models;

public class MatchSettings
{
    public MatchType MatchType { get; set; }

    public int ScoreLimit { get; set; }

    public int TimeLimit { get; set; }
}