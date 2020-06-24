using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBase : MonoBehaviour, ICell
{
    public const string CELL_TAG = "Cell";
    
    private CellBase[] NearestCells;
    
    public enum State
    {
        Free,
        Obstacle,
        StartPoint,
        DestinationPoint
    }

    private State _state = State.Free;
    private CellNode _node;

    [SerializeField] private MeshRenderer ObjectRenderer;
    [SerializeField] private Collider ObjectCollider;

    
    private void Start()
    {
        GlobalManager.Instance.OnGameReset += OnGameReset;
        RefreshStatus();
        RegisterNeighboring();
    }

    private void OnDestroy()
    {
        if (GlobalManager.Instance != null)
            GlobalManager.Instance.OnGameReset -= OnGameReset;
    }

    private void OnGameReset()
    {
        RefreshStatus();
    }

    public void OnSelected()
    {
        switch (UIManager.Instance.pathContructorType)
        {
            case UIManager.PathContructorType.StartPoint:
                SetState(State.StartPoint);
                GlobalManager.Instance.SetStartPoint(this);
                break;
            case UIManager.PathContructorType.DestinationPoint:
                SetState(State.DestinationPoint);
                GlobalManager.Instance.SetDestinationPoint(this);
                break;
            case UIManager.PathContructorType.Obstacles:
                SetState((_state != State.Obstacle) ? State.Obstacle : State.Free);
                break;
        }
        RefreshStatus();
    }

    public void SetFree()
    {
        SetState(State.Free);
        RefreshStatus();
    }

    public Vector3 GetPosition()
    {
        if (transform != null) return transform.position;
        else return Vector3.zero;
    }

    private void RefreshStatus()
    {
        switch (_state)
        {
            case State.Free:
                ObjectRenderer.material.color = Color.grey;
                break;
            case State.Obstacle:
                ObjectRenderer.material.color = Color.red;
                break;
            case State.StartPoint:
                ObjectRenderer.material.color = Color.green;
                break;
            case State.DestinationPoint:
                ObjectRenderer.material.color = Color.blue;
                break;
        }
    }

    /// <summary>
    /// no doubt this is not a best approach
    /// </summary>
    private void RegisterNeighboring()
    {
        for (int i = 0; i < 6; i++)
        {
            Collider[] collResults = new Collider[7];
            if (Physics.OverlapSphereNonAlloc((transform.position - transform.up * 0.2f), 0.25f, collResults) > 0)
            {
                List<CellBase> objFound = new List<CellBase>();
                foreach (var item in collResults)
                {
                    if (item != null && item != ObjectCollider && item.transform.parent != null)
                    {
                        CellBase _cellBase = item.transform.parent.GetComponent<CellBase>();
                        if (_cellBase != null)
                            objFound.Add(_cellBase);
                    }
                }
                
                NearestCells = new CellBase[objFound.Count];
                objFound.CopyTo(NearestCells);
            }
        }
    }
    
    //public method

    public CellBase[] GetNearestCells()
    {
        return NearestCells;
    }

    public void SetPartOfPath()
    {
        ObjectRenderer.material.color = Color.white;
    }

    public State GetState()
    {
        return _state;
    }

    public void SetState(State value)
    {
        _state = value;
    }

    public CellNode GetNode()
    {
        return _node;
    }

    public void SetNode(CellNode value)
    {
        _node = value;
    }

    public static bool CompareStates(State a, State b)
    {
        return a == b;
    }
}
