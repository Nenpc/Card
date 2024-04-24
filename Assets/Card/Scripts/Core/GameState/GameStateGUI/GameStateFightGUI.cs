using TheaCard.Core.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace TheaCard.Core.GameState
{
    public sealed class GameStateFightGUI : GameStateGUIAbstract, IGameStateFightGUI
    {
        [SerializeField] private Button _endMove;
        [SerializeField] private Button _giveUp;
        
        public override GameStates States => GameStates.Fight;
        
        public Button EndMove => _endMove;
        public Button GiveUp => _giveUp;
        
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