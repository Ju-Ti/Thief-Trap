using DataBase.Audio;
using DataBase.FX;
using DataBase.Objects;
using Runtime.DataBase.CellType;
using Runtime.DataBase.FX;
using Runtime.DataBase.FX.Impl;
using Runtime.DataBase.General.CommonParamsBase;
using Runtime.DataBase.General.CommonParamsBase.Impl;
using Runtime.DataBase.General.GameCFG;
using Runtime.DataBase.General.GameCFG.Impl;
using Runtime.DataBase.Objects;
using Runtime.DataBase.Objects.Impl;
using UnityEditor;
using UnityEngine;
using UnityTemplateProjects.Runtime.DataBase;
using UnityTemplateProjects.Runtime.DataBase.CellType;
using Zenject;
using ZenjectUtil.Test.Extensions;

namespace Runtime.Installers
{
    [CreateAssetMenu(menuName = "Installers/ProjectPrefabsInstaller", fileName = "ProjectPrefabsInstaller")]
    public class ProjectPrefabsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private PrefabsBase prefabBase;
        [SerializeField] private HexagonCellTypeBase hexagonCellTypeBase;
        [SerializeField] private HexagonCellField hexagonCellField;
        [SerializeField] private AudioBase audioBase;
        [SerializeField] private FxBase fxBase;
        [SerializeField] private CommonParamsBase commonParamsBase;
        [SerializeField] private GameConfig gameConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<IPrefabsBase>().FromSubstitute(prefabBase).AsSingle();
            Container.Bind<IHexagonCellTypeBase>().FromSubstitute(hexagonCellTypeBase).AsSingle();
            Container.Bind<IHexagonCellField>().FromSubstitute(hexagonCellField).AsSingle();
            Container.Bind<IAudioBase>().FromSubstitute(audioBase).AsSingle();
            Container.Bind<IFxBase>().FromSubstitute(fxBase).AsSingle();
            Container.Bind<ICommonParamsBase>().FromSubstitute(commonParamsBase).AsSingle();
            Container.Bind<IGameConfig>().FromSubstitute(gameConfig).AsSingle();
        }
#if UNITY_EDITOR
        [MenuItem("Assets/Bases/Project bases", false, 1000000)]
        public static void SelectBases()
        {
            Selection.activeObject = Resources.Load("Settings/Installers/ProjectPrefabsInstaller");
        }
        [MenuItem("Assets/Bases/Graph bases", false, 1000000)]
        public static void SelectGraphBases()
        {
            Selection.activeObject = Resources.Load("Settings/Bases/GameActionsGraphBase");
        }
        [MenuItem("Assets/Bases/Game actions bases", false, 1000000)]
        public static void SelectGameActionsBases()
        {
            Selection.activeObject = Resources.Load("Settings/Bases/MainGameActionsBase");
        }
        [MenuItem("Assets/Bases/Heroes base", false, 1000000)]
        public static void SelectHeroesBase()
        {
            Selection.activeObject = Resources.Load("Settings/Bases/HeroBase");
        }
#endif        
    }
}