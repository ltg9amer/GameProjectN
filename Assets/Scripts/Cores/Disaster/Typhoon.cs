using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Typhoon : Disaster
{
    public override void PlayDisaster()
    {
        //��ǳ ��ȯ
    }

    private IEnumerator TidalWaveCoroutine()
    {
        yield return new WaitForSeconds(DisasterManager.instance.TidalWaveDelay);

        DisasterManager.instance.Disaster = DisasterManager.instance.DisasterDictionary["TidalWave"];
    }
}