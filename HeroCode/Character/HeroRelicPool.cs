using BaseLib.Abstracts;
using Hero.HeroCode.Extensions;
using Godot;

namespace Hero.HeroCode.Character;

public class HeroRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => Hero.Color;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}