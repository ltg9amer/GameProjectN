using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingDoubleTap : MonoBehaviour
{
    float lastTapTime = 0;
    float doubleTapThreshold = 0.3f;
    bool singleTap = false;

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (Time.time - lastTapTime <= doubleTapThreshold)
                {
                    // 더블 클릭 감지됨
                    singleTap = false;
                    lastTapTime = 0;
                    SceneManager.LoadScene("SettingScene"); // 원하는 씬 이름으로 변경
                }
                else
                {
                    // 클릭 감지됨
                    singleTap = true;
                    lastTapTime = Time.time;
                }
            }
        }

        // 더블 클릭 감지 후, 다음 프레임에서 클릭 플래그 초기화
        if (singleTap && Time.time - lastTapTime > doubleTapThreshold)
        {
            singleTap = false;
        }
    }
}