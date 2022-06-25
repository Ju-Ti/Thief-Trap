using System;
using System.Collections.Generic;
using Runtime.DataBase.Objects.Impl;
using UnityEngine;

namespace UnityTemplateProjects.Runtime.DataBase
{
    [CreateAssetMenu(menuName = "Bases/HexagonFields", fileName = "HexagonFields")]
    public class HexagonCellField : ScriptableObject , IHexagonCellField
    {
        [SerializeField] private List<GameField> GameFields;

        public GameField Get(string prefabName)
        {
            for (var i = 0; i < GameFields.Count; i++)
            {
                var gameField = GameFields[i];
                if (gameField.Name == prefabName)
                    return gameField;
            }
            throw new Exception("[PrefabsBase] Can't find prefab with name: " + prefabName);
        }
        
        [Serializable]
        public class GameField
        {
            public string Name;
            public int NumberOfColumns;
            public int NumberOfLines;

        }
    }
}