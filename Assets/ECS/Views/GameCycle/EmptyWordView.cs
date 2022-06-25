using ECS.Views.General;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Views.GameCycle
{
    public class EmptyWordView : LinkableView
    {
        public GameObject EmptyLetterPrefab => _emptyLetterPrefab;
        [Header("LetterSettings")] [SerializeField] private GameObject _emptyLetterPrefab;
        [Header("WordSettings")] [SerializeField] public WordView WordView;
        [SerializeField] public BoxCollider Collider;
        [HideInInspector] public string Word;
        
        public override void Link(EcsEntity entity)
        {
            base.Link(entity);
            Entity.Get<EmptyWordComponent>().Value = Word;
            
            ref var emptyLetters = ref Entity.Get<EmptyWordComponent>().EmptyLetters;
            emptyLetters = new EmptyLetterView[Transform.childCount];
            for (int i = 0; i < Transform.childCount; i++)
                emptyLetters[i] = Transform.GetChild(i).GetComponent<EmptyLetterView>();
        }
    }

    public struct EmptyWordComponent
    {
        public string Value;
        public EmptyLetterView[] EmptyLetters;
    }
}