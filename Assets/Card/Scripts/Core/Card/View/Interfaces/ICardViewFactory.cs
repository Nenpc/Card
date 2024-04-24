using UnityEngine;

namespace TheaCard.Core.Card
{
    public interface ICardViewFactory<THeroInfo, TView> where TView : IView
    {
        TView Get(THeroInfo heroInfo, Transform parent);
        
        void Return(TView card);
    }
}