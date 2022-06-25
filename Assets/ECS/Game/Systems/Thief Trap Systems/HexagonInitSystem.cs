using DG.Tweening;
using ECS.Core.Utils.ReactiveSystem;
using ECS.Core.Utils.SystemInterfaces;
using ECS.Game.Components.Flags;
using ECS.Game.Components.General;
using ECS.Game.Components.Thief_Trap_Components;
using ECS.Game.Systems.GameCycle;
using ECS.Game.Systems.General;
using ECS.Utils.Extensions;
using ECS.Views.General;
using Leopotam.Ecs;
using ModestTree;
using Runtime.DataBase.CellType;
using Runtime.Services.DelayService;
using UnityEngine;
using UnityTemplateProjects.Runtime.DataBase.CellType;
using Zenject;

namespace ECS.Game.Systems.Thief_Trap_Systems
{
    public class HexagonInitSystem : ReactiveSystem<HexagonSelectedToSetComponent>
    {
        [Inject] private IDelayService _delayService;

        private EcsFilter<ActiveStatePlayer> _activePlayer;
        private EcsFilter<LinkComponent, HexagonObjectComponent> _hexgons;
        private EcsFilter<EnemyComponent, LinkComponent> _enemy;
        private readonly EcsWorld _world;
        
        private CellHexagonView _cellHexagonView;
        private CellHexagonComponent.CellStatus _cellStatus;
        private EcsEntity _objectHexagonEntity;
        private ObjectHexagonView _objectHexagonView;
        private Vector3 _objectHexagonPosition;
        private Quaternion _objectHexagonRotation;

        protected override EcsFilter<HexagonSelectedToSetComponent> ReactiveFilter { get; }
        protected override void Execute(EcsEntity entity)
        {
            _cellHexagonView = entity.Get<LinkComponent>().View as CellHexagonView;
            _cellHexagonView.cellStatus = CellHexagonComponent.CellStatus.IsBlocked;
            _cellStatus = _cellHexagonView.cellStatus;
            //_cellStatus = entity.Get<QueueComponent>().queueStatus;
            if(_cellHexagonView.cellStatus == CellHexagonComponent.CellStatus.IsBlocked)
              
            
            //_objectHexagonEntity = GetHexagon(_cellHexagonView.cellHexagonType);
            //_objectHexagonPosition = _cellHexagonView.Transform.position;
            //_objectHexagonRotation = _cellHexagonView.Transform.rotation;
            //
            //_objectHexagonEntity.Get<EventSetPositionComponent>().Value = _objectHexagonPosition;
            //_objectHexagonEntity.Get<EventSetRotationComponent>().Value = _objectHexagonRotation;
            
            
            foreach (var i in _activePlayer)
            {
                //Debug.Log("PlayerToCat");
                var playerEntity = _activePlayer.GetEntity(i);
                playerEntity.Get<QueueComponent>().queueStatus = QueueComponent.QueueStatus.Wait;
                
                var enemyEntity = _enemy.GetEntity(0);
                enemyEntity.GetAndFire<QueueComponent>().queueStatus = QueueComponent.QueueStatus.Go;
                playerEntity.Del<ActiveStatePlayer>();
            }
            
            
        }
        
        //private EcsEntity GetHexagon(CellHexagonComponent.CellHexagonType cellHexagonType)
        //{
        //    var hexagon = _world.CreateHexagon(cellHexagonType);
        //    return hexagon;
        //}
    }
    
    public struct HexagonSelectedToSetComponent
    {
        public Vector2 TouchPos;
    }
    
    //удаляет ивент-компонент
}