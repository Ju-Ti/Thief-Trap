using ECS.Game.Systems.GameCycle;

namespace Runtime.Signals
{
    public struct SignalLevelState
    {
        public ELevelState State;
        
        public SignalLevelState(ELevelState state)
        {
            State = state;
        }
    }
}