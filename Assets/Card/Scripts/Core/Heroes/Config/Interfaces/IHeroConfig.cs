using System.Collections.Generic;
using TheaCard.Core.Buff;
using TheaCard.Core.Enums;
using UnityEngine;

namespace TheaCard.Core.Heroes
{
    public interface IHeroConfig
    {
        public string Name { get; }
        public Sprite Icon { get; }
        public int Price { get; }
        public IReadOnlyList<DamageInfo> DamageInfos { get; }
        public BuffType Buff { get; }
    }
}