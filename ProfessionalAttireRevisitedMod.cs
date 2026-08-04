using HarmonyLib;
using KMod;
using PeterHan.PLib.AVC;
using PeterHan.PLib.Core;
using PeterHan.PLib.Database;
using PeterHan.PLib.Options;

namespace ProfessionalAttireRevisited
{
    public class Mod : UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary(false);
            new PLocalization().RegisterFromCallingClass();
            new POptions().RegisterOptions(this, typeof(ProfessionalAttireRevisitedSettings));
            new PVersionCheck().Register(this, new SteamVersionChecker());
            harmony.PatchAll();
        }
    }
}
