using UnityEngine;

namespace TheaCard.Core.Card
{
    [CreateAssetMenu(menuName = "Card/Config/CardViewConfig")]
    public sealed class CardViewConfig : ScriptableObject, ICardViewConfig
    {
        [SerializeField] private CardFightView _cardFightView;
        [SerializeField] private CardSelectView _cardSelectView;

        public ICardFightView FightView => _cardFightView;
        public ICardSelectView SelectView => _cardSelectView;
    }
}