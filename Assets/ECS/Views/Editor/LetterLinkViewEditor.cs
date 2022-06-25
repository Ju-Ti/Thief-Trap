using ECS.Views.GameCycle;
using UnityEngine;
using UnityEditor;

namespace ECS.Views.Editor
{
    [CustomEditor(typeof(LetterLinkView))]
    public class LetterLinkViewEditor : UnityEditor.Editor
    {
        private LetterLinkView _view;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            _view = target as LetterLinkView;
            InitGenerate();
        }

        private void InitGenerate()
        {
            if (!GUILayout.Button("Generate links"))
                return;

            if (PrefabUtility.IsAnyPrefabInstanceRoot(_view.gameObject))
                PrefabUtility.UnpackPrefabInstance(_view.gameObject, PrefabUnpackMode.OutermostRoot,
                    InteractionMode.UserAction);
            
            for (int i = _view.Transform.childCount - 1; i >= 0; i--)
                DestroyImmediate(_view.Transform.GetChild(i).gameObject);

            LineRenderer lr;
            for (int i = 0; i < _view.Letters.Length - 1; i++)
            for (int j = i + 1; j < _view.Letters.Length; j++)
            {
                lr = ((GameObject) PrefabUtility.InstantiatePrefab(_view.LinkRendererPrefab, _view.Transform)).GetComponent<LineRenderer>();
                lr.positionCount = 2;
                lr.SetPosition(0, _view.Letters[i].Transform.position);
                lr.SetPosition(1, _view.Letters[j].Transform.position);
            }
        }
    }
}