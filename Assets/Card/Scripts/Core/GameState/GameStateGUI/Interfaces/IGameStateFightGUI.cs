using UnityEngine.UI;

namespace TheaCard.Core.GameState
{
    public interface IGameStateFightGUI : IGameStateGUIBase
    {
        public Button EndMove { get; }
        public Button GiveUp { get; }
    }
}