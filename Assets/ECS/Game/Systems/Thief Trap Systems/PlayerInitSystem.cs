using ECS.Core.Utils.ReactiveSystem;
using ECS.Core.Utils.ReactiveSystem.Components;
using ECS.Game.Components.Flags;
using ECS.Game.Components.General;
using ECS.Views.General;
using Leopotam.Ecs;
using Runtime.Game.Utils.MonoBehUtils;
using UnityEngine;
using Zenject;

namespace ECS.Game.Systems.Thief_Trap_Systems
{
    public class PlayerInitSystem :  ReactiveSystem<EventAddComponent<EnemyComponent>>
    {
        protected override EcsFilter<EventAddComponent<EnemyComponent>> ReactiveFilter { get; }
        
        private readonly EcsFilter<EnemyComponent, LinkComponent> _player;
        
        [Inject] private ScreenVariables _screenVariables;
        private const string PLAYER_START = "PlayerStart";

        private EcsEntity _playerEntity;
        private EnemyView _enemyView;
        
        protected override bool DeleteEvent => true;
        protected override void Execute(EcsEntity entity)
        {
            foreach (var i in _player)
            {
                _playerEntity = _player.GetEntity(i);
                
                _enemyView = _playerEntity.Get<LinkComponent>().View as EnemyView;
                _enemyView.transform.position = _screenVariables.GetTransformPoint(PLAYER_START).position;
                _enemyView.transform.rotation = _screenVariables.GetTransformPoint(PLAYER_START).rotation;
            }
        }
    }
}