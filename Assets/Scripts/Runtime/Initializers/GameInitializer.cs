using System.Collections.Generic;
using ECS.DataSave;
using ECS.Game.Systems.GameCycle;
using ECS.Game.Systems.General;
using ECS.Utils.Extensions;
using Leopotam.Ecs;
using PdUtils.Interfaces;
using Runtime.Game.Ui.Windows.TouchPad;
using Runtime.Services.GameStateService;
using Zenject;

namespace Runtime.Initializers
{
    public class GameInitializer : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly ITouchpadViewController _touchpadViewController;
        private readonly IGameStateService<GameState> _gameState;
        private readonly IList<IUiInitializable> _uiInitializables; //late initialize after ecs init
        private readonly EcsWorld _world;

        public GameInitializer(SignalBus signalBus, ITouchpadViewController touchpadViewController,
            IList<IUiInitializable> uiInitializables, IGameStateService<GameState> gameState, EcsWorld world)
        {
            _signalBus = signalBus;
            _touchpadViewController = touchpadViewController;
            _uiInitializables = uiInitializables;
            _gameState = gameState;
            _world = world;
        }

        public void Initialize()
        {
            foreach (var ui in _uiInitializables)
                ui.Initialize();
            
            _world.GetEntity<LevelStateComponent>().Get<EventLevelStartComponent>();
            _touchpadViewController.SetActive(true);
        }
    }
}