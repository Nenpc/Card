namespace TheaCard.Core.Buff
{
    public interface IBuffContainer
    {
        IBuff GetBuff(BuffType buffType);
    }
}