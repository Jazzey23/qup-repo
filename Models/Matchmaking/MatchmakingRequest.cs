using System.ComponentModel.DataAnnotations;

namespace qup_repo.Models.Matchmaking;

public sealed class MatchmakingRequest : IValidatableObject
{
    private int partySize = 1;
    private int squadSize = 5;

    [Required] public string Game { get; set; } = "VALORANT";
    [Required] public string Mode { get; set; } = "Competitive";
    [Required] public string Rank { get; set; } = "Gold 2";

    [Required(ErrorMessage = "Enter your player identifier or use the demo ID.")]
    [StringLength(64)]
    public string PlayerId { get; set; } = "";

    public HashSet<string> SelectedRegions { get; set; } = ["SG1", "TYO", "HK"];

    [Range(1, 4)]
    public int PartySize
    {
        get => partySize;
        set
        {
            partySize = Math.Clamp(value, 1, 4);
            squadSize = Math.Max(squadSize, partySize + 1);
        }
    }

    [Range(2, 5)]
    public int SquadSize
    {
        get => squadSize;
        set
        {
            squadSize = Math.Clamp(value, 2, 5);
            partySize = Math.Min(partySize, squadSize - 1);
        }
    }

    public int TeammatesNeeded => SquadSize - PartySize;
    [StringLength(100)] public string PartyCode { get; set; } = "";
    [StringLength(200)] public string DiscordInvite { get; set; } = "";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var parts = PlayerId.Trim().Split('#');
        if (parts.Length != 2 || string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]))
        {
            yield return new("Use your Riot ID in the format PlayerName#1234.", [nameof(PlayerId)]);
        }

        if (SelectedRegions.Count == 0)
        {
            yield return new("Select at least one server region.", [nameof(SelectedRegions)]);
        }

        if (!string.IsNullOrWhiteSpace(DiscordInvite) && !IsDiscordInvite(DiscordInvite))
        {
            yield return new("Enter a valid HTTPS Discord invite or leave it empty.", [nameof(DiscordInvite)]);
        }
    }

    private static bool IsDiscordInvite(string value)
    {
        if (!Uri.TryCreate(value.Trim(), UriKind.Absolute, out var invite) || invite.Scheme != Uri.UriSchemeHttps)
        {
            return false;
        }

        return (invite.Host == "discord.gg" && invite.AbsolutePath.Trim('/').Length > 0)
            || (invite.Host == "discord.com" && invite.AbsolutePath.StartsWith("/invite/", StringComparison.Ordinal)
                && invite.AbsolutePath[8..].Trim('/').Length > 0);
    }
}
