using TheaCard.Core.Card;
using UnityEngine;
using Zenject;
using TheaCard.Core.Currency;
using TheaCard.Core.Heroes;

namespace TheaCard.Core.Initialize
{
    [CreateAssetMenu(menuName = "Card/Config Installer")]
    public sealed class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
    {
        [SerializeField] private CurrencyConfig _currencyConfig;
        [SerializeField] private HeroesConfig _heroeseConfig;
        [SerializeField] private CardViewConfig _cardViewConfig;

        private void InstallCurrency()
        {
            Container.Bind<CurrencyConfig>().FromInstance(_currencyConfig);
            Container.Bind<IHeroesConfig>().FromInstance(_heroeseConfig);

            Container.Bind<ICardViewConfig>().FromInstance(_cardViewConfig);
        }

        public override void InstallBindings()
        {
            InstallCurrency();
        }
    }
}