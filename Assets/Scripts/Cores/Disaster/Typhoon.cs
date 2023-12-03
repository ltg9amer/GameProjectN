using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Typhoon : Disaster
{
    [SerializeField]
    private TyphoonObject typhoonObject;

    public override IEnumerator PlayDisaster()
    {
        typhoonObject.transform.position = GameManager.instance.CorgiCharacter.transform.position + (Vector3)(Random.insideUnitCircle * 10f);

        if (typhoonObject.transform.position.y <= 0f)
        {
            typhoonObject.transform.position += Vector3.up * 10f;
        }

        typhoonObject.gameObject.SetActive(true);

        yield return StartCoroutine(typhoonObject.PlayDisaster());
    }

    public override void StopDisaster()
    {

    }
}
