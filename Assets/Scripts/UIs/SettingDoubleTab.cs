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
                    // ���� Ŭ�� ������
                    singleTap = false;
                    lastTapTime = 0;
                    SceneManager.LoadScene("SettingScene"); // ���ϴ� �� �̸����� ����
                }
                else
                {
                    // Ŭ�� ������
                    singleTap = true;
                    lastTapTime = Time.time;
                }
            }
        }

        // ���� Ŭ�� ���� ��, ���� �����ӿ��� Ŭ�� �÷��� �ʱ�ȭ
        if (singleTap && Time.time - lastTapTime > doubleTapThreshold)
        {
            singleTap = false;
        }
    }
}