namespace Runtime.Signals
{
    public struct SignalBeforeAttack
    {
        public bool Active;

        public SignalBeforeAttack(bool active)
        {
            Active = active;
        }
    }
}