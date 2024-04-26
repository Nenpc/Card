using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TheaCard.Core.Buff
{
    public sealed class BuffContainer : IBuffContainer
    {
        [SerializeField] private Dictionary<BuffType, IBuff> _buffs = new Dictionary<BuffType, IBuff>();
        
        public IReadOnlyDictionary<BuffType, IBuff> Buffs => _buffs;

        public BuffContainer(IEnumerable<IBuff> buffs, IBuffsConfig buffsConfig)
        {
            foreach (var buff in buffs)
            {
                var buffConfig = buffsConfig.BuffConfigs.FirstOrDefault(x => x.BuffType == buff.Buff);
                if (buffConfig != default)
                    buff.Init(buffConfig.Value);
                
                _buffs.Add(buff.Buff, buff);
            }
        }
        
        public IBuff GetBuff(BuffType buffType)
        {
            if (!_buffs.ContainsKey(buffType))
            {
                Debug.LogError($"No buff type {buffType}!");
                return null;
            }

            return _buffs[buffType];
        }
    }
}