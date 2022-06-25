using System;
using DG.Tweening;
using ECS.Game.Systems.GameCycle;
using ECS.Utils.Extensions;
using Game.SceneLoading;
using Leopotam.Ecs;
using Runtime.Game.Ui.Windows.InGameMenu;
using Runtime.Services.CommonPlayerData;
using Runtime.Services.CommonPlayerData.Data;
using Runtime.Services.DelayService;
using SimpleUi.Abstracts;
using SimpleUi.Signals;
using UniRx;
using UnityEngine;
using Utils.UiExtensions;
using Zenject;

namespace Runtime.Game.Ui.Windows.InGameButtons
{
    public class InGameButtonsController : UiController<InGameButtonsView>, IInitializable
    {
        [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _commonPlayerData;
        [Inject] private readonly ISceneLoadingManager _sceneLoadingManager;
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly IDelayService _delayService;
        
        private EcsWorld _world;

        private int _lastPrice = 0;
        private int _lastHp;
        private Color _color1 = new Color(0.347f, 0.964f, 0.185f, 1f);
        private Color _color2 = new Color(0.965f, 0.922f, 0.185f, 1f);
        private Color _color3 = new Color(0.965f, 0.185f, 0.34f, 1f);
        private Color _currentColor;
        private Color _tipColor;
        
        public InGameButtonsController(EcsWorld world) => _world = world;

        public void Initialize()
        {
            View.DropProgressButton.OnClickAsObservable().Subscribe(x => OnDropProggress()).AddTo(View);
            View.InGameMenuBtn.OnClickAsObservable().Subscribe(x => OnGameMenu()).AddTo(View);
            View.RollbackBtn.OnClickAsObservable().Subscribe(x => _world.SetLevelState(ELevelState.RollBack)).AddTo(View);
            InitTapOnStart();

            if (_commonPlayerData.GetData().Level == EScene.Level_01)
            {
            }
        }

        public void InitTapOnStart()
        {

            if (View.StartToPlayBtn.gameObject.activeSelf)
            {
                View.StartToPlayBtn.OnClickAsObservable().Subscribe(x => OnStartToPlay()).AddTo(View.StartToPlayBtn);
                View.UiBox.gameObject.SetActive(false);
                View.StartToPlayBtn.transform.DOScale(0.1f, 1.5f).SetRelative(true).SetLoops(-1, LoopType.Yoyo);
            }

            void OnStartToPlay()
            {
                View.UiBox.gameObject.SetActive(true);
                View.StartToPlayBtn.gameObject.SetActive(false);
                if (_commonPlayerData.GetData().Level == EScene.Level_01)
                {
                    View.TipText.gameObject.SetActive(true);
                    View.TipText.transform.DOScale(0.07f, 1.5f).SetRelative(true).SetLoops(-1, LoopType.Yoyo);
                }
            }
        }

        public override void OnShow()
        {
            View.LevelN.text = Enum.GetName(typeof(EScene), _commonPlayerData.GetData().Level)?.Replace("_", " ");
            var data = _commonPlayerData.GetData();
            View.Coins.text = data.Money.ToString();
        }

        public void OnDropProggress()
        {
            _commonPlayerData.Save(new CommonPlayerData());
            _sceneLoadingManager.LoadScene(_commonPlayerData.GetData().Level);
        }

        private void OnGameMenu()
        {
            _signalBus.OpenWindow<InGameMenuWindow>();
        }

        private void CloseStickersWindow() => _world.SetLevelState(ELevelState.RollBack);
    }
}