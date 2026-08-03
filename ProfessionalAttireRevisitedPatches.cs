using System;
using System.Collections.Generic;
using HarmonyLib;

namespace ProfessionalAttireRevisited
{
    public class ProfessionalAttireRevisitedPatches
    {

        public const float BasicAttributeIncrease = 2.0f;

        private static void RegisterTypeStrings(Type type)
        {
            string Id = (string)type.GetField("Id").GetValue(null);
            string DisplayName = (string)type.GetField("DisplayName").GetValue(null);
            string GenericName = (string)type.GetField("GenericName").GetValue(null);
            string Description = (string)type.GetField("Description").GetValue(null);
            Strings.Add($"STRINGS.EQUIPMENT.PREFABS.{Id.ToUpperInvariant()}.NAME", DisplayName);
            Strings.Add($"STRINGS.EQUIPMENT.PREFABS.{Id.ToUpperInvariant()}.GENERICNAME", GenericName);
            Strings.Add($"STRINGS.EQUIPMENT.PREFABS.{Id.ToUpperInvariant()}.DESC", Description);
        }

        [HarmonyPatch(typeof(GeneratedEquipment))]
        [HarmonyPatch("LoadGeneratedEquipment")]
        public class GeneratedEquipment_LoadGeneratedEquipment_Patch
        {
            private static void Prefix()
            {
                RegisterTypeStrings(typeof(ArtistAttireConfig));
                RegisterTypeStrings(typeof(BuildingAttireConfig));
                RegisterTypeStrings(typeof(CookAttireConfig));
                RegisterTypeStrings(typeof(DiggingAttireConfig));
                RegisterTypeStrings(typeof(DoctorAttireConfig));
                RegisterTypeStrings(typeof(FarmingAttireConfig));
                RegisterTypeStrings(typeof(RanchingAttireConfig));
                RegisterTypeStrings(typeof(ScientistAttireConfig));
                RegisterTypeStrings(typeof(StrongAttireConfig));
                RegisterTypeStrings(typeof(TinkerAttireConfig));
                RegisterTypeStrings(typeof(PilotAttireConfig));
                RegisterTypeStrings(typeof(HaulerAttireConfig));
            }

            // No Postfix: the game now auto-discovers and registers every IEquipmentConfig
            // implementation found in the mod assembly via reflection on the `types` argument,
            // so manually calling RegisterEquipment here would double-register each config.
        }



        [HarmonyPatch(typeof(ClothingFabricatorConfig))]
        [HarmonyPatch("ConfigureRecipes")]
        public class ClothingFabricatorConfig_ConfigureRecipes_Patch
        {
            // ConfigureRecipes() runs once per DLC content layer, so without this guard
            // every recipe below gets registered twice (two near-duplicate ComplexRecipe
            // entries sharing one category - shows up as a doubled queue count in the UI).
            private static bool alreadyConfigured = false;

            private static void Postfix()
            {
                if (alreadyConfigured)
                {
                    return;
                }
                alreadyConfigured = true;
                ArtistAttireConfig.ConfigureRecipe();
                BuildingAttireConfig.ConfigureRecipe();
                CookAttireConfig.ConfigureRecipe();
                DiggingAttireConfig.ConfigureRecipe();
                DoctorAttireConfig.ConfigureRecipe();
                FarmingAttireConfig.ConfigureRecipe();
                RanchingAttireConfig.ConfigureRecipe();
                ScientistAttireConfig.ConfigureRecipe();
                StrongAttireConfig.ConfigureRecipe();
                TinkerAttireConfig.ConfigureRecipe();
                PilotAttireConfig.ConfigureRecipe();
                HaulerAttireConfig.ConfigureRecipe();
            }
        }

        // Spacecraft.GetPilotNavigationEfficiency only sums SpaceNavigation bonuses from the
        // pilot's mastered skill perks - it never looks at equipped items - so Pilot's Outfit's
        // AttributeModifier is otherwise invisible to actual mission-duration math. This adds
        // it back in directly, using the same +2 scale as the vanilla Rocket Piloting II perk.
        [HarmonyPatch(typeof(Spacecraft), "GetPilotNavigationEfficiency")]
        public class Spacecraft_GetPilotNavigationEfficiency_Patch
        {
            private static void Postfix(Spacecraft __instance, ref float __result)
            {
                UnityEngine.Debug.Log($"[ProfessionalAttireRevisited] GetPilotNavigationEfficiency base result: {__result}");
                if (__instance.launchConditions.GetComponent<CommandModule>().robotPilotControlled)
                {
                    return;
                }
                List<MinionStorage.Info> storedMinionInfo = __instance.launchConditions.GetComponent<MinionStorage>().GetStoredMinionInfo();
                if (storedMinionInfo.Count < 1)
                {
                    return;
                }
                StoredMinionIdentity stored = storedMinionInfo[0].serializedMinion.Get().GetComponent<StoredMinionIdentity>();
                if (stored.equippedItems == null)
                {
                    return;
                }
                Tag pilotAttireTag = PilotAttireConfig.Id.ToTag();
                foreach (Ref<KPrefabID> itemRef in stored.equippedItems)
                {
                    KPrefabID prefabId = itemRef.Get();
                    UnityEngine.Debug.Log($"[ProfessionalAttireRevisited] Pilot has equipped: {(prefabId != null ? prefabId.PrefabTag.Name : "null")}");
                    if (prefabId != null && prefabId.PrefabTag == pilotAttireTag)
                    {
                        __result += PilotAttireConfig.AttributeIncrease;
                        UnityEngine.Debug.Log($"[ProfessionalAttireRevisited] Pilot's Outfit detected, boosted result to: {__result}");
                    }
                }
            }
        }
    }
}
