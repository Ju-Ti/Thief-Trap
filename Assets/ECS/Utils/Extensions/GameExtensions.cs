using System;
using ECS.Core.Utils.ReactiveSystem.Components;
using ECS.Game.Components;
using ECS.Game.Components.Flags;
using ECS.Game.Components.GameCycle;
using ECS.Game.Components.General;
using ECS.Game.Components.Thief_Trap_Components;
using ECS.Game.Systems.GameCycle;
using ECS.Game.Systems.General;
using ECS.Game.Systems.Thief_Trap_Systems;
using ECS.Views.GameCycle;
using ECS.Views.General;
using Leopotam.Ecs;
using Runtime.Game.Ui.Windows.LevelComplete;
using Runtime.Game.Ui.Windows.TouchPad;
using Services.Uid;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace ECS.Utils.Extensions
{
    public static class GameExtensions
    {
        public static void CreateEcsEntities(this EcsWorld world)
        {
            world.CreateInput();
            world.CreateLevelState();
            world.CreateDistanceTriggers();
            world.CreateNavMeshLinks();

            world.FindHexagonCell();
            world.FindHexagon();
            world.CreateEnemy();
            world.CreateCamera();
            world.CreateMatrix();
            world.CreateManagePlayer();
            //world.CreatePoliceCar();
        }

        public static EcsEntity GetInput(this EcsWorld world)
        {
            return world.GetEntity<InputComponent>();
        }

        public static void CreateInput(this EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<InputComponent>();
        }

        public static void CreateLevelState(this EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<LevelStateComponent>().State = ELevelState.Start;
        }

        public static void CreateMatrix(this EcsWorld world)
        {
            var views = Object.FindObjectsOfType<MatrixView>(true);

            foreach (var view in views)
            {
                var entity = world.NewEntity();
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                entity.GetAndFire<CellMatrixComponent>();
                entity.LinkView(view);
            }
        }
        
        public static EcsEntity CreateHexagon(this EcsWorld world, CellHexagonComponent.CellHexagonType cellHexagonType) 
        {
            var entity = world.NewEntity();
            entity.Get<UIdComponent>().Value = UidGenerator.Next();
            entity.Get<HexagonObjectComponent>();
            entity.Get<BlockedCellsComponent>();
            entity.GetAndFire<PrefabComponent>().Value = 
                Enum.GetName(typeof(CellHexagonComponent.CellHexagonType), cellHexagonType);
            return entity;
        }

        public static void CreateManagePlayer(this EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<UIdComponent>().Value = UidGenerator.Next();
            entity.Get<PlayerManageComponent>();
            entity.GetAndFire<QueueComponent>().queueStatus = QueueComponent.QueueStatus.Go;
            
        }

        public static void CreatePoliceCar(this EcsWorld world, ILinkable rotateTo, Vector3 spwanPoint , int randNum)
        {
            var entity = world.NewEntity();
            entity.Get<UIdComponent>().Value = UidGenerator.Next();
            entity.Get<PoliceComponent>();
            switch (randNum)
            {
                case 1: entity.GetAndFire<PrefabComponent>().Value = "PoliceCar";
                    break;
                case 2: entity.GetAndFire<PrefabComponent>().Value = "PoliceBoard";
                    break;
            }
            entity.Get<EventSetPositionComponent>().Value = spwanPoint;
            entity.Get<EventSetLookAtComponent>().View = rotateTo;
        }
        public static void CreateEnemy(this EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<UIdComponent>().Value = UidGenerator.Next();
            entity.GetAndFire<EnemyComponent>();
            entity.GetAndFire<PrefabComponent>().Value = "Player"; //Player is Enemy, but in prefab installer he called as "Player"
            // entity.Get<InteractWithTriggerComponent>();
            entity.GetAndFire<QueueComponent>().queueStatus = QueueComponent.QueueStatus.Wait;
        }
        
        
        public static void FindHexagonCell(this EcsWorld world)
        {
            var views = Object.FindObjectsOfType<CellHexagonView>(true);
            
            foreach (var view in views)
            {
                var entity = world.NewEntity();
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                entity.GetAndFire<CellHexagonComponent>();
                entity.LinkView(view);
            }
        }

        public static void FindHexagon(this EcsWorld world)
        {
            var views = Object.FindObjectsOfType<ObjectHexagonView>(true);
            
            foreach (var view in views)
            {
                var entity = world.NewEntity();
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                entity.GetAndFire<HexagonObjectComponent>();
                entity.LinkView(view);
                entity.Get<LinkComponent>().View = view;
            }
        }

       // public static void PlayerUp(this EcsWorld world)
       // {
       //     var view = Object.FindObjectOfType<PlayerView>(true);
       //     var entity = world.NewEntity();
       //     entity.Get<UIdComponent>().Value = UidGenerator.Next();
       //     entity.GetAndFire<>()
       // }
        
        public static void CreateCamera(this EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<UIdComponent>().Value = UidGenerator.Next();
            entity.GetAndFire<CameraComponent>();
            entity.GetAndFire<PrefabComponent>().Value = "Camera";
        }



        public static void CreateWords(this EcsWorld world)
        {
            var views = Object.FindObjectsOfType<WordView>(true);
            foreach (var view in views)
            {
                var entity = world.NewEntity();
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                entity.Get<WordComponent>();
                entity.Get<FreeComponent>();
                entity.LinkView(view);
            }
        }

        public static void CreateLetters(this EcsWorld world)
        {
            var views = Object.FindObjectsOfType<LetterView>(true);
            foreach (var view in views)
            {
                var entity = world.NewEntity();
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                entity.Get<LetterComponent>();
                entity.LinkView(view);
            }
        }
        
        

        public static EcsEntity CreateBusyLetters(this EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<UIdComponent>().Value = UidGenerator.Next();
            entity.Get<BusyLetterComponent>();
            entity.GetAndFire<PrefabComponent>().Value = "BusyLetter";
            return entity;
        }
        
       

        public static void CreateEmptyWords(this EcsWorld world)
        {
            var views = Object.FindObjectsOfType<EmptyWordView>(true);
            foreach (var view in views)
            {
                var entity = world.NewEntity();
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                entity.Get<EmptyWordComponent>();
                entity.LinkView(view);
            }
        }

        public static void CreateLetterLinks(this EcsWorld world)
        {
            var views = Object.FindObjectsOfType<LetterLinkView>(true);
            foreach (var view in views)
            {
                var entity = world.NewEntity();
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                entity.Get<LetterLinkComponent>();
                entity.LinkView(view);

                foreach (var emptyLetterView in entity.Get<LetterLinkComponent>().EmptyLetters)
                {
                    var emptyLetterEntity = world.NewEntity();
                    emptyLetterEntity.Get<UIdComponent>().Value = UidGenerator.Next();
                    emptyLetterEntity.Get<EmptyLetterComponent>().LinkEntity = entity;
                    emptyLetterEntity.LinkView(emptyLetterView);
                }
            }
        }
 
        public static void CreateDistanceTriggers(this EcsWorld world)
        {
            var views = Object.FindObjectsOfType<DistanceTriggerView>(true);
            foreach (var view in views)
            {
                var entity = world.NewEntity();
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                entity.Get<DistanceTriggerComponent>();
                entity.LinkView(view);
            }
        }

        public static void CreateNavMeshLinks(this EcsWorld world)
        {
            var views = Object.FindObjectsOfType<NavMeshLinkView>(true);
            foreach (var view in views)
            {
                var entity = world.NewEntity();
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                entity.LinkView(view);
            }
        }
        
        
        //By player entitys
        
       
    }


}