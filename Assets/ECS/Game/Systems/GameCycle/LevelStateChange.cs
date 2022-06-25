using ECS.Core.Utils.ReactiveSystem;
using ECS.Core.Utils.ReactiveSystem.Components;
using ECS.Game.Components.Flags;
using ECS.Game.Components.General;
using ECS.Game.Components.Thief_Trap_Components;
using ECS.Utils.Extensions;
using ECS.Views.General;
using Leopotam.Ecs;
using UnityEditorInternal;
using UnityEngine;

namespace ECS.Game.Systems.GameCycle
{
    public class LevelStateChange : ReactiveSystem<EventAddComponent<QueueComponent>>
    {
        protected override EcsFilter<EventAddComponent<QueueComponent>> ReactiveFilter { get; }

        protected override void Execute(EcsEntity entity)
        {
            switch (entity.Get<QueueComponent>().queueStatus)
            {
                case QueueComponent.QueueStatus.Go :
                    if (entity.Has<PlayerManageComponent>())
                    {
                        entity.Get<ActiveStatePlayer>(); // Player Go
                        //Debug.Log("PlayerStep");
                    }
                    else
                    {
                        entity.GetAndFire<ActiveStateCat>();  // Cat Go
                        //Debug.Log("CatStep");
                    }
                    break;
                
                //case QueueComponent.QueueStatus.Wait :
                //    if (entity.Has<PlayerManageComponent>())
                //    {
                //        entity.Get<ActiveStatePlayer>(); // Player Wait
                //        Debug.Log(33);
                //    }
                //    else
                //    {
                //        entity.Get<ActiveStateCat>();   // Cat Wait
                //        Debug.Log(44);
                //    }
                //    //entity.Get<ActiveStateCat>();
                //    break;
            }
        }
    }

    public struct ActiveStatePlayer : IEcsIgnoreInFilter
    {
        
    }
    
    public struct ActiveStateCat : IEcsIgnoreInFilter
    {
        
    }
}