using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Game.Components.Flags
{
    public struct EnemyComponent
    {
        public Animator enemyAnimation;
        public bool isJumping;
        public bool isRunning;
    }
}