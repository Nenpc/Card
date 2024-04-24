using UnityEngine.UI;

namespace TheaCard.Core.GameState
{
    public interface IGameStateMenuGUI : IGameStateGUIBase
    {
        public Button StartGame { get; }
        public Button Quite { get; }
    }
}