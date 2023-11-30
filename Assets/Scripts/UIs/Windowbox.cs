using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windowbox : MonoBehaviour
{
    [SerializeField]
    private float fixedAspectRatio;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;

        if (currentAspectRatio == fixedAspectRatio)
        {
            mainCamera.rect = new Rect(0f, 0f, 1f, 1f);

            return;
        }
        else if (currentAspectRatio > fixedAspectRatio)
        {
            float width = fixedAspectRatio / currentAspectRatio;
            float x = (1f - width) / 2f;
            mainCamera.rect = new Rect(x, 0f, width, 1f);
        }
        else if (currentAspectRatio < fixedAspectRatio)
        {
            float height = currentAspectRatio / fixedAspectRatio;
            float y = (1f - height) / 2f;
            mainCamera.rect = new Rect(0f, y, 1f, height);
        }
    }

    private void OnPreCull()
    {
        GL.Clear(true, true, Color.black);
    }
}
