using Hero.HeroCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

#nullable enable

namespace Hero.HeroCode.Cards;

public class FirstStrike() : HeroCard(
    2,
    CardType.Power,
    CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return new DynamicVar[]
            {
                new PowerVar<FirstStrikePower>(1M)
            };
        }
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        if (!Owner.Creature.Powers.OfType<CritManagerPower>().Any())
        {
            await PowerCmd.Apply<CritManagerPower>(choiceContext, Owner.Creature, 1M, Owner.Creature, this);
        }

        await PowerCmd.Apply<FirstStrikePower>(choiceContext, Owner.Creature, DynamicVars["FirstStrikePower"].BaseValue, Owner.Creature, this);

        Owner.Creature.Powers.OfType<CritManagerPower>().FirstOrDefault()?.AddGuaranteedCrit(1);
    }

    protected override void OnUpgrade()
    {
    }
}
