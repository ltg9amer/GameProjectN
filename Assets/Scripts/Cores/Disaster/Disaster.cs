using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Disaster : MonoBehaviour
{
    private event Action onPlay;

    public virtual void Awake()
    {
        onPlay += PlayDisaster;
    }

    public abstract void PlayDisaster();
}
