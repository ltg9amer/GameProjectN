using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidalWave : Disaster
{
    [SerializeField]
    private TidalWaveObject tidalWaveObject;
    [SerializeField]
    private float xOffset;
    private Vector3 originalScale;

    public override IEnumerator PlayDisaster()
    {
        originalScale = tidalWaveObject.transform.localScale;
        tidalWaveObject.transform.position = GameManager.instance.CorgiCharacter.transform.position + new Vector3(xOffset, 0f);
        tidalWaveObject.transform.localScale *= 1f + Random.Range(0, 4) * 0.5f;

        tidalWaveObject.gameObject.SetActive(true);

        yield return StartCoroutine(tidalWaveObject.PlayDisaster());

        StopDisaster();
    }

    public override void StopDisaster()
    {
        tidalWaveObject.transform.localScale = originalScale;
    }
}
