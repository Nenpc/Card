using System.Collections.Generic;
using UnityEngine;

namespace TheaCard.Core.Buff
{
    public sealed class BuffsConfig
    {
        [SerializeField] private Dictionary<BuffType,IBuff> _buffs = new Dictionary<BuffType,IBuff>();

        public BuffsConfig()
        {
            _buffs.Add(BuffType.StartPhysicalDamage, new StartPhysicalDamage());
            _buffs.Add(BuffType.IncreaseVerbalDamageAll, new IncreaseVerbalDamageAll());
            _buffs.Add(BuffType.IncreasePhysicalDamageLeft, new IncreasePhysicalDamageLeft());
        }

        public IReadOnlyDictionary<BuffType, IBuff> Buffs => _buffs;
    }
}