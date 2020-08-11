using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : BehaviourSingleton<Character>
{
    [SerializeField] private float MoveSpeed = 0.5f;
    private IList<ICell> PathPoints;
    
    private bool isMove = false;
    private int PathPointIndex = 0;
    private Vector3 targetPoint;
    
    public void SetStartPosition(ICell startPoint)
    {
        if (startPoint != null)
            transform.position = startPoint.GetPosition();
        else 
            Debug.Log("Setup StartPoint on game area");
    }

    public void GotToTarget(IList<ICell> pathPoints)
    {
        if (pathPoints == null || pathPoints.Count == 0)
            return;
        
        isMove = true;
        PathPointIndex = 0;
        PathPoints = pathPoints;
        targetPoint = PathPoints[PathPointIndex].GetPosition();
    }

    private void Update()
    {
        if (isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, MoveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPoint) < 0.2f)
            {
                PathPointIndex++;
                if (PathPointIndex >= PathPoints.Count)
                {
                    transform.position = targetPoint;
                    ResetMovement();
                }
                else
                {
                    targetPoint = PathPoints[PathPointIndex].GetPosition();
                }
            }
        }
    }

    private void ResetMovement()
    {
        isMove = false;
        PathPointIndex = 0;
    }
}
