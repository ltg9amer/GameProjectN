using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hail : Disaster
{
    [SerializeField]
    private GameObject hailPrefab;

    public override void PlayDisaster()
    {
        //¿ì¹Ú ¶³±¸±â
        StartCoroutine(TyphoonCoroutine());
    }

    private IEnumerator TyphoonCoroutine()
    {
        yield return new WaitForSeconds(DisasterManager.instance.TyphoonDelay);

        if (Random.Range(0, 4) == 0)
        {
            DisasterManager.instance.Disaster = DisasterManager.instance.DisasterDictionary["Typhoon"];
        }
    }
}
