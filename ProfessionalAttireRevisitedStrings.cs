using STRINGS;

namespace ProfessionalAttireRevisited
{
    // Canonical source of every translatable string this mod's equipment uses. Registered for
    // PLib .po translation via PLocalization.RegisterFromCallingClass() in Mod.OnLoad - field/
    // class names below become the .po msgctxt key path, e.g.
    // "ProfessionalAttireRevisited.ProfessionalAttireRevisitedStrings.ARTIST.NAME".
    // Fields must stay `public static LocString` (not const/readonly): translation is applied by
    // reflecting over these fields and replacing each one's value outright.
    //
    // NAME fields are wrapped in UI.FormatAsLink(text, Id), matching vanilla's own convention
    // (e.g. STRINGS.EQUIPMENT.PREFABS.FUNKY_VEST.NAME) - without it, the outfit's name renders as
    // plain text in the recipe/crafting UI instead of the clickable link to its database entry.
    public static class ProfessionalAttireRevisitedStrings
    {
        // Shared across all 12 outfits - every config's GenericName is literally "Clothing", so
        // this is one translation entry instead of twelve duplicates.
        public static LocString GENERIC_NAME_CLOTHING = "Clothing";

        public static class ARTIST
        {
            public static LocString NAME = UI.FormatAsLink("Artist's Outfit", ArtistAttireConfig.Id);
            public static LocString DESC = "Improves the creative capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "This smock prevents duplicants from worrying about spilling paint when making art.";
        }

        public static class BUILDING
        {
            public static LocString NAME = UI.FormatAsLink("Builder's Outfit", BuildingAttireConfig.Id);
            public static LocString DESC = "Improves the construction capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "This stylish and hardy vest helps duplicants to work effectively and safely while performing construction tasks.";
        }

        public static class COOK
        {
            public static LocString NAME = UI.FormatAsLink("Cook's Outfit", CookAttireConfig.Id);
            public static LocString DESC = "Improves the cooking capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "Wearing a {0} makes cooking a breeze.";
        }

        public static class DIGGING
        {
            public static LocString NAME = UI.FormatAsLink("Digger's Outfit", DiggingAttireConfig.Id);
            public static LocString DESC = "Improves the digging capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "Lightweight and strong mineral fibers keep this clothing from getting in a duplicant's way while digging.";
        }

        public static class DOCTOR
        {
            public static LocString NAME = UI.FormatAsLink("Doctor's Outfit", DoctorAttireConfig.Id);
            public static LocString DESC = "Improves the caring capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "Tending to duplicants in a {0} helps to speed up the recovery process.";
        }

        public static class FARMING
        {
            public static LocString NAME = UI.FormatAsLink("Farmer's Outfit", FarmingAttireConfig.Id);
            public static LocString DESC = "Improves the farming capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "Tending to plants in a {0} helps a duplicant to work more effectively.";
        }

        public static class HAULER
        {
            public static LocString NAME = UI.FormatAsLink("Hauler's Outfit", HaulerAttireConfig.Id);
            public static LocString DESC = "Improves the carrying capacity of one duplicant.";
            public static LocString RECIPE_DESC = "It's much easier to carry heavy loads while wearing a {0}.";
        }

        public static class PILOT
        {
            public static LocString NAME = UI.FormatAsLink("Pilot's Outfit", PilotAttireConfig.Id);
            public static LocString DESC = "Improves the piloting capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "It's much easier to navigate a rocket while wearing a {0}.";
        }

        public static class RANCHING
        {
            public static LocString NAME = UI.FormatAsLink("Rancher's Outfit", RanchingAttireConfig.Id);
            public static LocString DESC = "Improves the ranching capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "Caring for critters in a {0} helps a duplicant to work more effectively.";
        }

        public static class SCIENTIST
        {
            public static LocString NAME = UI.FormatAsLink("Researcher's Outfit", ScientistAttireConfig.Id);
            public static LocString DESC = "Improves the learning capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "It's much easier to learn new things while wearing a {0}.";
        }

        public static class STRONG
        {
            public static LocString NAME = UI.FormatAsLink("Strongman's Outfit", StrongAttireConfig.Id);
            public static LocString DESC = "Improves the strength capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "Tidying becomes a lot easier in a {0}.";
        }

        public static class TINKER
        {
            public static LocString NAME = UI.FormatAsLink("Engineer's Outfit", TinkerAttireConfig.Id);
            public static LocString DESC = "Improves the tinkering capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "It's much easier to tinker with and operate things while wearing a {0}.";
        }
    }
}
