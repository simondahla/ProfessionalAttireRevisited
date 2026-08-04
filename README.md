# Professional Attire Revisited

Adds 12 profession clothing items to Oxygen Not Included, craftable at the Clothing Fabricator.
Each one grants +2 to a duplicant attribute while worn - an Artist's outfit for Art, a Doctor's
outfit for Caring, and so on. Fiber and ingredient costs for every outfit are configurable
individually through PLib's in-game options menu, so you can tune or disable recipes without
editing files.

Built on Davis Cook's original 2019 [`Professional Attire`](https://github.com/daviscook477/ONI-Mods/tree/master/src/ProfessionalAttire) mod (MIT licensed code) - ported and
updated for the current game API. See `LICENSE` for the copyright notice and license scope.

Uses [PLib](https://github.com/peterhaneve/ONIMods) (Peter Han, MIT licensed) for the in-game
options menu.

## Outfits

Every recipe uses *fiber* as its base, plus the second ingredient below
where one is listed. All costs shown are the mod's defaults; every number is editable per outfit
in the options menu.

| Outfit | Attribute | Fiber cost | 2nd ingredient | 2nd ingredient cost |
|---|---|---|---|---|
| Artist's | Art | 3 | Diamond | 400 kg |
| Builder's | Construction | 3 | Iron | 400 kg |
| Cook's | Cooking | 3 | Polypropylene | 400 kg |
| Digger's | Digging | 3 | Obsidian | 2,000 kg |
| Doctor's | Caring | 3 | Intermediate Cure | 2 |
| Farmer's | Botanist | 6 | - | - |
| Rancher's | Ranching | 6 | - | - |
| Researcher's | Learning | 3 | Data Bank | 20 |
| Strongman's | Strength | 6 | - | - |
| Engineer's | Machinery | 3 | Refined Carbon | 2,000 kg |
| Pilot's | SpaceNavigation | 3 | Steel | 400 kg |
| Hauler's | CarryAmount | 6 | - | - |

The Pilot's outfit also cuts down rocket mission time. The vanilla mission-duration calculation
only checks a pilot's mastered skill perks, since vanilla has no equipment that boosts
SpaceNavigation for it to account for. This mod patches that calculation to also add the Pilot's
outfit's bonus, matching the scale of the vanilla "Rocket Piloting II" perk.

Outfits currently reuse the vanilla Snazzy Suits sprite as a placeholder - the original mod's
unique art doesn't load under the current game's mod content pipeline and isn't included here
(it isn't licensed for reuse). Unique art is a separate, unresolved piece of work; see Roadmap
below.

## Roadmap

Rough shape of what's next, no timeline attached:

- [ ] Translation support
- [ ] Work out some logic to be able to disable mod without game crashing… look at save safe funnction by Aki? https://github.com/aki-art/ONI-Mods
- [ ] An "advanced" tier of outfits with stronger bonuses and pricier ingredients.
- [ ] Real *kanim* art and animations for each outfit, replacing the Snazzy Suits placeholder.
- [ ] General visual polish on the clothing sprites once real art exists.

## Building

```bash
./scripts/fetch-reference-dlls.sh
dotnet build
```

After a game update, bump `minimumSupportedBuild` in `mod_info.yaml` to match the new build number, then commit:

```bash
dotnet run --project tools/BuildNumberSync
```

## Running locally

```bash
./dev.sh
```
<!-- CI/CD pipeline verified end-to-end -->
<!-- verify compiled build attaches to release -->
<!-- test -->
