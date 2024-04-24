using System.Collections.Generic;
using TheaCard.Core.Heroes;
using UnityEngine;

namespace TheaCard.Core.Card
{
    public abstract class CardPoolAbstract<TView> : MonoBehaviour where TView : IView 
    {
        [SerializeField] private RectTransform _parent;

        protected abstract MonoBehaviour ViewPrefab { get; }

        private List<TView> _pool = new List<TView>();
        
        protected TView Get(Transform parent)
        {
            if (_pool.Count > 0)
            {
                var card = _pool[0];
                _pool.RemoveAt(0);
                card.View.transform.SetParent(parent);
                return card;
            }
            else
            {
                var card = Instantiate(ViewPrefab, parent).GetComponent<TView>();
                return card;
            }
        }

        public void Return(TView card)
        {
            card.View.gameObject.SetActive(false);
            card.View.transform.SetParent(_parent);
            _pool.Add(card);
        }
    }
}