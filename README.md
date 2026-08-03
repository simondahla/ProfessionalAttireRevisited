# Professional Attire Revisited

Adds 12 profession-themed clothing items to Oxygen Not Included. Each grants +2 to one duplicant
attribute while worn - an Artist's outfit for Art, a Doctor's outfit for Caring, and so on.

Built on Davis Cook's original 2019 [`Professional Attire`](https://github.com/daviscook477/ONI-Mods/tree/master/src/ProfessionalAttire) mod (MIT licensed code) - ported and
updated for the current game API. See `LICENSE` for the copyright notice and license scope.

Uses [PLib](https://github.com/peterhaneve/ONIMods) (Peter Han, MIT licensed) for the in-game
options menu.

Outfits currently reuse the vanilla Snazzy Suits sprite as a placeholder - the original mod's
unique art doesn't load under the current game's mod content pipeline and isn't included here
(it isn't licensed for reuse). Unique art is a separate, unresolved piece of work.

## Building

```bash
./scripts/fetch-reference-dlls.sh
dotnet build
```

## Running locally

```bash
./dev.sh
```
<!-- CI/CD pipeline verified end-to-end -->
<!-- verify compiled build attaches to release -->
