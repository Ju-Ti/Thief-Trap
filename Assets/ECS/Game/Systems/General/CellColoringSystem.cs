using ECS.Core.Utils.ReactiveSystem;
using ECS.Core.Utils.ReactiveSystem.Components;
using ECS.Game.Components.Flags;
using ECS.Game.Components.General;
using ECS.Game.Components.Thief_Trap_Components;
using ECS.Game.Systems.GameCycle;
using ECS.Game.Systems.Thief_Trap_Systems;
using ECS.Utils.Extensions;
using ECS.Views.General;
using JetBrains.Annotations;
using Leopotam.Ecs;
using Runtime.DataBase.CellType;
using UnityEngine;
using UnityTemplateProjects.Runtime.DataBase.CellType;
using Zenject;
using Random = System.Random;

namespace ECS.Game.Systems.General
{
    public class CellColoringSystem : ReactiveSystem<EventAddComponent<CellHexagonComponent>>
    {
        [Inject] private IHexagonCellTypeBase _hexagonCellTypeBase;
        [CanBeNull] private EcsFilter<CellHexagonComponent> _cell;
        private EcsFilter<EnemyComponent, LinkComponent> _enemy;
        private ILinkable _enemyView;
        private EcsWorld _world;
        private EcsEntity _objectHexagonEntity;
        private Vector3 _objectHexagonPosition;
        private Quaternion _objectHexagonRotation;
        protected override EcsFilter<EventAddComponent<CellHexagonComponent>> ReactiveFilter { get; }
        protected override void Execute(EcsEntity entity)
        {
            
            var cellView = (CellHexagonView) entity.Get<LinkComponent>().View;
            Material cellMaterial = _hexagonCellTypeBase.Get(cellView.cellHexagonType.ToString());
            if (cellView.cellHexagonType != CellHexagonComponent.CellHexagonType.Base)
            {
                cellView.gameObject.GetComponent<Renderer>().material = cellMaterial;  
            }
            foreach (var e in _enemy)
            {
                _enemyView = _enemy.GetEntity(e).Get<LinkComponent>().View as EnemyView;
            }

            
            if (cellView.cellStatus == CellHexagonComponent.CellStatus.IsBlocked)
            {
                //_objectHexagonEntity = GetHexagon(cellView.cellHexagonType);
                //_objectHexagonPosition = cellView.Transform.position;
                //_objectHexagonRotation = cellView.Transform.rotation;
            //
                //_objectHexagonEntity.Get<EventSetPositionComponent>().Value = _objectHexagonPosition + new Vector3(0 , 0,0);
                //_objectHexagonEntity.Get<EventSetRotationComponent>().Value = _objectHexagonRotation;
                
                _world.CreatePoliceCar(_enemyView, cellView.Transform.position + new Vector3(0 , 5, 0),PoliceSpawnSystem.RandNum());
            }
            
        }


        private EcsEntity GetHexagon(CellHexagonComponent.CellHexagonType cellHexagonType)
        {
            var hexagon = _world.CreateHexagon(cellHexagonType);
            return hexagon;
        }
    }
}