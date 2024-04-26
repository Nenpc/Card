namespace TheaCard.Core.Buff
{
    public abstract class BuffAbstract
    {
        protected int _value = 1;
        
        public void Init(int value)
        {
            _value = value;
        }
    }
}