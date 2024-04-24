using TheaCard.Core.Heroes;
using UnityEngine;

namespace TheaCard.Core.Card
{
    public sealed class CardFightViewFactory : CardPoolAbstract<ICardFightView>, ICardViewFactory<IHeroModel, ICardFightView>
    {
        [SerializeField] private CardFightView _cardFightViewPrefab;

        protected override MonoBehaviour ViewPrefab => _cardFightViewPrefab;

        public ICardFightView Get(IHeroModel heroModel, Transform parent)
        {
            var view = Get(parent);
            view.Init(heroModel);
            view.View.SetActive(true);
            return view;
        }
    }
}