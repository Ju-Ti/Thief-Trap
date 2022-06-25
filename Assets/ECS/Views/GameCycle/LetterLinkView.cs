using ECS.Views.General;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Views.GameCycle
{
    public class LetterLinkView : LinkableView
    {
        public GameObject LinkRendererPrefab => _linkRendererPrefab;
        [SerializeField] private GameObject _linkRendererPrefab;
        public EmptyLetterView[] Letters => _letters;
        [SerializeField] private EmptyLetterView[] _letters;

        public override void Link(EcsEntity entity)
        {
            base.Link(entity);
            Entity.Get<LetterLinkComponent>().EmptyLetters = _letters;
        }
    }
    
    public struct LetterLinkComponent
    {
        public EmptyLetterView[] EmptyLetters;
    }

    public struct HandledLetterLinkComponent : IEcsIgnoreInFilter
    {
    }
}