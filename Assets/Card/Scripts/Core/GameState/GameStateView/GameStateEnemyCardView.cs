using System.Collections.Generic;
using TheaCard.Core.Card;
using TheaCard.Core.Heroes;
using TheaCard.Core.Enums;
using UnityEngine;

namespace TheaCard.Core.GameState
{
    public sealed class GameStateEnemyCardView : GameStateViewAbstract, IGameStateEnemyCardView
    {
        [SerializeField] private RectTransform _enemyField;
        [SerializeField] private RectTransform _playerField;

        public override GameStates States => GameStates.ViewEnemyCard;

        private List<ICardSelectView> _enemyViews = new List<ICardSelectView>();
        private List<ICardSelectView> _playerViews = new List<ICardSelectView>();
        private ICardViewFactory<IHeroConfig, ICardSelectView> _cardViewFactory;

        public void Init(ICardViewFactory<IHeroConfig, ICardSelectView> cardViewFactory, IReadOnlyList<IHeroConfig> enemyConfigs, IReadOnlyList<IHeroConfig> playerConfigs)
        {
            _cardViewFactory = cardViewFactory;
            foreach (var enemyConfig in enemyConfigs)
            {
                var enemyView = _cardViewFactory.Get(enemyConfig, _enemyField);
                _enemyViews.Add(enemyView);
            }
            
            foreach (var playerConfig in playerConfigs)
            {
                var playerView = _cardViewFactory.Get(playerConfig, _playerField);
                _playerViews.Add(playerView);
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            
            foreach (var enemyView in _enemyViews)
                _cardViewFactory.Return(enemyView);
            _enemyViews.Clear();
            
            foreach (var playerView in _playerViews)
                _cardViewFactory.Return(playerView);
            _playerViews.Clear();
        }
    }
}