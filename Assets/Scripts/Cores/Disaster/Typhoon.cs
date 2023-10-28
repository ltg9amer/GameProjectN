using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Typhoon : Disaster
{
    public override void PlayDisaster()
    {
        Debug.Log("ลยวณ");
    }

    public override void SetWarningPanelRectangle()
    {

    }

    public override void StopDisaster()
    {
        StartCoroutine(TidalWaveCoroutine());
    }

    private IEnumerator TidalWaveCoroutine()
    {
        yield return new WaitForSeconds(DisasterManager.instance.TidalWaveDelay);

        if (Random.value < 0.5f)
        {
            DisasterManager.instance.DisasterDictionary["TidalWave"]?.onPlay.Invoke();
        }
    }
}
