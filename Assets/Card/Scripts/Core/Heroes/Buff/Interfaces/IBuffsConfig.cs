using System.Collections.Generic;

namespace TheaCard.Core.Buff
{
    public interface IBuffsConfig
    {
        IReadOnlyList<BuffConfig> BuffConfigs { get; }
    }
}