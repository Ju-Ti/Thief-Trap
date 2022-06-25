using System.Collections.Generic;
using ECS.Game.Components.Thief_Trap_Components;
using UnityEngine;

namespace ECS.Views.General
{
    public class CellHexagonView : LinkableView
    {
        [SerializeField] public CellHexagonComponent.CellHexagonType cellHexagonType;
        [SerializeField] public CellHexagonComponent.CellStatus cellStatus;
        [SerializeField] public int FirstIndex_X;
        [SerializeField] public int SecondIndex_Y;
        [SerializeField] public int ThirdIndex_Z;
        
        public int g;
        public int h;
        public float F;
        
        public List<CellHexagonView> _adjacentTile = new List<CellHexagonView>();

        
    }
}