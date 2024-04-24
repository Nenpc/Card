using TheaCard.Infrastructure.GameState;
using TheaCard.Core.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace TheaCard.Core.GameState
{
    public sealed class GameStateFightView : GameStateViewAbstract, IGameStateFightView
    {
        [SerializeField] private HorizontalLayoutGroup _playerFightHand;
        [SerializeField] private HorizontalLayoutGroup _playerSupportHand;
        [SerializeField] private HorizontalLayoutGroup _enemyFightHand;
        [SerializeField] private HorizontalLayoutGroup _enemySupportHand;

        [SerializeField] private HorizontalLayoutGroup _mainField;
        
        public override GameStates States => GameStates.Fight;

        public HorizontalLayoutGroup PlayerFightHand => _playerFightHand;
        public HorizontalLayoutGroup PlayerSupportHand => _playerSupportHand;
        public HorizontalLayoutGroup EnemyFightHand => _enemyFightHand;
        public HorizontalLayoutGroup EnemySupportHand => _enemySupportHand;
        public HorizontalLayoutGroup MainField => _mainField;

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