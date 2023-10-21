using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public RectTransform jumpButtonInPopup;
    public RectTransform joystickInPopup;
    public RectTransform jumpButton;
    public RectTransform joystick;
    public Image on;
    public Image off;
    public bool isToggleOn;

    public void On()
    {
        off.gameObject.SetActive(false);
        on.gameObject.SetActive(true);

        if (!isToggleOn)
        {
            isToggleOn = true;

            ButtonSwap();
        }

        GameManager.instance.ControlReversed = isToggleOn;
    }

    public void Off()
    {
        on.gameObject.SetActive(false);
        off.gameObject.SetActive(true);

        if (isToggleOn)
        {
            isToggleOn = false;

            ButtonSwap();
        }

        GameManager.instance.ControlReversed = isToggleOn;
    }

    private void ButtonSwap()
    {
        Vector3 tempAnchoredPosition = jumpButtonInPopup.anchoredPosition;
        jumpButtonInPopup.anchoredPosition = joystickInPopup.anchoredPosition;
        joystickInPopup.anchoredPosition = tempAnchoredPosition;

        if (GameManager.instance.IsPlay)
        {
            Vector2 tempAnchorMax = jumpButton.anchorMax;
            Vector2 tempAnchorMin = jumpButton.anchorMin;
            Vector2 tempPivot = jumpButton.pivot;
            tempAnchoredPosition = jumpButton.anchoredPosition;
            jumpButton.anchorMax = joystick.anchorMax;
            jumpButton.anchorMin = joystick.anchorMin;
            jumpButton.pivot = joystick.pivot;
            jumpButton.anchoredPosition = joystick.anchoredPosition;
            joystick.anchorMax = tempAnchorMax;
            joystick.anchorMin = tempAnchorMin;
            joystick.pivot = tempPivot;
            joystick.anchoredPosition = tempAnchoredPosition;

            joystick.transform.GetChild(0).GetComponent<MMTouchJoystick>().SetNeutralPosition();
        }
    }
}
