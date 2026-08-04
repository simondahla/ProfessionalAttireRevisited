namespace ProfessionalAttireRevisited
{
    // Canonical source of every translatable string this mod's equipment uses. Registered for
    // PLib .po translation via PLocalization.RegisterFromCallingClass() in Mod.OnLoad - field/
    // class names below become the .po msgctxt key path, e.g.
    // "ProfessionalAttireRevisited.ProfessionalAttireRevisitedStrings.ARTIST.NAME".
    // Fields must stay `public static LocString` (not const/readonly): translation is applied by
    // reflecting over these fields and replacing each one's value outright.
    public static class ProfessionalAttireRevisitedStrings
    {
        // Shared across all 12 outfits - every config's GenericName is literally "Clothing", so
        // this is one translation entry instead of twelve duplicates.
        public static LocString GENERIC_NAME_CLOTHING = "Clothing";

        public static class ARTIST
        {
            public static LocString NAME = "Artist's Outfit";
            public static LocString DESC = "Improves the creative capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "This smock prevents duplicants from worrying about spilling paint when making art.";
        }

        public static class BUILDING
        {
            public static LocString NAME = "Builder's Outfit";
            public static LocString DESC = "Improves the construction capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "This stylish and hardy vest helps duplicants to work effectively and safely while performing construction tasks.";
        }

        public static class COOK
        {
            public static LocString NAME = "Cook's Outfit";
            public static LocString DESC = "Improves the cooking capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "Wearing a {0} makes cooking a breeze.";
        }

        public static class DIGGING
        {
            public static LocString NAME = "Digger's Outfit";
            public static LocString DESC = "Improves the digging capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "Lightweight and strong mineral fibers keep this clothing from getting in a duplicant's way while digging.";
        }

        public static class DOCTOR
        {
            public static LocString NAME = "Doctor's Outfit";
            public static LocString DESC = "Improves the caring capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "Tending to duplicants in a {0} helps to speed up the recovery process.";
        }

        public static class FARMING
        {
            public static LocString NAME = "Farmer's Outfit";
            public static LocString DESC = "Improves the farming capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "Tending to plants in a {0} helps a duplicant to work more effectively.";
        }

        public static class HAULER
        {
            public static LocString NAME = "Hauler's Outfit";
            public static LocString DESC = "Improves the carrying capacity of one duplicant.";
            public static LocString RECIPE_DESC = "It's much easier to carry heavy loads while wearing a {0}.";
        }

        public static class PILOT
        {
            public static LocString NAME = "Pilot's Outfit";
            public static LocString DESC = "Improves the piloting capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "It's much easier to navigate a rocket while wearing a {0}.";
        }

        public static class RANCHING
        {
            public static LocString NAME = "Rancher's Outfit";
            public static LocString DESC = "Improves the ranching capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "Caring for critters in a {0} helps a duplicant to work more effectively.";
        }

        public static class SCIENTIST
        {
            public static LocString NAME = "Researcher's Outfit";
            public static LocString DESC = "Improves the learning capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "It's much easier to learn new things while wearing a {0}.";
        }

        public static class STRONG
        {
            public static LocString NAME = "Strongman's Outfit";
            public static LocString DESC = "Improves the strength capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "Tidying becomes a lot easier in a {0}.";
        }

        public static class TINKER
        {
            public static LocString NAME = "Engineer's Outfit";
            public static LocString DESC = "Improves the tinkering capabilities of one duplicant.";
            public static LocString RECIPE_DESC = "It's much easier to tinker with and operate things while wearing a {0}.";
        }
    }
}
