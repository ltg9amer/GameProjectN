using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidalWave : Disaster
{
    public override IEnumerator PlayDisaster()
    {
        yield return null;

        Debug.Log("«ÿ¿œ");
    }

    public override void StopDisaster()
    {

    }
}
