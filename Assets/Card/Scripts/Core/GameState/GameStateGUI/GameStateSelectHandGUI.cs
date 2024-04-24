using System;
using TheaCard.Core.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace TheaCard.Core.GameState
{
    public sealed class GameStateSelectHandGUI : GameStateGUIAbstract, IGameStateSelectHandGUI
    {
        public event Action OnNextStage;
        public event Action OnShuffleCards;
        
        [SerializeField] private Button _nextState;
        [SerializeField] private Button _shuffleCards;

        public override GameStates States => GameStates.SelectHand;

        private void Awake()
        {
            _nextState.onClick.AddListener(() => OnNextStage?.Invoke());
            _shuffleCards.onClick.AddListener(() => OnShuffleCards?.Invoke());
        }

        private void OnDestroy()
        {
            _nextState.onClick.RemoveAllListeners();
            _shuffleCards.onClick.RemoveAllListeners();
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