using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellNode
{
    public ICell ParentNode;
    public int GridX, GridY;
    public int igCost, ihCost;
    public int FCost { get { return igCost + ihCost; } }
}
