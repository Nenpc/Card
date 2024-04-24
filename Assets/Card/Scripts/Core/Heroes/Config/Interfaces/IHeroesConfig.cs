using System.Collections.Generic;

namespace TheaCard.Core.Heroes
{
    public interface IHeroesConfig
    {
        IReadOnlyList<HeroConfig> Heroes { get; }
        IReadOnlyList<HeroConfig> Enemies { get; }
    }
}