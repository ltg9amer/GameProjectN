using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hail : Disaster
{
    [SerializeField]
    private List<HailObject> hailObjects;
    [SerializeField]
    private float minimumXOffset, maximumXOffset, yOffset;

    public override IEnumerator PlayDisaster()
    {
        int hailCount = Random.Range(1, hailObjects.Count + 1);

        for (int i = 0; i < hailCount; ++i)
        {
            hailObjects[i].transform.position = new Vector3(Random.Range(GameManager.instance.CorgiCharacter.transform.position.x + minimumXOffset, GameManager.instance.CorgiCharacter.transform.position.x + maximumXOffset), yOffset);
            hailObjects[i].transform.localScale = Vector3.one * Random.Range(1, 5);

            hailObjects[i].gameObject.SetActive(true);
            StartCoroutine(hailObjects[i].PlayDisaster());

            yield return new WaitForSeconds(Random.Range(0.25f, 0.75f));
        }

        StartCoroutine(TyphoonCoroutine());
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
