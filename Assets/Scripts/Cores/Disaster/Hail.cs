using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hail : Disaster
{
    [SerializeField]
    private GameObject hailPrefab;

    public override void PlayDisaster()
    {
        Debug.Log("¿ì¹Ú");
        StartCoroutine(TyphoonCoroutine());
    }

    public override void SetWarningPanelRectangle()
    {

    }

    public override void StopDisaster()
    {

    }

    private IEnumerator TyphoonCoroutine()
    {
        yield return new WaitForSeconds(DisasterManager.instance.TyphoonDelay);

        if (Random.value < 0.25f)
        {
            DisasterManager.instance.DisasterDictionary["Typhoon"]?.onPlay.Invoke();
        }
    }
}
