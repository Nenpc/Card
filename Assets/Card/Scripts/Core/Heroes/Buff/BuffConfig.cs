using System;
using UnityEngine;

namespace TheaCard.Core.Buff
{
    [Serializable]
    public class BuffConfig
    {
        [SerializeField] private BuffType _buffType;
        [SerializeField] private int _value;

        public BuffType BuffType => _buffType;
        public int Value => _value;
    }
}