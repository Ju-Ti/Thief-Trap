using ECS.Core.Utils.ReactiveSystem;
using ECS.Core.Utils.SystemInterfaces;
using ECS.Game.Components.Thief_Trap_Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Game.Systems.GameCycle
{
    public class BlockedCellsSystem : ReactiveSystem<BlockedCellsComponent>
    {
        protected override EcsFilter<BlockedCellsComponent> ReactiveFilter { get; }

        protected override void Execute(EcsEntity entity)
        {
        
        }
    }
}