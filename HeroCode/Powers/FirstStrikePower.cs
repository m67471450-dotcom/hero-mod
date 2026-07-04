using Hero.HeroCode.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace Hero.HeroCode.Powers;

public class FirstStrikePower() : HeroPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
}
