using System.Collections.Generic;
using TheaCard.Core.Buff;
using TheaCard.Core.Enums;
using UnityEngine;

namespace TheaCard.Core.Heroes
{
    [CreateAssetMenu(menuName = "TheaCard/Hero/HeroConfig")]
    public sealed class HeroConfig : ScriptableObject, IHeroConfig
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _price;
        [SerializeField] private List<DamageInfo> _damageInfos;
        [SerializeField] private BuffType _buff;

        public string Name => _name;
        public Sprite Icon => _icon;
        public int Price => _price;
        public IReadOnlyList<DamageInfo> DamageInfos => _damageInfos; 
        public BuffType Buff => _buff;
    }
}