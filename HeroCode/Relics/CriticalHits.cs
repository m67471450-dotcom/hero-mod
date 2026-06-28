using Hero.HeroCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using HarmonyLib;

namespace Hero.HeroCode.Relics;

public class CriticalHits() : HeroRelic
{
    public override RelicRarity Rarity => RelicRarity.Starter;
}

[HarmonyPatch(typeof(Hook), nameof(Hook.AfterPlayerTurnStart))]
public static class CriticalHitsTurnStartPatch
{
    public static void Postfix(CombatState combatState, PlayerChoiceContext choiceContext, Player player)
    {
        var creature = player.Creature;
        if (!creature.Powers.OfType<CritManagerPower>().Any())
        {
            PowerCmd.Apply<CritManagerPower>(choiceContext, creature, 1M, creature, null);
        }
    }
}
