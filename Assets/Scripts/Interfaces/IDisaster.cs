using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDisaster
{
    public AudioSource AlertSound { get; }
    public GameObject WarningPanel { get; }

    public virtual IEnumerator BlinkWarningPanel()
    {
        // ±ôºýÀÌ´Â °æ°í Ç¥½Ã
        /*for (int i = 0; i < 3; ++i)
        {
            warningPanel.SetActive(true);

            yield return new WaitForSeconds(0.75f);

            warningPanel.SetActive(false);

            yield return new WaitForSeconds(0.75f);
        }*/

        // ÂªÀº °æ°í Ç¥½Ã
        AlertSound.Play();
        WarningPanel.SetActive(true);

        yield return new WaitForSeconds(0.75f);

        WarningPanel.SetActive(false);
    }
}
