using System;
using TheaCard.Core.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace TheaCard.Core.GameState
{
    public sealed class GameStateEnemyCardGUI : GameStateGUIAbstract, IGameStateEnemyCardGUI
    {
        public event Action OnNextStage;
        public event Action<FightType> OnFightTypeChanged;
        
        [SerializeField] private Button _nextState;

        [SerializeField] private Toggle _physicalToggle;
        [SerializeField] private Toggle _magicToggle;
        [SerializeField] private Toggle _verbalToggle;
        
        public override GameStates States => GameStates.ViewEnemyCard;

        private void Awake()
        {
            _nextState.onClick.AddListener(() => OnNextStage?.Invoke());
            _physicalToggle.onValueChanged.AddListener((x) => SelectFightType(x,FightType.Physical));
            _magicToggle.onValueChanged.AddListener((x) => SelectFightType(x,FightType.Magic));
            _verbalToggle.onValueChanged.AddListener((x) => SelectFightType(x,FightType.Verbal));
           
        }
        
        private void OnDestroy()
        {
            _nextState.onClick.RemoveAllListeners();
            _physicalToggle.onValueChanged.RemoveAllListeners();
            _magicToggle.onValueChanged.RemoveAllListeners();
            _verbalToggle.onValueChanged.RemoveAllListeners();
        }

        private void SelectFightType(bool select, FightType fightType)
        {
            if (select)
                OnFightTypeChanged?.Invoke(fightType);
        }
        

        public void Show()
        {
            gameObject.SetActive(true);
            OnFightTypeChanged?.Invoke(FightType.Physical);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}