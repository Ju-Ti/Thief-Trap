using ECS.Views.General;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Views.GameCycle
{
    public class EmptyLetterView : LinkableView
    {
        [HideInInspector] public int CharIndex;
        
        public override void Link(EcsEntity entity)
        {
            base.Link(entity);
            Entity.Get<EmptyLetterComponent>().CharIndex = CharIndex;
        }
    }

    public struct EmptyLetterComponent
    {
        public int CharIndex;
        public EcsEntity LinkEntity;
    }
    
    public struct BusyLetterComponent
    {
        public char Char;
    }
}