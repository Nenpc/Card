using TheaCard.Core.Enums;
using TheaCard.Core.Heroes;

namespace TheaCard.Core.Buff
{
    public sealed class IncreaseVerbalDamageAll : BuffAbstract, IBuff
    {
        public BuffType Buff => BuffType.IncreaseVerbalDamageAll;
        
        public void Use(FightType fightType, IHeroModel mainHero, IHeroModel leftHero, IHeroModel rightHero)
        {
            if (fightType != FightType.Verbal)
                return;
            
            if (leftHero != null && leftHero.Team == mainHero.Team)
                leftHero.UpdateDamage(_value);
            
            if (rightHero != null && leftHero.Team == mainHero.Team)
                rightHero.UpdateDamage(_value);
        }
    }
}