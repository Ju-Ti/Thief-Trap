using System.Diagnostics.CodeAnalysis;
using ECS.Core.Utils.ReactiveSystem;
using ECS.Game.Components.Events;
using ECS.Game.Components.Flags;
using ECS.Game.Components.General;
using Leopotam.Ecs;
using Runtime.DataBase.Game;
using Runtime.Game.Ui.Windows.GameOver;
using Runtime.Game.Ui.Windows.LevelComplete;
using Runtime.Game.Utils.MonoBehUtils;
using Runtime.Services.FxSpawnService;
using Runtime.Services.VibrationService;
using SimpleUi.Signals;
using UnityEngine;
using Zenject;

#pragma warning disable 649

namespace ECS.Game.Systems.General
{
    [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public class LevelEndSystem : ReactiveSystem<ChangeStageComponent>
    {
        [Inject] private readonly ScreenVariables _screenVariables;
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly IVibrationService _vibrationService;
        [Inject] private readonly IFxSpawnService _fxSpawnService;

        private readonly EcsWorld _world;
        private readonly EcsFilter<CameraComponent, LinkComponent> _cameraF;
        private readonly EcsFilter<EnemyComponent, LinkComponent> _player;
        protected override EcsFilter<ChangeStageComponent> ReactiveFilter { get; }
        protected override bool DeleteEvent => false;
        public bool disable;

        protected override void Execute(EcsEntity entity)
        {
            if (disable)
                return;
            switch (entity.Get<ChangeStageComponent>().Value)
            {
                case EGameStage.Lose:
                    HandleLevelLose();
                    disable = true;
                    break;
                case EGameStage.Complete:
                    HandleLevelComplete();
                    disable = true;
                    break;
            }
        }

        [SuppressMessage("ReSharper", "Unity.InefficientPropertyAccess")]
        private void HandleLevelComplete()
        {
            _fxSpawnService.SpawnParticle("ConfettiShower");
            _signalBus.OpenWindow<LevelCompleteWindow>();
        }

        private void HandleLevelLose()
        {
            Transform camTrans = null;
            foreach (var i in _cameraF)
                camTrans = _cameraF.Get2(i).View.Transform;
            foreach (var i in _player)
            {
                // var deathTrans = _player.Get2(i).Get<PlayerView>().CameraDeath;
                // camTrans.DOMove(deathTrans.position, 1.2f);
                // camTrans.DORotateQuaternion(deathTrans.rotation, 1.2f);
            }
            _signalBus.OpenWindow<GameOverWindow>();
        }
    }
}