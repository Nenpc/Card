namespace TheaCard.Core.Buff
{
    public interface IBuff
    {
        BuffType Buff { get; }

        void Use();
    }
}