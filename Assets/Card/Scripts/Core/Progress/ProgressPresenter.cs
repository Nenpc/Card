using System;
using System.Collections.Generic;
using TheaCard.Core.Enums;
using TheaCard.Core.FightModel;
using TheaCard.Core.Heroes;
using TheaCard.Infrastructure.ScreenDealer;

namespace TheaCard.Core.Progress
{
    public sealed class ProgressPresenter : IProgressPresenter, IDisposable, IAdditionalScreen<AdditionalScreens>
    {
        private readonly IProgressModel _progressModel;
        private readonly IProgressGUIController _progressGUIController;
        private readonly IFightModel _fightModel;

        private readonly List<IHeroModel> _playerHeroes = new List<IHeroModel>();
        private readonly List<IHeroModel> _enemyHeroes = new List<IHeroModel>();

        public AdditionalScreens ScreenType => AdditionalScreens.Progress;

        public ProgressPresenter(IProgressModel progressModel,
            IProgressGUIController progressGUIController,
            IFightModel fightModel)
        {
            _progressModel = progressModel;
            _progressGUIController = progressGUIController;
            
            _fightModel = fightModel;
        }

        public void Init()
        {
            _fightModel.OnEndFight += FightResult;
            
            _fightModel.Player.OnHeroModelAdd += AddHero;
            _fightModel.Player.OnHeroModelRemove += RemoveHero;
            _fightModel.Player.OnAllHeroModelRemove += _playerHeroes.Clear;
            
            _fightModel.Enemy.OnHeroModelAdd += AddHero;
            _fightModel.Enemy.OnHeroModelRemove += RemoveHero;
            _fightModel.Enemy.OnAllHeroModelRemove += _enemyHeroes.Clear;
            
            _progressGUIController.UpdateProgress(_progressModel.Progress);
        }

        private void AddHero(IHeroModel heroModel)
        {
            if (heroModel.Team == GameTeam.Player)
            {
                _playerHeroes.Add(heroModel);
            }
            else
            {
                _enemyHeroes.Add(heroModel);
            }
            
            heroModel.OnTakeDamage += UpdateTakeDamage;
            heroModel.OnDeath += UpdateHeroDeath;
        }

        private void RemoveHero(IHeroModel heroModel)
        {
            if (heroModel.Team == GameTeam.Player)
            {
                _playerHeroes.Remove(heroModel);
            }
            else
            {
                _enemyHeroes.Remove(heroModel);
            }
            
            heroModel.OnTakeDamage -= UpdateTakeDamage;
            heroModel.OnDeath -= UpdateHeroDeath;
        }
        
        private void UpdateTakeDamage(IHeroModel heroModel, int damageAmount)
        {
            if (heroModel.Team == GameTeam.Player)
            {
                _progressModel.AddProgress(ProgressTypes.DamageReceived, damageAmount);
            }
            else
            {
                _progressModel.AddProgress(ProgressTypes.DamageDone, damageAmount);
            }
            
            _progressGUIController.UpdateProgress(_progressModel.Progress);
        }
        
        private void UpdateHeroDeath(IHeroModel heroModel)
        {
            if (heroModel.Team == GameTeam.Player)
            {
                _progressModel.AddProgress(ProgressTypes.LostCards);
            }
            else
            {
                _progressModel.AddProgress(ProgressTypes.CardsDestroyed);
            }
            
            _progressGUIController.UpdateProgress(_progressModel.Progress);
        }

        private void FightResult()
        {
            _progressModel.AddProgress(ProgressTypes.GamesPlayed);
            _progressGUIController.UpdateProgress(_progressModel.Progress);
        }
        
        public void Dispose()
        {
            foreach (var heroModel in _playerHeroes)
            {
                heroModel.OnTakeDamage -= UpdateTakeDamage;
                heroModel.OnDeath -= UpdateHeroDeath;
            }
            _playerHeroes.Clear();
            
            foreach (var heroModel in _enemyHeroes)
            {
                heroModel.OnTakeDamage -= UpdateTakeDamage;
                heroModel.OnDeath -= UpdateHeroDeath;
            }
            _enemyHeroes.Clear();
            
            _fightModel.Player.OnHeroModelAdd -= AddHero;
            _fightModel.Player.OnHeroModelRemove -= RemoveHero;
            _fightModel.Player.OnAllHeroModelRemove -= _playerHeroes.Clear;
            
            _fightModel.Enemy.OnHeroModelAdd -= AddHero;
            _fightModel.Enemy.OnHeroModelRemove -= RemoveHero;
            _fightModel.Enemy.OnAllHeroModelRemove -= _enemyHeroes.Clear;

            _fightModel.OnEndFight -= FightResult;
        }

        public void Show()
        {
            _progressGUIController.ShowPanel();
        }

        public void Hide()
        {
            _progressGUIController.HidePopup();
            _progressGUIController.HidePanel(); 
        }
    }
}