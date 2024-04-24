using TheaCard.Core.Heroes;
using TheaCard.Infrastructure.ViewModel;

namespace TheaCard.Core.Card
{
    public interface ICardViewModel : IViewModel<IHeroModel, ICardFightView>
    {

    }
}