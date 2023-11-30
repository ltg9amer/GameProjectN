using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Disaster : MonoBehaviour
{
    public Action onPlay;

    public virtual void Awake()
    {
        onPlay += StartDisaster;
    }

    public abstract IEnumerator PlayDisaster();

    public abstract void StopDisaster();

    private void StartDisaster()
    {
        StartCoroutine(PlayDisaster());
    }
}
