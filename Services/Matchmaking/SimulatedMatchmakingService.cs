using qup_repo.Models.Matchmaking;

namespace qup_repo.Services.Matchmaking;

public sealed record MatchmakingResult(string Game, string Mode, string Rank, IReadOnlyList<string> Teammates);

public sealed class SimulatedMatchmakingService
{
    public async Task<MatchmakingResult> FindTeammatesAsync(MatchmakingRequest request, CancellationToken cancellationToken)
    {
        var result = new MatchmakingResult(request.Game, request.Mode, request.Rank,
            Enumerable.Range(1, request.TeammatesNeeded).Select(i => $"Teammate_{i:00}").ToArray());
        await Task.Delay(TimeSpan.FromMilliseconds(1500), cancellationToken);
        return result;
    }
}
