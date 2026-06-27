using BaseLib.Abstracts;
using BaseLib.Utils;
using Hero.HeroCode.Character;

namespace Hero.HeroCode.Potions;

[Pool(typeof(HeroPotionPool))]
public abstract class HeroPotion : CustomPotionModel;