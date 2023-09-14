using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoubleTap : MonoBehaviour
{
    float lastTapTime = 0;
    float doubleTapThreshold = 0.3f;

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (Time.time - lastTapTime <= doubleTapThreshold)
                {
                    lastTapTime = 0;
                    // ���� ��ġ ������ �� PlayScene���� �̵��մϴ�.
                    SceneManager.LoadScene("PlayScene");
                }
                else
                {
                    lastTapTime = Time.time;
                }
            }
        }
    }
}
