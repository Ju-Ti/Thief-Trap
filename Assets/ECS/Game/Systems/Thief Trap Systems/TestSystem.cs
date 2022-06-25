using ECS.Core.Utils.ReactiveSystem;
using ECS.Core.Utils.ReactiveSystem.Components;
using ECS.Core.Utils.SystemInterfaces;
using ECS.Game.Components.Flags;
using ECS.Game.Components.General;
using ECS.Game.Components.Thief_Trap_Components;
using ECS.Views.General;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Game.Systems.Thief_Trap_Systems
{
    public class TestSystem : ReactiveSystem<EnemyComponent>

    {
        protected override EcsFilter<EnemyComponent> ReactiveFilter { get; }

        private ObjectHexagonView _objectHexagonView;

    

    protected override void Execute(EcsEntity entity)
    {
        _objectHexagonView = entity.Get<LinkComponent>().Get<ObjectHexagonView>();

        if (_objectHexagonView != null)
        {
            Debug.Log("Hexogon not null");
        }
    }
    }
}