namespace qup_repo.Models.Matchmaking;

public sealed record ServerRegion(string Code, string Name);

public static class MatchmakingOptions
{
    public static IReadOnlyList<string> Games { get; } = ["VALORANT"];
    public static IReadOnlyList<string> Modes { get; } = ["Competitive", "Unrated", "Swiftplay", "Spike Rush", "Deathmatch"];
    public static IReadOnlyList<string> Ranks { get; } =
    [
        "Unranked", "Iron 1", "Iron 2", "Iron 3", "Bronze 1", "Bronze 2", "Bronze 3",
        "Silver 1", "Silver 2", "Silver 3", "Gold 1", "Gold 2", "Gold 3",
        "Platinum 1", "Platinum 2", "Platinum 3", "Diamond 1", "Diamond 2", "Diamond 3",
        "Ascendant 1", "Ascendant 2", "Ascendant 3", "Immortal 1", "Immortal 2", "Immortal 3", "Radiant"
    ];
    public static IReadOnlyList<ServerRegion> Regions { get; } =
    [
        new("SG1", "Singapore 1"), new("SG2", "Singapore 2"), new("TYO", "Tokyo"),
        new("HK", "Hong Kong"), new("SYD", "Sydney"), new("MUM", "Mumbai")
    ];
}
