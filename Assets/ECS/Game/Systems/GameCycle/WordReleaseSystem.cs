using DG.Tweening;
using ECS.Core.Utils.ReactiveSystem;
using ECS.Game.Components.GameCycle;
using ECS.Game.Components.General;
using ECS.Utils.Extensions;
using ECS.Views.GameCycle;
using Leopotam.Ecs;
using Runtime.Game.Utils.MonoBehUtils;
using Runtime.Services.VibrationService;
using UnityEngine;
using Zenject;

namespace ECS.Game.Systems.GameCycle
{
    public class WordReleaseSystem : ReactiveSystem<EventWordReleaseComponent>
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private readonly ScreenVariables _screenVariables;
        [Inject] private readonly IVibrationService _vibrationService;

        private readonly EcsWorld _world;
        protected override EcsFilter<EventWordReleaseComponent> ReactiveFilter { get; }
        private readonly EcsFilter<WordComponent, FreeComponent> _freeWords;
        protected override bool DeleteEvent => true;

        private WordView _view;
        private EcsEntity _entity;
        private EmptyWordView _emptyWordView;
        private EcsEntity _emptyWordEntity;
        private EcsEntity _cacheLetterEntity;

        protected override void Execute(EcsEntity entity)
        {
            _view = entity.Get<LinkComponent>().Get<WordView>();
            _entity = entity;
            _emptyWordEntity = _entity.Get<EventWordReleaseComponent>().EmptyWordEntity;
            _emptyWordView = _entity.Get<EventWordReleaseComponent>().EmptyWordEntity.Get<LinkComponent>()
                .Get<EmptyWordView>();

            if (_entity.Get<WordComponent>().Value.Length !=
                _emptyWordView.Entity.Get<EmptyWordComponent>().Value.Length)
            {
                ReturnWord();
                return;
            }

            if (!CanBePlacedHere())
            {
                ReturnWord();
                return;
            }

            _entity.Del<FreeComponent>();
            _view.Transform.DOMove(_emptyWordView.Transform.position, 0.3f).OnComplete(PlaceAllLinked);
            _view.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            _emptyWordView.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

            if (_freeWords.GetEntitiesCount() < 1) _world.SetLevelState(ELevelState.End);
        }

        private void ReturnWord()
        {
            _view.Transform.position = _entity.Get<DefaultPosition>().Value;
        }

        private bool CanBePlacedHere()
        {
            foreach (var emptyLetterView in _emptyWordEntity.Get<EmptyWordComponent>().EmptyLetters)
            {
                _cacheLetterEntity = emptyLetterView.Entity;
                if (!_cacheLetterEntity.IsNull() &&
                    _cacheLetterEntity.Has<BusyLetterComponent>() &&
                    _entity.Get<WordComponent>().Value[_cacheLetterEntity.Get<EmptyLetterComponent>().CharIndex] !=
                    _cacheLetterEntity.Get<BusyLetterComponent>().Char)
                    return false;
            }

            return true;
        }

        private void PlaceAllLinked()
        {
            _emptyWordView.gameObject.SetActive(false);
            
            foreach (var emptyLetterView in _emptyWordEntity.Get<EmptyWordComponent>().EmptyLetters)
            {
                _cacheLetterEntity = emptyLetterView.Entity;
                if (_cacheLetterEntity.IsNull())
                    continue;

                _view.Transform.GetChild(_cacheLetterEntity.Get<EmptyLetterComponent>().CharIndex).gameObject
                    .SetActive(false);
                var linkEntity = _cacheLetterEntity.Get<EmptyLetterComponent>().LinkEntity;
                if (linkEntity.Has<HandledLetterLinkComponent>())
                    continue;
                
                _cacheLetterEntity.Get<BusyLetterComponent>();
                linkEntity.Get<HandledLetterLinkComponent>();
                var letter = _entity.Get<WordComponent>()
                    .Value[_cacheLetterEntity.Get<EmptyLetterComponent>().CharIndex];
                foreach (var emptyLetter in linkEntity.Get<LetterLinkComponent>().EmptyLetters)
                {
                    emptyLetter.Entity.Get<BusyLetterComponent>().Char = letter;
                    var entity = _world.CreateBusyLetters();
                    entity.Get<LetterComponent>().Value = letter;
                    entity.Get<EventSetPositionComponent>().Value = emptyLetter.Transform.position;
                }
            }
        }
    }

    public struct EventWordReleaseComponent
    {
        public EcsEntity EmptyWordEntity;
    }
}