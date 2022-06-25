using System;
using UnityEngine;
using UnityTemplateProjects.Runtime.DataBase.CellType;

namespace Runtime.DataBase.CellType
{
    [CreateAssetMenu(menuName = "Bases/MaterialBase", fileName = "MaterialBase")]
    public class HexagonCellTypeBase : ScriptableObject, IHexagonCellTypeBase
    {
        [SerializeField] private Material[] cellTypes;

        public Material Get(string prefabName)
        {
            for (var i = 0; i < cellTypes.Length; i++)
            {
                var prefab = cellTypes[i];
                if (prefab.name == prefabName)
                    return prefab;
            }

            throw new Exception("[PrefabsBase] Can't find prefab with name: " + prefabName);
        }
    }
}