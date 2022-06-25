using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using ECS.Game.Systems.GameCycle;
using UnityEngine;
using UnityEngine.AI;

namespace ECS.Views.General
{
    public class EnemyView : LinkableView, IHasNavMeshAgent


    {
        //List<CellHexagonView> adjacentTiles = new List<CellHexagonView>();

        private int valueToPath = 8;

        private void Start()
        {

        }

        public ref NavMeshAgent GetAgent()
        {
            throw new NotImplementedException();
        }
    }
}
// List<CellHexagonView> adjacentTiles = new List<CellHexagonView>();
    //
    // public List<CellHexagonView> FindPath(CellHexagonView startPoint, CellHexagonView endPoint)
    // {
    //     List<CellHexagonView> openPathCellHexagonViews = new List<CellHexagonView>();
    //     List<CellHexagonView> closedPathCellHexagonViews = new List<CellHexagonView>();
    //
    //     // Prepare the start CellHexagonView.
    //     CellHexagonView currentCellHexagonView = startPoint;
    //
    //     currentCellHexagonView.g = 0;
    //     currentCellHexagonView.h = GetEstimatedPathCost(startPoint.position, endPoint.position);
    //
    //     // Add the start CellHexagonView to the open list.
    //     openPathCellHexagonViews.Add(currentCellHexagonView);
    //
    //     while (openPathCellHexagonViews.Count != 0)
    //     {
    //         // Sorting the open list to get the CellHexagonView with the lowest F.
    //         openPathCellHexagonViews = openPathCellHexagonViews.OrderBy(x => x.F).ThenByDescending(x => x.g).ToList();
    //         currentCellHexagonView = openPathCellHexagonViews[0];
    //
    //         // Removing the current CellHexagonView from the open list and adding it to the closed list.
    //         openPathCellHexagonViews.Remove(currentCellHexagonView);
    //         closedPathCellHexagonViews.Add(currentCellHexagonView);
    //
    //         int g = currentCellHexagonView.g + 1;
    //
    //         // If there is a target CellHexagonView in the closed list, we have found a path.
    //         if (closedPathCellHexagonViews.Contains(endPoint))
    //         {
    //             break;
    //         }
    //
    //         // Investigating each adjacent CellHexagonView of the current CellHexagonView.
    //         foreach (CellHexagonView adjacentCellHexagonView in currentCellHexagonView.adjacentCellHexagonViews)
    //         {
    //             // Ignore not walkable adjacent CellHexagonViews.
    //             if (adjacentCellHexagonView.isObstacle)
    //             {
    //                 continue;
    //             }
    //
    //             // Ignore the CellHexagonView if it's already in the closed list.
    //             if (closedPathCellHexagonViews.Contains(adjacentCellHexagonView))
    //             {
    //                 continue;
    //             }
    //
    //             // If it's not in the open list - add it and compute G and H.
    //             if (!(openPathCellHexagonViews.Contains(adjacentCellHexagonView)))
    //             {
    //                 adjacentCellHexagonView.g = g;
    //                 adjacentCellHexagonView.h = GetEstimatedPathCost(adjacentCellHexagonView.position, endPoint.position);
    //                 openPathCellHexagonViews.Add(adjacentCellHexagonView);
    //             }
    //             // Otherwise check if using current G we can get a lower value of F, if so update it's value.
    //             else if (adjacentCellHexagonView.F > g + adjacentCellHexagonView.h)
    //             {
    //                 adjacentCellHexagonView.g = g;
    //             }
    //         }
    //     }
    //
    //     List<CellHexagonView> finalPathCellHexagonViews = new List<CellHexagonView>();
    //
    //     // Backtracking - setting the final path.
    //     if (closedPathCellHexagonViews.Contains(endPoint))
    //     {
    //         currentCellHexagonView = endPoint;
    //         finalPathCellHexagonViews.Add(currentCellHexagonView);
    //
    //         for (int i = endPoint.g - 1; i >= 0; i--)
    //         {
    //             currentCellHexagonView = closedPathCellHexagonViews.Find(x => x.g == i && currentCellHexagonView.adjacentCellHexagonViews.Contains(x));
    //             finalPathCellHexagonViews.Add(currentCellHexagonView);
    //         }
    //
    //         finalPathCellHexagonViews.Reverse();
    //     }
    //
    //     return finalPathCellHexagonViews;
    // }
    //
    //
    // protected static int GetEstimatedPathCost(Vector3Int startPosition, Vector3Int targetPosition)
    // {
    //     return Mathf.Max(Mathf.Abs(startPosition.z - targetPosition.z), Mathf.Max(Mathf.Abs(startPosition.x - targetPosition.x), Mathf.Abs(startPosition.y - targetPosition.y)));
    // }
