using TheaCard.Core.Heroes;
using TheaCard.Core.Enums;

namespace TheaCard.Core.FightModel
{
    public interface IFightModel
    {
        IHeroesFightModel Player { get; }
        IHeroesFightModel Enemy { get; }
        FightType FightType { get; set; }
        int Round { get; }

        void MoveToBoard(IHeroModel heroModel);
        void NextRound();
    }
}