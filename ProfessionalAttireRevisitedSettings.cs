using PeterHan.PLib.Options;

namespace ProfessionalAttireRevisited
{
    [ConfigFile(IndentOutput: true)]
    public class ProfessionalAttireRevisitedSettings
    {
        [Option("Fiber Cost", "How much fiber (Basic Fabric or Feather Fabric) is required.", "Artist's Outfit", Format = "F0")]
        [Limit(1, 8)]
        public int ArtistFiberCost { get; set; } = 3;

        [Option("Diamond Cost (kg)", "How much Diamond is required.", "Artist's Outfit", Format = "F0")]
        [Limit(100, 1000)]
        public int ArtistDiamondCost { get; set; } = 400;

        [Option("Fiber Cost", "How much fiber (Basic Fabric or Feather Fabric) is required.", "Builder's Outfit", Format = "F0")]
        [Limit(1, 8)]
        public int BuilderFiberCost { get; set; } = 3;

        [Option("Iron Cost (kg)", "How much Iron is required.", "Builder's Outfit", Format = "F0")]
        [Limit(100, 1000)]
        public int BuilderIronCost { get; set; } = 400;

        [Option("Fiber Cost", "How much fiber (Basic Fabric or Feather Fabric) is required.", "Cook's Outfit", Format = "F0")]
        [Limit(1, 8)]
        public int CookFiberCost { get; set; } = 3;

        [Option("Polypropylene Cost (kg)", "How much Polypropylene is required.", "Cook's Outfit", Format = "F0")]
        [Limit(100, 1000)]
        public int CookPolypropyleneCost { get; set; } = 400;

        [Option("Fiber Cost", "How much fiber (Basic Fabric or Feather Fabric) is required.", "Digger's Outfit", Format = "F0")]
        [Limit(1, 8)]
        public int DiggerFiberCost { get; set; } = 3;

        [Option("Obsidian Cost (kg)", "How much Obsidian is required.", "Digger's Outfit", Format = "F0")]
        [Limit(500, 4000)]
        public int DiggerObsidianCost { get; set; } = 2000;

        [Option("Fiber Cost", "How much fiber (Basic Fabric or Feather Fabric) is required.", "Doctor's Outfit", Format = "F0")]
        [Limit(1, 8)]
        public int DoctorFiberCost { get; set; } = 3;

        [Option("Intermediate Cure Cost", "How many Intermediate Cures are required.", "Doctor's Outfit", Format = "F0")]
        [Limit(1, 10)]
        public int DoctorCureCost { get; set; } = 2;

        [Option("Fiber Cost", "How much fiber (Basic Fabric or Feather Fabric) is required.", "Farmer's Outfit", Format = "F0")]
        [Limit(1, 8)]
        public int FarmerFiberCost { get; set; } = 6;

        [Option("Fiber Cost", "How much fiber (Basic Fabric or Feather Fabric) is required.", "Rancher's Outfit", Format = "F0")]
        [Limit(1, 8)]
        public int RancherFiberCost { get; set; } = 6;

        [Option("Fiber Cost", "How much fiber (Basic Fabric or Feather Fabric) is required.", "Researcher's Outfit", Format = "F0")]
        [Limit(1, 8)]
        public int ResearcherFiberCost { get; set; } = 3;

        [Option("Data Bank Cost", "How many Data Banks are required.", "Researcher's Outfit", Format = "F0")]
        [Limit(5, 50)]
        public int ResearcherDatabankCost { get; set; } = 20;

        [Option("Fiber Cost", "How much fiber (Basic Fabric or Feather Fabric) is required.", "Strongman's Outfit", Format = "F0")]
        [Limit(1, 8)]
        public int StrongmanFiberCost { get; set; } = 6;

        [Option("Fiber Cost", "How much fiber (Basic Fabric or Feather Fabric) is required.", "Engineer's Outfit", Format = "F0")]
        [Limit(1, 8)]
        public int EngineerFiberCost { get; set; } = 3;

        [Option("Refined Carbon Cost (kg)", "How much Refined Carbon is required.", "Engineer's Outfit", Format = "F0")]
        [Limit(500, 4000)]
        public int EngineerCarbonCost { get; set; } = 2000;

        [Option("Fiber Cost", "How much fiber (Basic Fabric or Feather Fabric) is required.", "Pilot's Outfit", Format = "F0")]
        [Limit(1, 8)]
        public int PilotFiberCost { get; set; } = 3;

        [Option("Steel Cost (kg)", "How much Steel is required.", "Pilot's Outfit", Format = "F0")]
        [Limit(100, 1000)]
        public int PilotSteelCost { get; set; } = 400;

        [Option("Fiber Cost", "How much fiber (Basic Fabric or Feather Fabric) is required.", "Hauler's Outfit", Format = "F0")]
        [Limit(1, 8)]
        public int HaulerFiberCost { get; set; } = 6;
    }
}
