using System;
using TheaCard.Core.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace TheaCard.Core.GameState
{
    public sealed class GameStateSelectCardGUI : GameStateGUIAbstract, IGameStateSelectCardGUI
    {
        public event Action OnNextStage;

        [SerializeField] private Button _nextState;

        public override GameStates States => GameStates.SelectCard;

        private void Awake()
        {
            _nextState.onClick.AddListener(() => OnNextStage?.Invoke());
        }

        private void OnDestroy()
        {
            _nextState.onClick.RemoveAllListeners();
        }

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