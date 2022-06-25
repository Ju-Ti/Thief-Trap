using DG.Tweening;
using ECS.Core.Utils.SystemInterfaces;
using ECS.Game.Components.General;
using ECS.Views.General;
using Leopotam.Ecs;
using Unity.Mathematics;
using UnityEngine;

namespace ECS.Game.Systems.GameCycle
{
    public class TransformTranslateSystem : IEcsUpdateSystem
    {
        private readonly EcsFilter<LinkComponent, EventSetPositionComponent> _viewsPos;
        private readonly EcsFilter<LinkComponent, EventSetRotationComponent> _viewsRot;
        private readonly EcsFilter<LinkComponent, EventSetLookAtComponent> _viewsLookAt;

        private ObjectHexagonView _hexagonView;
        public void Run()
        {
            foreach (var i in _viewsLookAt)
            {
                var view = _viewsLookAt.Get2(i).View;
                _viewsLookAt.Get1(i).View.Transform.DOMoveY(0.3f, 1f);
                _viewsLookAt.Get1(i).View.Transform.rotation =
                    Quaternion.LookRotation(view.Transform.position + new Vector3(0 , 0.1f ,0) - _viewsLookAt.Get1(i).View.Transform.position);
            }
            foreach (var i in _viewsPos)
            {
                _viewsPos.Get1(i).View.Transform.position = _viewsPos.Get2(i).Value;
                _viewsPos.GetEntity(i).Del<EventSetPositionComponent>();
            }
            foreach (var i in _viewsRot)
            {
                _viewsRot.Get1(i).View.Transform.rotation = _viewsRot.Get2(i).Value;
                _viewsRot.GetEntity(i).Del<EventSetRotationComponent>();
            }
        }
    }

    public struct EventSetPositionComponent
    {
        public Vector3 Value;
    }
    
    public struct EventSetRotationComponent
    {
        public Quaternion Value;
    }

    public struct EventSetLookAtComponent
    {
        public ILinkable View;
        
    }
}