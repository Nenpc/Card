using System;
using System.Collections.Generic;
using TheaCard.Core.Card;
using TheaCard.Core.Heroes;
using TheaCard.Core.Enums;

namespace TheaCard.Core.GameState
{
    public sealed class GameStateFightViewController : IGameStateFightViewController, IDisposable
    {
        public event Action<IHeroModel> OnHeroClick;
        
        private readonly IGameStateFightView _view;
        private readonly ICardViewFactory<IHeroModel, ICardFightView> _cardViewFactory;

        private Dictionary<GameTeam, Dictionary<IHeroModel, ICardFightView>> _cardViews = 
            new Dictionary<GameTeam, Dictionary<IHeroModel, ICardFightView>>();

        public GameStateFightViewController(IGameStateFightView view, 
            ICardViewFactory<IHeroModel, ICardFightView> cardViewFactory)
        {
            _view = view;
            _cardViewFactory = cardViewFactory;
        }
        
        public void Init(IReadOnlyList<IHeroModel> playerHeroes, IReadOnlyList<IHeroModel> enemyHeroes)
        {
            _cardViews.Add(GameTeam.Enemy, new Dictionary<IHeroModel, ICardFightView>());
            foreach (var enemyModel in enemyHeroes)
            {
                var parent = enemyModel.Hand == Hands.Fight
                    ? _view.EnemyFightHand
                    : _view.EnemySupportHand;
                var enemyView = _cardViewFactory.Get(enemyModel, parent.transform);
                enemyView.RotateCard(false);
                _cardViews[GameTeam.Enemy].Add(enemyModel, enemyView);
            }
            
            _cardViews.Add(GameTeam.Player, new Dictionary<IHeroModel, ICardFightView>());
            foreach (var playerModel in playerHeroes)
            {
                var parent = playerModel.Hand == Hands.Fight
                    ? _view.PlayerFightHand
                    : _view.PlayerSupportHand;
                var playerView = _cardViewFactory.Get(playerModel, parent.transform);
                playerView.OnCardClick += SelectCard;
                _cardViews[GameTeam.Player].Add(playerModel, playerView);
            }
        }
        
        public void Dispose()
        {
            if (_cardViews.ContainsKey(GameTeam.Player))
            {
                foreach (var card in _cardViews[GameTeam.Player])
                {
                    card.Value.OnCardClick -= SelectCard;
                }
            }
        }

        private void SelectCard(IHeroModel heroModel)
        {
            OnHeroClick?.Invoke(heroModel);
        }

        public void MoveToMainField(IHeroModel hero)
        {
            _cardViews[hero.Team][hero].View.transform.SetParent(_view.MainField.transform);
        }

        public void AttackHero(IHeroModel hero, IHeroModel goal)
        {
            _cardViews[hero.Team][hero].UpdateView();;
        }

        public void DestroyHero(IHeroModel hero)
        {
            var cardView = _cardViews[hero.Team][hero].View.GetComponent<ICardFightView>();
            _cardViews[hero.Team].Remove(hero);
            
            _cardViewFactory.Return(cardView);
        }

        public void Show()
        {
            _view.Show();
        }

        public void Hide()
        {
            _view.Hide();
        }
    }
}