namespace TheaCard.Core.Card
{
    public interface ICardViewConfig
    {
        public ICardFightView FightView { get; }
        public ICardSelectView SelectView { get; }
    }
}