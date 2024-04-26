using TheaCard.Core.Enums;
using TheaCard.Core.Heroes;

namespace TheaCard.Core.Buff
{
    public sealed class StartPhysicalDamage : BuffAbstract, IBuff
    {
        public BuffType Buff => BuffType.StartPhysicalDamage;
        
        public void Use(FightType fightType, IHeroModel mainHero, IHeroModel leftHero, IHeroModel rightHero)
        {
            if (fightType != FightType.Physical)
                return;
            
            if (leftHero != null && leftHero.Team != mainHero.Team)
                leftHero.TakeDamage(_value);
        }
    }
}