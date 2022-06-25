using ECS.Core.Utils.SystemInterfaces;
using ECS.Game.Components.Flags;
using ECS.Game.Components.General;
using ECS.Views.General;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Game.Systems.General
{
    public class CameraFollow : IEcsUpdateSystem
    {
        private readonly EcsFilter<CameraComponent, LinkComponent> _cameraF;
        private readonly EcsFilter<EnemyComponent, LinkComponent> _player;
        private EnemyView _enemyView;

        public void Run()
        {
            foreach (var i in _cameraF)
            {
                var cameraView = _cameraF.Get2(i).Get<CameraView>();
                foreach (var j in _player)
                {
                    _enemyView = _player.Get2(j).View as EnemyView;
                    cameraView.transform.position = _enemyView.transform.position + new Vector3(0, 6 ,-4);
                }
            }
        }
    }
}