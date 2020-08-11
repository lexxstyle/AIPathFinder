using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private RaycastHit[] hits = new RaycastHit[3];
    private Camera MainCamera;

    private void Start()
    {
        MainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown (0)) 
        { 
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition); 
            if (Physics.RaycastNonAlloc (ray, hits,100.0f) > 0) 
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit hit = hits[i];
                    if (hit.collider != null && hit.transform.CompareTag(CellBase.CELL_TAG))
                    {
                        CellBase cell = hit.transform.parent.GetComponent<CellBase>();
                        if (cell != null)
                        {
                            cell.OnSelected();
                            return;
                        }
                    }
                }
            }
        }
    }
}
