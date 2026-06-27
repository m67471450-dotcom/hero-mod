using HarmonyLib;
using Hero.HeroCode.Relics;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Unlocks;

namespace Hero.patches;


[HarmonyPatch(typeof(SharedRelicPool), "GenerateAllRelics")]
public class RelicPoolPatch
{
    static void Postfix(ref IEnumerable<RelicModel> __result)
    {
        var list = __result.ToList();

        // Add your custom relic
        var customRelic = ModelDb.Relic<CriticalHits>();
        list.Add(customRelic);

        // Reassign back to __result
        __result = list;

        Log.Info($"Added ExampleRelic to SharedRelicPool! Total: {list.Count}");
    }
}