using TheaCard.Core.Enums;
using TheaCard.Core.Heroes;

namespace TheaCard.Core.Buff
{
    public sealed class IncreasePhysicalDamageLeft : BuffAbstract, IBuff
    {
        public BuffType Buff => BuffType.IncreasePhysicalDamageLeft;
        
        public void Use(FightType fightType, IHeroModel mainHero, IHeroModel leftHero, IHeroModel rightHero)
        {
            if (fightType != FightType.Physical)
                return;
            
            if (leftHero != null && leftHero.Team == mainHero.Team)
                leftHero.UpdateDamage(_value);
        }
    }
}