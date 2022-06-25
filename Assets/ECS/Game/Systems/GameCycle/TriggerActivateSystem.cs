using System.Diagnostics.CodeAnalysis;
using ECS.Core.Utils.SystemInterfaces;
using ECS.Game.Components.Flags;
using ECS.Game.Components.General;
using ECS.Views.General;
using Leopotam.Ecs;

namespace ECS.Game.Systems.GameCycle
{
    public class TriggerActivateSystem : IEcsUpdateSystem
    {
        private readonly EcsFilter<EventTriggerActivateComponent, LinkComponent> _events;
        private readonly EcsFilter<EnemyComponent, LinkComponent> _player;
        private readonly EcsFilter<LevelStateComponent> _levelState;

        private EcsEntity _eventEntity;
        private EnemyView _enemyView;
        private bool cond = false;

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void Run()
        {
            
        }
    }
    
    public struct EventTriggerActivateComponent : IEcsIgnoreInFilter
    {
    }
    public struct ActivatedComponent : IEcsIgnoreInFilter
    {
    }
}