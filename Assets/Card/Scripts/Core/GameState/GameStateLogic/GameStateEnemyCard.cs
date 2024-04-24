using System;
using TheaCard.Core.Card;
using TheaCard.Core.FightModel;
using TheaCard.Core.Heroes;
using TheaCard.Core.Enums;
using TheaCard.Infrastructure.Intermediary;

namespace TheaCard.Core.GameState
{
    public sealed class GameStateEnemyCard : IIntermediaryState<GameStates>, IDisposable
    {
        public event Action<GameStates> OnEndState;
        
        public GameStates State => GameStates.ViewEnemyCard;

        private readonly IGameStateEnemyCardView _enemyCardView;
        private readonly IGameStateEnemyCardGUI _gui;
        private readonly IHeroesConfig _heroesConfig;
        private readonly ICardViewFactory<IHeroConfig, ICardSelectView> _cardViewFactory;
        private readonly IFightModel _fightModel;

        private bool _isInitialize = false;
        
        public GameStateEnemyCard(IGameStateEnemyCardView enemyCardView, 
            IGameStateEnemyCardGUI gui,
            IHeroesConfig heroesConfig,
            ICardViewFactory<IHeroConfig, ICardSelectView> cardViewFactory,
            IFightModel fightModel)
        {
            _enemyCardView = enemyCardView;
            
            _gui = gui;
            _gui.OnNextStage += NextStage;
            _gui.OnFightTypeChanged += SelectFightType;

            _heroesConfig = heroesConfig;
            _fightModel = fightModel;
            _cardViewFactory = cardViewFactory;
        }
        
        private void NextStage()
        {
            OnEndState?.Invoke(State);
        }
        
        private void SelectFightType(FightType fightType)
        {
            _fightModel.FightType = fightType;
        }

        public void Start()
        {
            if (!_isInitialize)
            {
                CreateEnemies();
                _enemyCardView.Init(_cardViewFactory, _fightModel.Enemy.HeroesConfig, _fightModel.Player.HeroesConfig);
                _isInitialize = true;
            }

            _gui.Show();
            _enemyCardView.Show();
        }

        private void CreateEnemies()
        {
            var enemyAmount = UnityEngine.Random.Range(4, 6);
            for (int i = 0; i < enemyAmount; i++)
            {
                var enemyPosition = UnityEngine.Random.Range(0, _heroesConfig.Enemies.Count);
                var heroConfig = _heroesConfig.Enemies[enemyPosition];
                _fightModel.Enemy.AddHeroConfig(heroConfig);
            }
        }

        public void End()
        {
            _gui.Hide();
            _enemyCardView.Hide();
        }

        public void Dispose()
        {
            _gui.OnNextStage -= NextStage;
        }
    }
}