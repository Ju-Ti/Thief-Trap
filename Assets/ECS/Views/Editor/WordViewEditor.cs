using ECS.Views.GameCycle;
using UnityEngine;
using UnityEditor;

namespace ECS.Views.Editor
{
    [CustomEditor(typeof(WordView))]
    public class WordViewEditor : UnityEditor.Editor
    {
        private WordView _view;
        private EmptyWordView _emptyWordView;
        private LetterView _letterView;
        private EmptyLetterView _emptyLetterView;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            _view = target as WordView;
            InitGenerate();
        }

        private void InitGenerate()
        {
            if (!GUILayout.Button("Generate word"))
                return;
            
            for (int i = 0; i < _view.Word.Length; i++)
            {
                _letterView = ((GameObject) PrefabUtility.InstantiatePrefab(_view.LetterPrefab, _view.Transform)).GetComponent<LetterView>();
                _letterView.Letter = _view.Word[i];
                _letterView.InitText();
                _letterView.Transform.localPosition += Vector3.right * _view.Offset * (i - ((float) _view.Word.Length - 1) / 2);
                _letterView.name = _view.Word[i].ToString();
            }
            
            
            
            if (PrefabUtility.IsAnyPrefabInstanceRoot(_view.gameObject))
                PrefabUtility.UnpackPrefabInstance(_view.gameObject, PrefabUnpackMode.OutermostRoot,
                    InteractionMode.UserAction);
            
            for (int i = _view.Transform.childCount - 1; i >= 0; i--)
                DestroyImmediate(_view.Transform.GetChild(i).gameObject);
            
            for (int i = 0; i < _view.Word.Length; i++)
            {
                _letterView = ((GameObject) PrefabUtility.InstantiatePrefab(_view.LetterPrefab, _view.Transform)).GetComponent<LetterView>();
                _letterView.Letter = _view.Word[i];
                _letterView.InitText();
                _letterView.Transform.localPosition += Vector3.right * _view.Offset * (i - ((float) _view.Word.Length - 1) / 2);
                _letterView.name = _view.Word[i].ToString();
            }
            
            _view.Collider.size =  new Vector3(_view.Word.Length * _view.Offset, _view.Collider.size.y, _view.Collider.size.z);

            var oldPos = _view.EmptyWordView ? _view.EmptyWordView.Transform.position : Vector3.zero;
            if (_view.EmptyWordView)
                DestroyImmediate(_view.EmptyWordView.gameObject);
            
            _emptyWordView = ((GameObject) PrefabUtility.InstantiatePrefab(_view.EmptyWordPrefab, _view.Transform.parent)).GetComponent<EmptyWordView>();
            _view.EmptyWordView = _emptyWordView;
            _emptyWordView.WordView = _view;
            _emptyWordView.Transform.position = oldPos;
            _emptyWordView.Collider.size =  new Vector3(_view.Word.Length * _view.Offset, _view.Collider.size.y, _view.Collider.size.z);
            _emptyWordView.Word = _view.Word;

            for (int i = 0; i < _view.Word.Length; i++)
            {
                _emptyLetterView = ((GameObject) PrefabUtility.InstantiatePrefab(_emptyWordView.EmptyLetterPrefab, _emptyWordView.Transform)).GetComponent<EmptyLetterView>();
                _emptyLetterView.transform.localPosition += Vector3.right * _view.Offset * (i - ((float) _view.Word.Length - 1) / 2);
                _emptyLetterView.name = _view.Word[i].ToString();
                _emptyLetterView.CharIndex = i;
            }
        }
    }
}