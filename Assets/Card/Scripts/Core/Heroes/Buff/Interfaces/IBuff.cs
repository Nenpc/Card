using TheaCard.Core.Enums;
using TheaCard.Core.Heroes;

namespace TheaCard.Core.Buff
{
    public interface IBuff
    {
        BuffType Buff { get; }

        void Init(int value);

        void Use(FightType fightType, IHeroModel mainHero, IHeroModel leftHero, IHeroModel rightHero);
    }
}