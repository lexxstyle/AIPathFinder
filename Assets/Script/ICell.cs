using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;

public interface ICell
{
    CellBase.State GetState();
    void SetState(CellBase.State value);

    CellNode GetNode();
    void SetNode(CellNode value);
    
    CellBase[] GetNearestCells();

    void SetFree();
    void SetPartOfPath();
    
    Vector3 GetPosition();
}
