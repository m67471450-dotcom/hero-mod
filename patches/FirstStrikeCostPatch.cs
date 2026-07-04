using Hero.HeroCode.Cards;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using HarmonyLib;

namespace Hero.patches;

[HarmonyPatch(typeof(Hook), nameof(Hook.ModifyEnergyCostInCombat))]
public static class FirstStrikeCostPatch
{
    public static void Postfix(CardModel card, ref decimal __result)
    {
        if (card is FirstStrike && card.IsUpgraded)
        {
            __result = 1M;
        }
    }
}
