using System;
using TheaCard.Core.Enums;

namespace TheaCard.Core.GameState
{
    public interface IGameStateFightType
    {
        event Action OnNextStage;
        event Action<FightType> OnSelectType;
    }
}