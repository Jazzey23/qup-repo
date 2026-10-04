# q->up

A Blazor (.NET 8) and Tailwind CSS 4 implementation of the Figma “Build the squad” screen.

```sh
npm ci
dotnet run
```

Tailwind CSS compiles automatically during `dotnet build` or `dotnet run`. For CSS development, run `npm run css:watch` in a second terminal.

The screen is available at `/` and `/matchmaking/form-party`. It includes server region selection, Riot ID validation, party and squad sizing, optional connection details, and simulated matchmaking. The demo runs locally; it does not connect to a matchmaking service.

Original Figma SVGs are saved in `wwwroot/assets/figma`. Typography uses Arimo and Roboto Mono from Google Fonts with local system fallbacks.

The route pages compose `Components/Matchmaking/BuildSquadForm.razor`. Its child components handle the player checklist, regions, squad capacity, connection fields, and feedback. Form orchestration lives in `BuildSquadForm.razor.cs`, validation and squad rules in `Models/Matchmaking/MatchmakingRequest.cs`, and the cancellable demo search in the injected `Services/Matchmaking/SimulatedMatchmakingService.cs`.
