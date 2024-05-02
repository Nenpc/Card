using TheaCard.Core.Enums;

namespace TheaCard.Core.Buff
{
    public interface IBuffContainer
    {
        IBuff GetBuff(BuffType buffType);
    }
}