using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavySnow : Disaster
{
    public override IEnumerator PlayDisaster()
    {
        yield return null;

        Debug.Log("����");
    }

    public override void StopDisaster()
    {

    }
}
