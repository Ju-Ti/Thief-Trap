using ECS.Core.Utils.ReactiveSystem;
using ECS.Game.Components.Flags;
using ECS.Game.Components.GameCycle;
using ECS.Game.Components.General;
using ECS.Utils.Extensions;
using ECS.Views.GameCycle;
using Leopotam.Ecs;
using Runtime.DataBase.Game;
using Runtime.Game.Utils.MonoBehUtils;
using Runtime.Services.VibrationService;
using UnityEngine;
using Zenject;

namespace ECS.Game.Systems.GameCycle
{
    public class LevelStateSystem : ReactiveSystem<EventChangeLevelStateComponent>
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private readonly ScreenVariables _screenVariables;
        [Inject] private readonly IVibrationService _vibrationService;

        private readonly EcsWorld _world;

        private readonly EcsFilter<WordComponent, LinkComponent, DefaultPosition> _words;
        private readonly EcsFilter<EmptyWordComponent, LinkComponent> _emptyWords;
        private readonly EcsFilter<LetterComponent, BusyLetterComponent> _busyLetters;
        private readonly EcsFilter<HandledLetterLinkComponent> _links;
        protected override EcsFilter<EventChangeLevelStateComponent> ReactiveFilter { get; }
        protected override bool DeleteEvent => true;

        private WordView _wordView;
        private EmptyWordView _emptyWordView;
        private EcsEntity _wordEntity;

        protected override void Execute(EcsEntity entity)
        {
            entity.Get<LevelStateComponent>().State = entity.Get<EventChangeLevelStateComponent>().State;

            switch (entity.Get<LevelStateComponent>().State)
            {
                case ELevelState.RollBack:
                    foreach (var i in _words)
                    {
                        _wordView = _words.Get2(i).Get<WordView>();
                        _wordEntity = _words.GetEntity(i);

                        _wordView.Transform.position = _words.Get3(i).Value;
                        _wordView.gameObject.layer = LayerMask.NameToLayer("Draggable");
                        for (int j = 0; j < _wordView.Transform.childCount; j++)
                            _wordView.Transform.GetChild(j).gameObject.SetActive(true);
                        
                        _wordEntity.Get<FreeComponent>();
                    }

                    foreach (var i in _emptyWords)
                    {
                        _emptyWordView =  _emptyWords.Get2(i).Get<EmptyWordView>();
                        _emptyWordView.gameObject.SetActive(true);
                        _emptyWordView.gameObject.layer = LayerMask.NameToLayer("Releasable");
                    }

                    foreach (var i in _busyLetters)
                    {
                        _busyLetters.GetEntity(i).Get<IsDestroyedComponent>();
                    }

                    foreach (var i in _links)
                    {
                        _links.GetEntity(i).Del<HandledLetterLinkComponent>();
                    }

                    break;
                case ELevelState.End:
                    _world.SetStage(EGameStage.Complete);
                    break;
            }
        }
    }

    public struct EventChangeLevelStateComponent
    {
        public ELevelState State;
    }

    public struct LevelStateComponent
    {
        public ELevelState State;
    }

    public enum ELevelState
    {
        Start,
        RollBack,
        End
    }
}