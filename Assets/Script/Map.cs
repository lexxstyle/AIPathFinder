using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : BehaviourSingleton<Map>, IPathFinder
{
    [SerializeField] private Transform ParentForCells;
    [SerializeField] private GameObject PrefabCell;
    [SerializeField] private float width = 10;
    [SerializeField] private float height = 10;
    
    
    private void Start()
    {
        GenerateHexagonGrid();
    }
    private void GenerateHexagonGrid()
    {
        float offsetX = 0.38f;
        float offsetZ = 0.32f;
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float posX = x * offsetX;

                if (y % 2 == 1)
                {
                    posX += offsetX / 2;
                }

                GameObject _newCell = Instantiate(PrefabCell, new Vector3(posX, 0, y * offsetZ), Quaternion.identity);
                
                CellBase _cell = _newCell.GetComponent<CellBase>();
                CellNode _node = new CellNode();
                _node.GridX = x;
                _node.GridY = y;
                _cell.SetNode(_node);
                
                _newCell.transform.parent = ParentForCells;
            }   
        }
    }
    
    public IList<ICell> FindPathOnMap(ICell cellStart, ICell cellEnd)
    {
        if (cellStart == null || cellEnd == null)
            return null;
        
        List<ICell> OpenList = new List<ICell>();
        List<ICell> ClosedList = new List<ICell>();
        
        OpenList.Add(cellStart);

        while (OpenList.Count > 0)
        {
            ICell CurrentNode = OpenList[0]; 
            for (int i = 1; i < OpenList.Count; i++)
            {
                CellNode _target = OpenList[i].GetNode();
                CellNode _current = CurrentNode.GetNode();
                
                if (_target.FCost < _current.FCost || _target.FCost == _current.FCost && _target.ihCost < _current.ihCost) 
                {
                    CurrentNode = OpenList[i];
                }
            }

            OpenList.Remove(CurrentNode);
            ClosedList.Add(CurrentNode);

            if (CurrentNode == cellEnd)
            {
                return GetFinalPath(cellStart, cellEnd);
            }

            foreach (ICell NeighborNode in CurrentNode.GetNearestCells())
            {
                CellNode _target = NeighborNode.GetNode();
                CellNode _current = CurrentNode.GetNode();
                
                if (CellBase.CompareStates(NeighborNode.GetState(), CellBase.State.Obstacle) || 
                    ClosedList.Contains(NeighborNode)) 
                    continue;

                int MoveCost = _current.igCost + GetManhattenDistance(_current, _target);

                if (MoveCost < _target.igCost || !OpenList.Contains(NeighborNode))
                {
                    NeighborNode.GetNode().igCost = MoveCost; 
                    NeighborNode.GetNode().ihCost = GetManhattenDistance(_target, cellEnd.GetNode()); 
                    NeighborNode.GetNode().ParentNode = CurrentNode; 

                    if (!OpenList.Contains(NeighborNode)) 
                    {
                        OpenList.Add(NeighborNode);
                    }
                }
            }
        }

        return null;
    }
    private static IList<ICell> GetFinalPath(ICell a_StartingNode, ICell a_EndNode)
    {
        List<ICell> FinalPath = new List<ICell>();
        ICell CurrentNode = a_EndNode;

        while(CurrentNode != a_StartingNode)
        {
            FinalPath.Add(CurrentNode);
            CurrentNode = CurrentNode.GetNode().ParentNode;
        }

        FinalPath.Reverse();
        return FinalPath;
    }
    private static int GetManhattenDistance(CellNode a_nodeA, CellNode a_nodeB)
    {
        int x = Mathf.Abs(a_nodeA.GridX - a_nodeB.GridX);
        int y = Mathf.Abs(a_nodeA.GridY - a_nodeB.GridY);
        return x + y;
    }
}
