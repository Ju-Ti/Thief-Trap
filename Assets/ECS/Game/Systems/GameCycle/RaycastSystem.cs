using DG.Tweening;
using ECS.Core.Utils.SystemInterfaces;
using ECS.Game.Components.Flags;
using ECS.Game.Components.GameCycle;
using ECS.Game.Components.General;
using ECS.Game.Components.Thief_Trap_Components;
using ECS.Game.Systems.General;
using ECS.Game.Systems.Thief_Trap_Systems;
using ECS.Utils.Extensions;
using ECS.Views.GameCycle;
using ECS.Views.General;
using Leopotam.Ecs;
using Runtime.Game.Utils.MonoBehUtils;
using Runtime.Services.VibrationService;
using Runtime.Signals;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace ECS.Game.Systems.GameCycle
{
    public class RaycastSystem : IEcsUpdateSystem
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private ScreenVariables _screenVariables;
        [Inject] private IVibrationService _vibrationService;

#pragma warning disable
        private readonly EcsWorld _world;
        private readonly EcsFilter<ActiveStatePlayer> _activePlayer;
        private readonly EcsFilter<CameraComponent, LinkComponent> _cameraF;
        private readonly EcsFilter<EventInputDownComponent> _eventDown;
        private readonly EcsFilter<EventInputHoldAndDragComponent> _eventHoldAndDrag;
        private readonly EcsFilter<EventInputUpComponent> _eventUp;
        private readonly EcsFilter<PickupedComponent, LinkComponent> _pickuped;
        private readonly EcsFilter<EnemyComponent, LinkComponent> _enemy;
#pragma warning restore 649

        private CameraView _cameraView;
        private Camera _camera;

        private int _releasableMask = LayerMask.GetMask("Releasable");
        private int _draggableMask = LayerMask.GetMask("Draggable");
        private int _surfaceMask = LayerMask.GetMask("Surface");
        private int _defaultMask = LayerMask.GetMask("Default");
        
        private int _hexagonCellLayerMask = LayerMask.GetMask("HexagonCell");
        private int _hexagonLayerMask = LayerMask.GetMask("Hexagon");
        private int _playerLayerMask = LayerMask.GetMask("Player");
        

        private RaycastHit _hitToHexagonCell;
        private bool _hitToPlayerOrHexagon;
        private Collider[] _colliders = new Collider[8];
        private Vector3 _collisionTolerance = new Vector3(0.01f, 0.01f, 0.01f);
        private Vector2 _targetPositionRaycastOffset = new Vector2(0, 100f);

        private LinkableView _view;
        private EcsEntity _pickupedEntity;

        private SignalJoystickUpdate _signalJoystickUpdate =
            new SignalJoystickUpdate(false, Vector2.zero, Vector2.zero);

        public void Run()
        {
            foreach (var i in _cameraF)
            {
                _cameraView = _cameraF.Get2(i).Get<CameraView>();
                _camera = _cameraView.GetCamera();
                _targetPositionRaycastOffset = Vector2.zero;
            }

            foreach (var i in _eventDown)
            {
                foreach (var j in _activePlayer)
                {
                    //Debug.Log("PlayerRay");
                    if (!TryCameraRaycast(_eventDown.Get1(i).Down, ref _hexagonCellLayerMask))
                        break;

                    if (IsHitObstacles(_eventDown.Get1(i).Down))
                        break;
                
                    _view = _hitToHexagonCell.collider.GetComponent<CellHexagonView>();
                    var position = _view.Transform.position;

                    var _enemyView = _enemy.GetEntity(0).Get<LinkComponent>().View as EnemyView;
                    var _cellView = _view.Entity.Get<LinkComponent>().View as CellHexagonView;
                    if (_cellView.cellHexagonType != CellHexagonComponent.CellHexagonType.Base && _cellView.cellStatus != CellHexagonComponent.CellStatus.IsBlocked && Vector3.Distance(_cellView.Transform.position, _enemyView.Transform.position) > 0.2f)
                    {
                        _view.Entity.Get<PoliceHexComponent>();
                        _view.Entity.Get<HexagonSelectedToSetComponent>();
                    }
                    //through component blocked 
                    
                }
            }

            foreach (var i in _eventHoldAndDrag)
            {
                Debug.DrawRay(_camera.transform.position, _eventHoldAndDrag.Get1(i).Down, Color.red);
                if (!TryCameraRaycast(_eventHoldAndDrag.Get1(i).Drag, ref _surfaceMask))
                    break;

                foreach (var j in _pickuped)
                {
                    _pickuped.Get2(j).View.Transform.position = _hitToHexagonCell.point;
                }
            }

            foreach (var i in _eventUp)
            {
                foreach (var j in _pickuped)
                {
                    _pickupedEntity = _pickuped.GetEntity(j);
                    if (TryCameraRaycast(_eventUp.Get1(i).Up, ref _releasableMask))
                    {
                        _pickupedEntity.Get<EventWordReleaseComponent>().EmptyWordEntity = _hitToHexagonCell.collider.GetComponent<EmptyWordView>().Entity;
                    }
                    else
                        _pickuped.Get2(j).View.Transform.position = _pickupedEntity.Get<DefaultPosition>().Value;

                    _pickupedEntity.Del<PickupedComponent>();
                }
            }
        }
        

        private bool IsHitObstacles(Vector2 point) =>
            Physics.Raycast(_camera.ScreenPointToRay(point), 
                100f, LayerMask.GetMask("Player", "Hexagon"));

        private bool TryCameraRaycast(Vector2 point, ref int layerMask) =>
            Physics.Raycast(_camera.ScreenPointToRay(point), out _hitToHexagonCell, 100f, layerMask);

        private void ClearColliders()
        {
            for (int i = 0; i < _colliders.Length; i++)
                if (_colliders[0] != null)
                {
                    _colliders[0] = null;
                }
        }
    }

    public struct PickupedComponent : IEcsIgnoreInFilter
    {
    }
}