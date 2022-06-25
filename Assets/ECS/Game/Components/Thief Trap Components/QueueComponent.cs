using ECS.Game.Components.General;
using ECS.Views.General;
using Leopotam.Ecs;
using UnityEngine;



namespace ECS.Game.Components.Thief_Trap_Components
{

    public struct QueueComponent : IEcsIgnoreInFilter

    {
        public QueueStatus queueStatus;
        public enum QueueStatus
        {
            Go,
            Wait
        }
    }
}