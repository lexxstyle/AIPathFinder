﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourSingleton<T> : MonoBehaviour where T: BehaviourSingleton<T>
{
    public static T Instance { get; protected set; }
     
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            throw new System.Exception("Instance of this singleton already exists.");
        }
        else
        {
            Instance = (T)this;
        }
    }

    private void OnDestroy() { Instance = null; }
}