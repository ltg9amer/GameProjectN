using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTextBlink : MonoBehaviour
{
    [SerializeField]
    private float blinkCoolTime = 0.5f;
    private Image text;

    private void Start()
    {
        text = GetComponent<Image>();

        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            yield return new WaitForSeconds(blinkCoolTime);

            text.enabled = false;

            yield return new WaitForSeconds(blinkCoolTime);

            text.enabled = true;
        }
    }
}
