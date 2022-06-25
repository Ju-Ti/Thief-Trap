using DG.Tweening;
using ECS.Core.Utils.ReactiveSystem;
using ECS.Game.Components.Flags;
using ECS.Game.Components.General;
using ECS.Game.Components.Thief_Trap_Components;
using ECS.Game.Systems.GameCycle;
using ECS.Utils.Extensions;
using ECS.Views.General;
using Leopotam.Ecs;
using UnityEngine;
using Random = System.Random;

namespace ECS.Game.Systems.Thief_Trap_Systems
{
    public class PoliceSpawnSystem: ReactiveSystem<PoliceHexComponent>
    {
        protected override EcsFilter<PoliceHexComponent> ReactiveFilter { get; }
        private EcsFilter<PoliceHexComponent, LinkComponent> _hex;
        private EcsFilter<PoliceComponent, LinkComponent> _police;
        private EcsFilter<EnemyComponent, LinkComponent> _enemy;
        private EcsFilter<CellHexagonComponent, LinkComponent> _cell;
        private EcsWorld _world;

        private ILinkable _policeView;
        protected override void Execute(EcsEntity entity)
        {
            foreach (var i in _hex)
            {
                foreach (var j in _enemy)
                {
                    var enemyView = _enemy.GetEntity(j).Get<LinkComponent>().View;
                    var hexEntity = _hex.GetEntity(i);
                    var hexView = hexEntity.Get<LinkComponent>().View;

                    _world.CreatePoliceCar(enemyView, hexView.Transform.position + new Vector3(0 , 5, 0),RandNum());
                }
            }

            foreach (var i in _police)
            {
                var _policeEntity = _police.GetEntity(i);
                _policeEntity.Del<EventSetLookAtComponent>();
            }
        }

        public static int RandNum()
        {
            Random random = new Random();
            int num = random.Next(1, 3);
            return num;
        }
    }
}