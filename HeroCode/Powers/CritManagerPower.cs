using System.Linq;
using HarmonyLib;
using Hero.HeroCode.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

#nullable enable

namespace Hero.HeroCode.Powers;

public class CritManagerPower() : HeroPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    private const int RandomCritDenominator = 8;
    private Decimal _critMultiplier = 1.5M;
    private int _remainingGuaranteedCrits = 1;

    public Decimal CritMultiplier
    {
        get => _critMultiplier;
        set => _critMultiplier = value;
    }

    public int CritsThisTurn { get; private set; }
    public int AttacksThisTurn { get; private set; }
    public bool HasCritThisTurn => CritsThisTurn > 0;
    public int RemainingGuaranteedCrits
    {
        get => _remainingGuaranteedCrits;
        set => _remainingGuaranteedCrits = value;
    }

    public void ResetTurn()
    {
        AttacksThisTurn = 0;
        CritsThisTurn = 0;
        _remainingGuaranteedCrits = 1;
    }

    public void SetCritMultiplier(Decimal multiplier) => _critMultiplier = multiplier;
    public void ModifyCritMultiplier(Decimal delta) => _critMultiplier += delta;
    public void AddGuaranteedCrit(int count = 1) => _remainingGuaranteedCrits += count;

    private bool ConsumeGuaranteedCrit()
    {
        if (_remainingGuaranteedCrits > 0)
        {
            _remainingGuaranteedCrits--;
            return true;
        }
        return false;
    }

    public override Decimal ModifyDamageMultiplicative(
        Creature? target,
        Decimal amount,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource)
    {
        if (!props.IsPoweredAttack() || cardSource == null || cardSource.Owner.Creature != this.Owner)
            return 1M;

        AttacksThisTurn++;

        bool isCrit = ConsumeGuaranteedCrit();

        if (isCrit)
        {
            CritsThisTurn++;
            return _critMultiplier;
        }

        if (Random.Shared.Next(RandomCritDenominator) == 0)
        {
            CritsThisTurn++;
            return _critMultiplier;
        }

        return 1M;
    }
}

[HarmonyPatch(typeof(Hook), nameof(Hook.AfterPlayerTurnStart))]
public static class CritManagerTurnPatch
{
    public static void Postfix(CombatState combatState, PlayerChoiceContext choiceContext, Player player)
    {
        var creature = player.Creature;
        var power = creature.Powers.OfType<CritManagerPower>().FirstOrDefault();
        power?.ResetTurn();
    }
}
