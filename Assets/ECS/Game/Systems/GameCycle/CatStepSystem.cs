using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using ECS.Core.Utils.ReactiveSystem;
using ECS.Core.Utils.ReactiveSystem.Components;
using ECS.Game.Components.Flags;
using ECS.Game.Components.General;
using ECS.Game.Components.Thief_Trap_Components;
using ECS.Utils.Extensions;
using ECS.Views.General;
using Leopotam.Ecs;
using ECS.Game.Systems.GameCycle;

using UniRx;
using UnityEngine;

namespace ECS.Game.Systems.GameCycle
{
    public class CatStepSystem : ReactiveSystem<EventAddComponent<ActiveStateCat>>

    {
        private EcsFilter <PlayerManageComponent> _player;
        private EcsFilter<EnemyComponent, LinkComponent> _enemy;

        private EcsEntity _playerEntity;
        private EcsEntity _enemyEntity;

        private Vector3 moveToEnemy;
        protected override EcsFilter<EventAddComponent<ActiveStateCat>> ReactiveFilter { get; }
        protected override void Execute(EcsEntity entity)
        {
            //var list = PathCalc.FinalPath();
            //foreach (var i in list)
            //{
            //    i.Transform.DOMoveY(1f, 1f);
            //}
            //foreach (var i in _player)
            //{
            //    _playerEntity = _player.GetEntity(i);
            //}
            //
            //foreach (var i in _enemy)
            //{
            //    _enemyEntity = _enemy.GetEntity(i);
            //}
            //
            //var enemyEntity = _enemy.GetEntity(0);
            //var _enemyView = enemyEntity.Get<LinkComponent>().View as EnemyView;
            ////_enemyView.Transform.DOMove(moveToEnemy, 0.1f).SetEase(Ease.Linear).SetRelative(true).OnComplete(() =>
            ////{
            ////    _playerEntity.Get<QueueComponent>().queueStatus = QueueComponent.QueueStatus.Wait;
            ////    _playerEntity.GetAndFire<QueueComponent>().queueStatus = QueueComponent.QueueStatus.Go;
            ////    _playerEntity.Del<ActiveStateCat>();
            ////});
            //
            //_playerEntity.Get<QueueComponent>().queueStatus = QueueComponent.QueueStatus.Wait;
            //_playerEntity.GetAndFire<QueueComponent>().queueStatus = QueueComponent.QueueStatus.Go;
            //_playerEntity.Del<ActiveStateCat>();
            
            //var enemyEntity = _enemy.GetEntity(0);
            //var enemyView = enemyEntity.Get<LinkComponent>().View as EnemyView;
            //enemyView.Transform.position = FinalPath();

        }
        
    }
}