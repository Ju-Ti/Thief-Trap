namespace Runtime.Signals
{
    public struct SignalLifeCountUpdate
    {
        public int Count;

        public SignalLifeCountUpdate(int count)
        {
            Count = count;
        }
    }
}