using System;
using TheaCard.Core.Enums;
using UnityEngine;

namespace TheaCard.Core.Heroes
{
    [Serializable]
    public sealed class DamageInfo
    {
        [SerializeField] private FightType _fightType;
        [SerializeField] private int _damage;
        [SerializeField] private int _defense;
        [SerializeField] private int _health;

        public FightType FightType => _fightType;
        public int Damage => _damage;
        public int Defense => _defense;
        public int Health => _health;
    }
}