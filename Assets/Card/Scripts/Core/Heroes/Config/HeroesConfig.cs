using System.Collections.Generic;
using UnityEngine;

namespace TheaCard.Core.Heroes
{
    [CreateAssetMenu(menuName = "TheaCard/Hero/HeroesConfig")]
    public sealed class HeroesConfig : ScriptableObject, IHeroesConfig
    {
        [SerializeField] private List<HeroConfig> _heroConfigs = new List<HeroConfig>();
        [SerializeField] private List<HeroConfig> _enemiesConfigs = new List<HeroConfig>();

        public IReadOnlyList<HeroConfig> Heroes => _heroConfigs;
        public IReadOnlyList<HeroConfig> Enemies => _enemiesConfigs;
    }
}