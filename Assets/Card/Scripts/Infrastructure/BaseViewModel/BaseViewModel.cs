using System.Collections.Generic;
using UnityEngine;

namespace TheaCard.Infrastructure.ViewModel
{
    public abstract class BaseViewModel<TEntity, TEntityView> : IViewModel<TEntity, TEntityView>
    {
        public IDictionary<TEntity, TEntityView> ViewsByEntity { get; } = new Dictionary<TEntity, TEntityView>();

        public void AddView(TEntity entity, TEntityView entityView)
        {
            if (!ViewsByEntity.TryAdd(entity, entityView))
                Debug.LogWarning("Card model already has key!");
        }

        public void RemoveView(TEntity entity)
        {
            ViewsByEntity.Remove(entity);
        }

        public TEntityView GetView(TEntity entity)
        {
            ViewsByEntity.TryGetValue(entity, out var entityView);

            if (entityView == null)
            {
                Debug.LogWarning($"Can't find view for {entity}");
                return default;
            }

            return entityView;
        }

        public void Clear()
        {
            ViewsByEntity.Clear();
        }
    }
}