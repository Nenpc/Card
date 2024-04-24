using System;

namespace TheaCard.Core.GameState
{
    public interface IGameStateSelectHandGUI : IGameStateGUIBase
    {
        public event Action OnNextStage;
        public event Action OnShuffleCards;
    }
}