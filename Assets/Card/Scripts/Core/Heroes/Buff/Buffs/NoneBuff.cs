using TheaCard.Core.Enums;
using TheaCard.Core.Heroes;

namespace TheaCard.Core.Buff
{
    public sealed class NoneBuff : BuffAbstract, IBuff
    {
        public BuffType Buff => BuffType.None;
        
        public void Use(FightType fightType, IHeroModel mainHero, IHeroModel leftHero, IHeroModel rightHero)
        {

        }
    }
}