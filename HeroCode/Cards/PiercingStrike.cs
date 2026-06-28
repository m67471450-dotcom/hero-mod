using Hero.HeroCode.Cards;
using Hero.HeroCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

#nullable enable

namespace Hero.HeroCode.Cards;

public class PiercingStrike() : HeroCard(
    2,
    CardType.Attack,
    CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return new DynamicVar[]
            {
                new DamageVar(8M, ValueProp.Move)
            };
        }
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        await PowerCmd.Apply<CritManagerPower>(choiceContext, Owner.Creature, 1M, Owner.Creature, this);
        Owner.Creature.Powers.OfType<CritManagerPower>().FirstOrDefault()?.AddGuaranteedCrit(1);

        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }
}
