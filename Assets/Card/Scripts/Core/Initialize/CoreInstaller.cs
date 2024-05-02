using TheaCard.Code.ScreenDealer;
using TheaCard.Core.Buff;
using TheaCard.Core.Currency;
using TheaCard.Core.Enums;
using TheaCard.Core.GameState;
using TheaCard.Core.Progress;
using TheaCard.Infrastructure.GameState;
using TheaCard.Core.FightModel;
using TheaCard.Core.Process;
using TheaCard.Infrastructure.Intermediary;
using TheaCard.Infrastructure.ScreenDealer;
using Zenject;

namespace TheaCard.Core.Initialize
{
    public sealed class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindModel();
            BindGameSateViewController();
            BindGameSate();
            BindCurrency();
            BindProgress();
            BindProcess();
            BindBuff();
            BindAdditionalScreens();
            
            Container.Bind<IInitializable>().To<GameInitializer>().AsSingle().NonLazy();
            Container.Bind<IInitializable>().To<AdditionalInitializer>().AsSingle().NonLazy();
        }

        private void BindGameSate()
        {
            Container.Bind<IIntermediaryState<GameStates>>().To<GameStateMenu>().AsSingle().NonLazy();
            Container.Bind<IIntermediaryState<GameStates>>().To<GameStateSelectCard>().AsSingle().NonLazy();
            Container.Bind<IIntermediaryState<GameStates>>().To<GameStateEnemyCard>().AsSingle().NonLazy();
            Container.Bind<IIntermediaryState<GameStates>>().To<GameStateSelectHand>().AsSingle().NonLazy();
            Container.Bind<IIntermediaryState<GameStates>>().To<GameStateFight>().AsSingle().NonLazy();

            Container.Bind<IIntermediary<GameStates>>().To<GameStateIntermediary>().AsSingle().NonLazy();
        }

        private void BindModel()
        {
            Container.Bind<IFightModel>().To<FightModel.FightModel>().AsSingle().NonLazy();
        }

        private void BindGameSateViewController()
        {
            Container.Bind<IGameStateFightViewController>().To<GameStateFightViewController>().AsSingle().NonLazy();
        }

        private void BindAdditionalScreens()
        {
            Container.Bind<IAdditionalScreen<AdditionalScreens>>().To<CurrencyPresenter>().AsCached().NonLazy();
            Container.Bind<IAdditionalScreen<AdditionalScreens>>().To<ProgressPresenter>().AsCached().NonLazy();
            Container.Bind<IAdditionalScreenDealer>().To<AdditionalScreenDealer>().AsSingle().NonLazy();
        }

        private void BindCurrency()
        {
            Container.Bind<ICurrencyModel>().To<CurrencyModel>().AsSingle().NonLazy();
            Container.Bind<ICurrencyPresenter>().To<CurrencyPresenter>().AsCached().NonLazy();
            Container.Bind<ICurrencyGUIController>().To<CurrencyGUIController>().AsSingle().NonLazy();
        }
        
        private void BindProgress()
        {
            Container.Bind<IProgressModel>().To<ProgressModel>().AsSingle().NonLazy();
            Container.Bind<IProgressPresenter>().To<ProgressPresenter>().AsCached().NonLazy();
            Container.Bind<IProgressGUIController>().To<ProgressGUIController>().AsSingle().NonLazy();
        }

        private void BindProcess()
        {
            Container.Bind<IIntermediaryState<ProcessStates>>().To<EnemySelectProcess>().AsSingle().NonLazy();
            Container.Bind<IIntermediaryState<ProcessStates>>().To<PlayerSelectProcess>().AsSingle().NonLazy();
            Container.Bind<IIntermediaryState<ProcessStates>>().To<FightProcess>().AsSingle().NonLazy();
            
            Container.Bind<IIntermediary<ProcessStates>>().To<ProcessIntermediary>().AsSingle().NonLazy();
        }

        private void BindBuff()
        {
            Container.Bind<IBuff>().To<IncreasePhysicalDamageLeft>().AsSingle().NonLazy();
            Container.Bind<IBuff>().To<NoneBuff>().AsSingle().NonLazy();
            Container.Bind<IBuff>().To<IncreaseVerbalDamageAll>().AsSingle().NonLazy();
            Container.Bind<IBuff>().To<StartPhysicalDamage>().AsSingle().NonLazy();
            
            Container.Bind<IBuffContainer>().To<BuffContainer>().AsSingle().NonLazy();
        }
    }
}