using System.Collections.Generic;
using Klei.AI;
using PeterHan.PLib.Options;
using STRINGS;
using UnityEngine;

namespace ProfessionalAttireRevisited
{
    public class ScientistAttireConfig : IEquipmentConfig
    {
        public const string Id = "ScientistAttire";
        public static string DisplayName => ProfessionalAttireRevisitedStrings.SCIENTIST.NAME;
        public static string GenericName => ProfessionalAttireRevisitedStrings.GENERIC_NAME_CLOTHING;
        public static string RecipeDescription => string.Format(ProfessionalAttireRevisitedStrings.SCIENTIST.RECIPE_DESC, DisplayName);
        public static string Description => ProfessionalAttireRevisitedStrings.SCIENTIST.DESC;

        public static int DecorModifier = ClothingWearer.ClothingInfo.BASIC_CLOTHING.decorMod;
        public static float ConductivityModifier = ClothingWearer.ClothingInfo.BASIC_CLOTHING.conductivityMod;
        public static float HomeostasisEfficiencyModifier = ClothingWearer.ClothingInfo.BASIC_CLOTHING.homeostasisEfficiencyMultiplier;
        public const float AttributeIncrease = ProfessionalAttireRevisitedPatches.BasicAttributeIncrease;

        public static readonly ClothingWearer.ClothingInfo NEW_CLOTHING =
            new ClothingWearer.ClothingInfo(DisplayName, DecorModifier, ConductivityModifier, HomeostasisEfficiencyModifier);

        public static readonly ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[]
        {
            new ComplexRecipe.RecipeElement(Id.ToTag(), 1f)
        };

        public static void ConfigureRecipe()
        {
            var settings = POptions.ReadSettings<ProfessionalAttireRevisitedSettings>() ?? new ProfessionalAttireRevisitedSettings();
            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(GameTags.Fabrics, settings.ResearcherFiberCost),
                new ComplexRecipe.RecipeElement(DatabankHelper.TAG, settings.ResearcherDatabankCost)
            };
            new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("ClothingFabricator",
                ingredients, results), ingredients, results)
            {
                time = TUNING.EQUIPMENT.VESTS.FUNKY_VEST_FABTIME,
                description = RecipeDescription,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { "ClothingFabricator" },
                sortOrder = 1
            };
        }

        public EquipmentDef CreateEquipmentDef()
        {
            ClothingWearer.ClothingInfo clothingInfo = NEW_CLOTHING;
            List<AttributeModifier> attributeModifiers = new List<AttributeModifier>();
            attributeModifiers.Add(new AttributeModifier(Db.Get().Attributes.Learning.Id, AttributeIncrease, DisplayName, false, false, true));
            EquipmentDef equipment = EquipmentTemplates.CreateEquipmentDef(
                Id: Id,
                Slot: TUNING.EQUIPMENT.CLOTHING.SLOT,
                OutputElement: SimHashes.Carbon,
                Mass: TUNING.EQUIPMENT.VESTS.FUNKY_VEST_MASS,
                Anim: "shirt_decor01_kanim",
                SnapOn: TUNING.EQUIPMENT.VESTS.SNAPON0,
                BuildOverride: "body_shirt_decor01_kanim",
                BuildOverridePriority: 4,
                AttributeModifiers: attributeModifiers,
                SnapOn1: TUNING.EQUIPMENT.VESTS.SNAPON1,
                IsBody: true,
                CollisionShape: EntityTemplates.CollisionShape.RECTANGLE,
                width: 0.75f,
                height: 0.4f,
                additional_tags: new Tag[0],
                RecipeTechUnlock: null);
            string thermalConductivityDescriptor = string.Format("{0}: {1}",
                    DUPLICANTS.ATTRIBUTES.THERMALCONDUCTIVITYBARRIER.NAME,
                    GameUtil.GetFormattedDistance(clothingInfo.conductivityMod));
            equipment.additionalDescriptors.Add(new Descriptor(
                thermalConductivityDescriptor, thermalConductivityDescriptor,
                Descriptor.DescriptorType.Effect, false));
            string decorDescriptor = string.Format("{0}: {1}",
                DUPLICANTS.ATTRIBUTES.DECOR.NAME,
                clothingInfo.decorMod);
            equipment.additionalDescriptors.Add(
                new Descriptor(decorDescriptor, decorDescriptor,
                Descriptor.DescriptorType.Effect, false));
            equipment.OnEquipCallBack = eq => ClothingWearer.ClothingInfo.OnEquipVest(eq, clothingInfo);
            equipment.OnUnequipCallBack = eq => ClothingWearer.ClothingInfo.OnUnequipVest(eq);
            equipment.RecipeDescription = RecipeDescription;
            return equipment;
        }

        public void DoPostConfigure(GameObject go)
        {
            go.GetComponent<KPrefabID>().AddTag(GameTags.Clothes, false);
            Equippable equippable = go.AddOrGet<Equippable>();
            equippable.SetQuality(QualityLevel.Poor);
            go.GetComponent<KBatchedAnimController>().sceneLayer = Grid.SceneLayer.BuildingBack;
            go.GetComponent<KPrefabID>().AddTag(GameTags.PedestalDisplayable, false);
        }

        public string[] GetDlcIds() => null;
    }

}
