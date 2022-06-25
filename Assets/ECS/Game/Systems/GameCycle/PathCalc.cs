using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using ECS.Core.Utils.ReactiveSystem;
using ECS.Game.Components.Flags;
using ECS.Game.Components.General;
using ECS.Game.Components.Thief_Trap_Components;
using ECS.Game.Systems.General;
using ECS.Utils.Extensions;
using ECS.Views.General;
using Leopotam.Ecs;
using ModestTree;
using UniRx.Triggers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.UI;
using UnityEngine.Tilemaps;
using Runtime.Game.Ui ;
using Runtime.Game.Ui.Windows.LevelComplete;
using Game.SceneLoading;
using Runtime.DataBase.Game;
using Runtime.Game.Ui.Windows.GameOver;
using Runtime.Services.SceneLoading.Impls;
using SimpleUi.Signals;
using UnityEditor.Animations;
using Zenject;

namespace ECS.Game.Systems.GameCycle
{
    public class PathCalc : ReactiveSystem<ActiveStateCat>
    {
        [Inject] private SignalBus _signalBus;
        
        private EcsWorld _world;
        protected override EcsFilter<ActiveStateCat> ReactiveFilter { get; }
        private EcsFilter<PlayerManageComponent> _activePlayer;
        private EcsFilter<ActiveStateCat> _start;
        private EcsFilter<CellHexagonComponent> _cells;
        private EcsFilter<EnemyComponent, LinkComponent> _enemy;

        private readonly LevelCompleteController _levelCompleteController;

        protected override void Execute(EcsEntity entity)
        {
            finalPath();
        }

        protected void finalPath()
        {
           
            foreach (var x in _start)
            {
                var _enemyEntity = _enemy.GetEntity(0);
                var _enemyView = _enemyEntity.Get<LinkComponent>().View as EnemyView;

                var _cellEntity = new EcsEntity();
                var _playerEntity = new EcsEntity();

                var _cellView = new CellHexagonView();
                var targetTilePosition = new CellHexagonView();
                var currentTilePosition = new CellHexagonView();

                var _matrixCellViews = new List<CellHexagonView>();
                var _toPoint = new List<CellHexagonView>();

                foreach (var i in _activePlayer)
                {
                    _playerEntity = _activePlayer.GetEntity(i);
                }

                int j = _cells.GetEntitiesCount();

                foreach (var i in _cells)
                {
                    _cellEntity = _cells.GetEntity(i);
                    _cellView = _cellEntity.Get<LinkComponent>().View as CellHexagonView;
                    //Matrix state
                    if (_matrixCellViews.Count < j && !_matrixCellViews.Contains(_cellView))
                    {
                        _matrixCellViews.Add(_cellView);
                    }

                    // Target Tiles
                    if (_cellView.cellHexagonType == CellHexagonComponent.CellHexagonType.Base)
                    {
                        _toPoint.Add(_cellView);
                    }

                    //Find Current Tile
                    if (Vector3.Distance(_enemyView.Transform.position, _cellView.Transform.position) < 0.1f)
                    {
                        currentTilePosition = _cellView;
                    }
                    

                }

                // Calculate Current Tile Params
                
                // add adjacent tiles to current tile
                foreach (var s in _matrixCellViews)
                {
                    currentTilePosition.g = 0;
                    if (Vector3.Distance(currentTilePosition.Transform.position,
                            s.Transform.position) < 1.2f && s != currentTilePosition)
                    {
                        currentTilePosition._adjacentTile.Add(s);
                    }
                }
                
                var _toPointList = new List<List<CellHexagonView>>();
                var _openPath = new List<CellHexagonView>();
                var _closedPath = new List<CellHexagonView>();
                
                foreach (var t in _toPoint)
                {

                    var _finalPath = new List<CellHexagonView>();
                    targetTilePosition = t;

                    currentTilePosition.h = GetCost(
                        new Vector3Int(currentTilePosition.FirstIndex_X, currentTilePosition.SecondIndex_Y,
                            currentTilePosition.ThirdIndex_Z),
                        new Vector3Int(targetTilePosition.FirstIndex_X, targetTilePosition.SecondIndex_Y,
                            targetTilePosition.ThirdIndex_Z));

                    currentTilePosition.F = currentTilePosition.g + currentTilePosition.h;
                    //Debug.Log(currentTilePosition.h);
                    
                    _openPath.Add(currentTilePosition);
                    while (_openPath.Count != 0)
                    {
                        _openPath = _openPath.OrderBy(x => x.F).ThenByDescending(x => x.g).ToList();
                        currentTilePosition = _openPath[0];

                        _openPath.Remove(currentTilePosition);
                        _closedPath.Add(currentTilePosition);
                        
                        int g = currentTilePosition.g + 1;

                        if (_closedPath.Contains(targetTilePosition))
                            break;


                        foreach (var c in currentTilePosition._adjacentTile)
                        {
                            if (c.cellStatus == CellHexagonComponent.CellStatus.IsBlocked)
                                continue;
                            if (_closedPath.Contains(c))
                                continue;
                            if (!(_openPath.Contains(c)))
                            {
                                foreach (var s in _matrixCellViews)
                                {
                                    float dist = Vector3.Distance(c.Transform.position,
                                        s.Transform.position);
                                    if (dist < 1.2f)
                                    {
                                        c._adjacentTile.Add(s);
                                    }
                                }

                                c.g = g;
                                c.h = (GetCost(new Vector3Int(c.FirstIndex_X, c.SecondIndex_Y, c.ThirdIndex_Z),
                                    new Vector3Int(targetTilePosition.FirstIndex_X, targetTilePosition.SecondIndex_Y,
                                        targetTilePosition.ThirdIndex_Z)));
                                c.F = c.g + c.h;
                                _openPath.Add(c);
//
                            }
                            else if (c.F > g + c.h)
                                c.g = g;
                        }

                    }
                    if (_closedPath.Contains(targetTilePosition))
                    {
                        foreach (var cl in _closedPath)
                        {
                            currentTilePosition = targetTilePosition;

                            _finalPath.Add(currentTilePosition);


                            for (int h = targetTilePosition.g - 1; h >= 0; h--)
                            {

                                foreach (var z in _closedPath)
                                {
                                    if (z.g == h && currentTilePosition._adjacentTile.Contains(z))
                                    {
                                        currentTilePosition = z;
                                        break;
                                    }
                                }
                                _finalPath.Add(currentTilePosition);
                            }

                            _finalPath.Reverse();
                            _toPointList.Add(_finalPath);
                            break;
                        }
                        
                    }

                }
                if (_toPointList.Count == 0)
                {
                    _signalBus.OpenWindow<LevelCompleteWindow>();
                    break;
                }
                _toPointList.Sort((x1, x2) => x1.Count.CompareTo(x2.Count));
                _enemyEntity.Get<RotateComponent>();
                _enemyView.Transform.rotation = Quaternion.LookRotation(_toPointList[0][1].Transform.position - _enemyView.Transform.position);


                var enemyTemp = _enemy.Get1(0);
                enemyTemp.enemyAnimation = _enemyView.GetComponent<Animator>();
                enemyTemp.enemyAnimation.SetBool("isJumping", true);
                _enemyView.Transform.DOMove(_toPointList[0][1].Transform.position + new Vector3(0 , 0.08f, 0), 1f).SetEase(Ease.Linear)
                        .OnComplete(() =>
                        {
                            _playerEntity.Get<QueueComponent>().queueStatus = QueueComponent.QueueStatus.Wait;
                            _playerEntity.GetAndFire<QueueComponent>().queueStatus =
                                QueueComponent.QueueStatus.Go;
                            _playerEntity.Del<ActiveStateCat>();
                            enemyTemp.enemyAnimation.SetBool("isJumping", false);
                            if (_toPointList[0][1].cellHexagonType == CellHexagonComponent.CellHexagonType.Base)
                            {
                                //enemyTemp.enemyAnimation.SetBool("isJumping", false);
                                //enemyTemp.enemyAnimation.SetBool("isRunning", true);
                            }
                        });
                


                if (_toPointList[0][1].cellHexagonType == CellHexagonComponent.CellHexagonType.Base)
                {
                    _signalBus.OpenWindow<GameOverWindow>();
                }
            }
        }
        
        protected static int GetCost(Vector3Int startTile, Vector3Int targetTile)
        {
            return Mathf.Max(Mathf.Abs(startTile.x - targetTile.x), Mathf.Abs(startTile.y - targetTile.y),
                Mathf.Abs(startTile.z - targetTile.z));
        }
    }

    public struct RotateComponent
    {
    }
}