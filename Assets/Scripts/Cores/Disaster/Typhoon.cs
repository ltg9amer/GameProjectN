using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Typhoon : Disaster
{
    public override void PlayDisaster()
    {
        //ÅÂÇ³ ¼ÒÈ¯
    }

    public override void StopDisaster()
    {
        StartCoroutine(TidalWaveCoroutine());
    }

    private IEnumerator TidalWaveCoroutine()
    {
        yield return new WaitForSeconds(DisasterManager.instance.TidalWaveDelay);

        DisasterManager.instance.Disaster = DisasterManager.instance.DisasterDictionary["TidalWave"];
    }
}
