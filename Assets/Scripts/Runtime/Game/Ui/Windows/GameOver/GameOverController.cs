using Game.SceneLoading;
using Runtime.Services.AnalyticsService;
using Runtime.Services.CommonPlayerData;
using Runtime.Services.CommonPlayerData.Data;
using Runtime.Signals;
using SimpleUi.Abstracts;
using UniRx;
using Utils.UiExtensions;
using Zenject;

namespace Runtime.Game.Ui.Windows.GameOver 
{
    public class GameOverController : UiController<GameOverView>, IInitializable
    {
        [Inject] private IAnalyticsService _analyticsService;
        [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _commonPlayerData;
        private readonly SignalBus _signalBus;
        
        private readonly ISceneLoadingManager _sceneLoadingManager;
        
        public GameOverController(SignalBus signalBus, ISceneLoadingManager sceneLoadingManager)
        {
            _sceneLoadingManager = sceneLoadingManager;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            View.Restart.OnClickAsObservable().Subscribe(x => OnRestart()).AddTo(View.Restart);
            // _signalBus.GetStream<SignalScoreUpdate>().Subscribe(x => View.UpdateScore(ref x)).AddTo(View);
        }
        
        public override void OnShow()
        {
            var data = _commonPlayerData.GetData();
            View.Show(data);
            // ShowScore();
            _analyticsService.SendRequest("level_failed");
        }

        // private void ShowScore()
        // {
        //     var data = _commonPlayerData.GetData();
        //     ref var score = ref View.GetScore();
        //     if (data.HighScore < score)
        //     {
        //         data.HighScore = score;
        //         _commonPlayerData.Save(data);
        //     }
        // }

        private void OnRestart()
        {
            _sceneLoadingManager.ReloadScene();
        }
    }
}