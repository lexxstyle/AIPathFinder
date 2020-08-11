using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMap
{
    List<ICell> GetResultPath();
    void SetResultPath(List<ICell> value);
}
