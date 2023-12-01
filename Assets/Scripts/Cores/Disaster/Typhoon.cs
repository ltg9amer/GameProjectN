using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Typhoon : Disaster
{
    [SerializeField]
    private List<TyphoonObject> typhoonObjects;

    public override IEnumerator PlayDisaster()
    {
        for (int i = 0; i < Random.Range(1, typhoonObjects.Count + 1); ++i)
        {
            typhoonObjects[i].transform.position = GameManager.instance.CorgiCharacter.transform.position + (Vector3)(Random.insideUnitCircle * 10f);

            if (typhoonObjects[i].transform.position.y <= 0f)
            {
                typhoonObjects[i].transform.position += Vector3.up * 10f;
            }

            typhoonObjects[i].gameObject.SetActive(true);

            yield return StartCoroutine(typhoonObjects[i].PlayDisaster());
        }

        StopDisaster();
    }

    public override void StopDisaster()
    {

    }
}
