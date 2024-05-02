using System;
using System.Collections.Generic;
using TheaCard.Core.Heroes;
using TheaCard.Core.Enums;

namespace TheaCard.Core.FightModel
{
    public interface IFightModel
    {        
        public event Action OnEndFight;
        
        IHeroesFightModel Player { get; }
        IHeroesFightModel Enemy { get; }
        IReadOnlyList<IHeroModel> HeroesBoard { get; }
        
        FightType FightType { get; set; }

        void MoveToBoard(IHeroModel heroModel);
        void RemoveFromBoard(IHeroModel heroModel);
        void EndFight();
    }
}