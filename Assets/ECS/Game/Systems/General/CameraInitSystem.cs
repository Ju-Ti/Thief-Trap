using System.Diagnostics.CodeAnalysis;
using ECS.Core.Utils.ReactiveSystem;
using ECS.Core.Utils.ReactiveSystem.Components;
using ECS.Game.Components.Flags;
using ECS.Game.Components.General;
using ECS.Views.General;
using Leopotam.Ecs;
using Runtime.Game.Utils.MonoBehUtils;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace ECS.Game.Systems.General
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public class CameraInitSystem : ReactiveSystem<EventAddComponent<CameraComponent>>
    {
        [Inject] private ScreenVariables _screenVariables;
        private const string CAMERA_PRE_START = "CameraPreStart";
        
        private readonly EcsFilter<CameraComponent, LinkComponent> _cameraF;
        private readonly EcsFilter<EnemyComponent, LinkComponent> _player;
        protected override EcsFilter<EventAddComponent<CameraComponent>> ReactiveFilter { get; }
        protected override bool DeleteEvent => true;

        private EnemyView _enemyView;
    
        protected override void Execute(EcsEntity entity)
        {
            foreach (var i in _cameraF)
            {
                var cameraView = _cameraF.Get2(i).Get<CameraView>();
                
                cameraView.Transform.position = _screenVariables.GetTransformPoint(CAMERA_PRE_START).position;
                cameraView.Transform.rotation = _screenVariables.GetTransformPoint(CAMERA_PRE_START).rotation;

                var cameraData = cameraView.GetCamera().GetUniversalAdditionalCameraData();
                foreach (var uIcamera in GameObject.FindGameObjectsWithTag("UiCamera"))
                    if (uIcamera.transform.parent == null)
                    {
                        cameraData.cameraStack.Add(uIcamera.GetComponent<Camera>());
                        break;
                    }
                
                foreach (var j in _player)
                {
                    _enemyView = _player.Get2(j).View as EnemyView;
                    cameraView.transform.position = _enemyView.transform.position + new Vector3(0, 6 ,-4);
                    //cameraView.transform.rotation = cameraView.transform.rotation + Quaternion.Angle(0 , 10 , 0);
                }
            }

           
        }
    }
}