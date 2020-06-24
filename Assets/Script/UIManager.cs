using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : BehaviourSingleton<UIManager>
{
    [SerializeField] private UIButton Btn_StartPoint, Btn_DestPoint, Btn_Obstacles;
    [SerializeField] private Text ResultInfo;
    
    public enum PathContructorType
    {
        StartPoint,
        DestinationPoint,
        Obstacles
    }

    public PathContructorType pathContructorType = PathContructorType.Obstacles;

    private void Start()
    {
        OnSelectedButton(3);
    }

    public void OnSelectedButton(int id)
    {
       DeselectAllButtons();
        switch (id)
        {
            case 1:
                Btn_StartPoint.interactable = false;
                pathContructorType = PathContructorType.StartPoint;
                break;
            case 2:
                Btn_DestPoint.interactable = false;
                pathContructorType = PathContructorType.DestinationPoint;
                break;
            case 3:
                Btn_Obstacles.interactable = false;
                pathContructorType = PathContructorType.Obstacles;
                break;
        }
    }

    public void WriteResultInfo(string message)
    {
        ResultInfo.text = message;
    }

    private void DeselectAllButtons()
    {
        Btn_StartPoint.interactable = true;
        Btn_DestPoint.interactable = true;
        Btn_Obstacles.interactable = true;
    }
}
