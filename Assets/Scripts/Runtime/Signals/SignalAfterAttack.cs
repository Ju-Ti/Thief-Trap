namespace Runtime.Signals
{
    public struct SignalAfterAttack
    {
        public bool Active;

        public SignalAfterAttack(bool active)
        {
            Active = active;
        }
    }
}