using System;
using System.Collections;
using System.Collections.Generic;
using ECS.Core.Utils.ReactiveSystem;
using ECS.Core.Utils.ReactiveSystem.Components;
using ECS.Core.Utils.SystemInterfaces;
using ECS.Game.Components.Flags;
using ECS.Game.Components.General;
using ECS.Game.Components.Thief_Trap_Components;
using ECS.Views.General;
using Leopotam.Ecs;
using ModestTree;
using Runtime.Services.CommonPlayerData;
using Runtime.Services.CommonPlayerData.Data;
using UnityEngine;
using UnityTemplateProjects.Runtime.DataBase;
using Zenject;

namespace ECS.Game.Systems.Thief_Trap_Systems
{
    public class MatrixCellInitSystem : ReactiveSystem<EventAddComponent<CellMatrixComponent>>
    {
        [Inject] private IHexagonCellField _hexagonCellField;
        [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _commonPlayerData;
        
        private readonly EcsFilter<EnemyComponent, LinkComponent> _player;
        private readonly EcsFilter<CellHexagonComponent, LinkComponent> _hexagonCell;
        
        protected override EcsFilter<EventAddComponent<CellMatrixComponent>> ReactiveFilter { get; }

        private int[,] _cellMatrix;

        private CellHexagonView _cellHexagonView;
        private MatrixView _matrixView;

        private int _columnsOfMatrix;
        private int _linesOfMatrix;
        private int _indexI;
        private int _indexJ;

        private string _currentLevel;
        
        protected override void Execute(EcsEntity entity)
        {
            CreateCurrentMatrix();
            entity.Get<CellMatrixComponent>().CellMatrix = _cellMatrix;
            
            _matrixView = entity.Get<LinkComponent>().View as MatrixView;
            _matrixView.SaveAndPrintMatrix(_cellMatrix);
        }
        
        private void CreateCurrentMatrix()
        {
            _currentLevel = Enum.GetName(typeof(EScene), _commonPlayerData.GetData().Level)?.Replace("Level_0", "");
            
            _columnsOfMatrix = _hexagonCellField.Get(_currentLevel).NumberOfColumns;
            _linesOfMatrix = _hexagonCellField.Get(_currentLevel).NumberOfLines;

            _cellMatrix = new int[_linesOfMatrix, _columnsOfMatrix];
            
            foreach (var cell in _hexagonCell)
            {
                _cellHexagonView = _hexagonCell.Get2(cell).Get<CellHexagonView>();
                _indexI = _cellHexagonView.FirstIndex_X;
                _indexJ = _cellHexagonView.SecondIndex_Y;
                
                _cellMatrix[_indexI, _indexJ] = (int) _cellHexagonView.cellStatus;
            }
        }
        private void ShowMatrixInDebug()
        {
            for (int j = 0; j < _cellMatrix.GetLength(1); j++)
            {
                for (int i = 0; i < _cellMatrix.GetLength(0); i++)
                {
                    var msg = "[" + i.ToString() + ", " + j.ToString() + "] = " + _cellMatrix[i, j].ToString();
                    Debug.Log(msg);
                }
            }
        }
    }
}

public struct CellMatrixComponent
{
    public int[,] CellMatrix;
}
