using TheaCard.Core.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace TheaCard.Core.GameState
{
    public sealed class GameStateMenuGUI : GameStateGUIAbstract, IGameStateMenuGUI
    {
        [SerializeField] private Button _startGame;
        [SerializeField] private Button _quite;
        
        public override GameStates States => GameStates.Menu;

        public Button StartGame => _startGame;
        public Button Quite => _quite;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}