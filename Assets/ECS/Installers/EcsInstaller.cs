using ECS.Game.Systems.GameCycle;
using ECS.Game.Systems.General;
using ECS.Game.Systems.Linked;
using ECS.Game.Systems.Thief_Trap_Systems;
using Leopotam.Ecs;
using Runtime.Game.Utils.MonoBehUtils;
using UnityEngine;
using Zenject;

namespace ECS.Installers
{
    public class EcsInstaller : MonoInstaller
    {
        [SerializeField] private ScreenVariables _screenVariables;
        public override void InstallBindings()
        {
            Container.Bind<ScreenVariables>().FromInstance(_screenVariables).AsSingle();
            Container.BindInterfacesAndSelfTo<EcsWorld>().AsSingle().NonLazy();
            BindSystems();
            Container.BindInterfacesTo<EcsMainBootstrap>().AsSingle();
        }

        private void BindSystems()
        {
            Container.BindInterfacesAndSelfTo<GameInitializeSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<InstantiateSystem>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<CameraInitSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraResizeSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelStartSystem>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<TransformTranslateSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputSystem>().AsSingle();
            
            // Container.BindInterfacesAndSelfTo<MatrixCellInitSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraFollow>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInitSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellColoringSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<HexagonInitSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<BlockedCellsSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<TriggersDistanceSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<TriggerActivateSystem>().AsSingle();

            //Container.BindInterfacesAndSelfTo<BlockedCellsSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<WordReleaseSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<CatStepSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathCalc>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelStateChange>().AsSingle();
            Container.BindInterfacesAndSelfTo<RaycastSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PoliceSpawnSystem>().AsSingle();
            
            
            
            Container.BindInterfacesAndSelfTo<LevelStateSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputCleanUpSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DelayCleanUpSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelEndSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GamePauseSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveGameSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStageSystem>().AsSingle();        //always must been last
            Container.BindInterfacesAndSelfTo<CleanUpSystem>().AsSingle();          //must been latest than last!
        }       
    }
}