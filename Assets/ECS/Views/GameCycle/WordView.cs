using ECS.Game.Components.GameCycle;
using ECS.Views.General;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Views.GameCycle
{
    public class WordView : LinkableView
    {
        public GameObject LetterPrefab => _letterPrefab;
        [Header("LetterSettings")] [SerializeField] private GameObject _letterPrefab;
        public float Offset => _offset;
        [SerializeField] private float _offset;
        public string Word => _word;
        [Header("WordSettings")] [SerializeField] private string _word;
        [SerializeField] public EmptyWordView EmptyWordView;
        [SerializeField] public BoxCollider Collider;
        public GameObject EmptyWordPrefab => _emptyWordPrefab;
        [SerializeField] private GameObject _emptyWordPrefab;

        public override void Link(EcsEntity entity)
        {
            base.Link(entity);
            Entity.Get<WordComponent>().Value = Word;
            entity.Get<DefaultPosition>().Value = Transform.position;
        }
    }

    public struct WordComponent
    {
        public string Value;
    }
    
    public struct FreeComponent : IEcsIgnoreInFilter
    {}
}