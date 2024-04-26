using System.Collections.Generic;
using UnityEngine;

namespace TheaCard.Core.Buff
{
    [CreateAssetMenu(menuName = "TheaCard/Config/BuffsConfig")]
    public sealed class BuffsConfig : ScriptableObject, IBuffsConfig
    {
        [SerializeField] private List<BuffConfig> _buffConfigs = new List<BuffConfig>();

        public IReadOnlyList<BuffConfig> BuffConfigs => _buffConfigs;

        private void OnValidate()
        {
            for (int i = 0; i < _buffConfigs.Count - 1; i++)
            {
                for (int j = i + 1; j < _buffConfigs.Count; j++)
                {
                    if (_buffConfigs[i].BuffType == _buffConfigs[j].BuffType)
                        Debug.LogError($"Type {_buffConfigs[i].BuffType} exist!");
                }
            }
        }
    }
}