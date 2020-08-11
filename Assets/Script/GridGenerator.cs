using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private GameObject PrefabCell;

    [SerializeField] private int Columns;
    [SerializeField] private int Rows;
    
    private void Start()
    {
        for (int i = 0; i < Columns; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                GameObject _newCell = Instantiate(PrefabCell, Vector3.zero, Quaternion.identity);
                if (j % 2 == 0)
                    _newCell.transform.position = new Vector3(1.4f * i,0,1.4f * j);
                else
                    _newCell.transform.position = new Vector3(0.3f * i,0,-0.175f * j);
            }   
        }
    }
}
