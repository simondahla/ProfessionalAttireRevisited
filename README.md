# Professional Attire

Adds 12 profession-themed clothing items to Oxygen Not Included. Each grants +2 to one duplicant
attribute while worn - an Artist's outfit for Art, a Doctor's outfit for Caring, and so on.

Built on Davis Cook's original 2019 `ProfessionalAttire` mod (MIT licensed) - ported and updated
for the current game API. See `LICENSE` for the original copyright notice.

Uses [PLib](https://github.com/peterhaneve/ONIMods) (Peter Han, MIT licensed) for the in-game
options menu.

## Building

```bash
./scripts/fetch-reference-dlls.sh
dotnet build
```

## Deploying locally

```bash
./deploy.sh
```
