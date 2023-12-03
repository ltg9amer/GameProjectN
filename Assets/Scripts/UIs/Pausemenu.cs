using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;

    public void Pause()
    {
        PausePanel.SetActive(true);

        if (GameManager.instance.IsPlay)
        {
            Time.timeScale = 0;
        }
    }

    public void Continue()
    {
        PausePanel.SetActive(false);

        if (GameManager.instance.IsPlay)
        {
            Time.timeScale = 1;
        }
    }
}
