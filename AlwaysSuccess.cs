using System;
using TaiwuModdingLib.Core.Plugin;
using HarmonyLib;
using GameData.Domains;
using GameData.Domains.Merchant;
using GameData.Domains.Taiwu;

namespace AlwaysCuccess
{
    [PluginConfig("百分百突破", "ReachingFoul", "v0.0.1")]
    public class AlwaysCuccess : TaiwuRemakePlugin
    {
        private Harmony harmony;
        public override void Dispose()
        {
            if (harmony != null)
                return;
            harmony.UnpatchSelf();
        }

        public override void Initialize() => this.harmony = Harmony.CreateAndPatchAll(typeof(AlwaysCuccess), (string)null);

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TaiwuDomain), "UpdateBreakPlateTotalStepAndSuccessRate")]
        public static void Taiwu_AlwaysCuccess_Update_PostPatch(short skillTemplateId, TaiwuDomain __instance)
        {
            SkillBreakPlate plate = __instance.GetElement_SkillBreakPlateDict(skillTemplateId);
            plate.BaseSuccessRate = byte.MaxValue;
            plate.BonusGridSuccessRatePercentageFix = sbyte.MaxValue;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TaiwuDomain), "InitSkillBreakPlate")]
        public static void Taiwu_Init_PostPatch(SkillBreakPlate plate)
        {
            SkillBreakPlateGrid[][] grid = plate.Grids;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    plate.Grids[i][j].State = 0;
                }
            }
        }
    }
}
