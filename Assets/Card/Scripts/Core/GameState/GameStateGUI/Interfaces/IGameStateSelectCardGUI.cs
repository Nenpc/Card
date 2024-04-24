using System;

namespace TheaCard.Core.GameState
{
    public interface IGameStateSelectCardGUI : IGameStateGUIBase
    {
        event Action OnNextStage;
    }
}