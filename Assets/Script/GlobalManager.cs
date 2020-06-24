using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GlobalManager : BehaviourSingleton<GlobalManager>, IPathPoints
{
    private Character _character => Character.Instance;
    
    [HideInInspector] public ICell StartPoint, DestinationPoint;

    public Action OnGameReset;


    public void StartGame()
    {
        OnGameReset?.Invoke();
        _character.SetStartPosition(StartPoint);

        IList<ICell> FinalPath = Map.Instance.FindPathOnMap(StartPoint, DestinationPoint);
        if (FinalPath != null)
        {
            UIManager.Instance.WriteResultInfo(FinalPath.Count + " Distance cells\n" + StartPoint.GetPosition() + " Start position\n" + DestinationPoint.GetPosition() + " Destination position");
            
            _character.GotToTarget(FinalPath);

            foreach (var item in FinalPath)
                if (item != DestinationPoint)
                    item.SetPartOfPath();
        }
    }

    public void SetStartPoint(CellBase point)
    {
        if (StartPoint != null && CellBase.CompareStates(StartPoint.GetState(), CellBase.State.StartPoint)) StartPoint.SetFree();
        StartPoint = point;
    }

    public void SetDestinationPoint(CellBase point)
    {
        if (DestinationPoint != null && CellBase.CompareStates(DestinationPoint.GetState(), CellBase.State.DestinationPoint)) DestinationPoint.SetFree();
        DestinationPoint = point;
    }
}
