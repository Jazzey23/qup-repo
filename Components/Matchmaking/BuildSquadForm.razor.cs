using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using qup_repo.Models.Matchmaking;
using qup_repo.Services.Matchmaking;

namespace qup_repo.Components.Matchmaking;

public partial class BuildSquadForm : IDisposable
{
    private readonly MatchmakingRequest request = new();
    private EditContext editContext = default!;
    private PlayerChecklist? playerChecklist;
    private MatchmakingResult? result;
    private CancellationTokenSource? searchCancellation;
    private bool isSearching;

    [Inject] private SimulatedMatchmakingService MatchmakingService { get; set; } = default!;

    protected override void OnInitialized()
    {
        editContext = new EditContext(request);
        editContext.OnFieldChanged += HandleFieldChanged;
    }

    private void HandleFieldChanged(object? sender, FieldChangedEventArgs args)
    {
        searchCancellation?.Cancel();
        isSearching = false;
        result = null;
        StateHasChanged();
    }

    private async Task FindTeammatesAsync()
    {
        if (isSearching)
        {
            return;
        }

        using var cancellation = new CancellationTokenSource();
        searchCancellation = cancellation;
        result = null;
        isSearching = true;
        playerChecklist?.CloseRegions();

        try
        {
            result = await MatchmakingService.FindTeammatesAsync(request, cancellation.Token);
        }
        catch (OperationCanceledException) when (cancellation.IsCancellationRequested)
        {
            // Editing the form or navigating away cancels the pending demo request.
        }
        finally
        {
            if (ReferenceEquals(searchCancellation, cancellation))
            {
                searchCancellation = null;
                isSearching = false;
            }
        }
    }

    public void Dispose()
    {
        editContext.OnFieldChanged -= HandleFieldChanged;
        searchCancellation?.Cancel();
    }
}
