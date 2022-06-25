using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Views.General
{
    public class ParticleView : LinkableView
    {
        
    }

    public struct ParticleComponent : IEcsIgnoreInFilter
    {
        public Vector3 Pos;
    }
}