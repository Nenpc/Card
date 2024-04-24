using TheaCard.Core.Heroes;
using UnityEngine;

namespace TheaCard.Core.Card
{
    public sealed class CardSelectViewFactory : CardPoolAbstract<ICardSelectView>,
        ICardViewFactory<IHeroConfig, ICardSelectView>
    {
        [SerializeField] private CardSelectView _cardFightViewPrefab;

        protected override MonoBehaviour ViewPrefab => _cardFightViewPrefab;

        public ICardSelectView Get(IHeroConfig heroConfig, Transform parent)
        {
            var view = Get(parent);
            view.Init(heroConfig);
            view.View.SetActive(true);
            return view;
        }
    }
}