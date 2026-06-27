using Hero.HeroCode.Relics;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Hero.HeroCode.Relics;

public class CriticalHits() : HeroRelic
{
    public override RelicRarity Rarity => RelicRarity.Starter;
    private HashSet<CardModel> _criticalCards = [];

    public Boolean HasCritThisTurn()
    {
        return _criticalCards.Count > 0;
    }

    public void ClearTurnCrits()
    {
        _criticalCards.Clear();
    }

    public override Decimal ModifyDamageMultiplicative(
        Creature? target,
        Decimal amount,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource)
    {
        if (!props.IsPoweredAttack())
            return 1M;

        if (dealer != this.Owner.Creature)
            return 1M;

        if (cardSource == null)
            return 1M;

        if (Random.Shared.Next(8) == 0)
        {
            _criticalCards.Add(cardSource);
            return 1.5M;
        }

        return 1M;
    }
}