using Leopotam.Ecs;

namespace ECS.Game.Components.Thief_Trap_Components
{
    public struct CellHexagonComponent : IEcsIgnoreInFilter
    {
        public CellHexagonType cellHexagonType;
        
        public enum CellHexagonType
        {
            Green,
            Purple,
            Base
        }

        public CellStatus cellStatus;

        public enum CellStatus
        {
            IsClear,
            IsBlocked
        }
    }
}