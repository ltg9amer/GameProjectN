using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public RectTransform jumpButton;
    public RectTransform joystick;
	public Image on;
	public Image off;
    public bool isToggleOff;

	public void On()
    {
        off.gameObject.SetActive(false);
        on.gameObject.SetActive(true);

        if (isToggleOff)
        {
            isToggleOff = false;

            ButtonSwap();
        }

        GameManager.instance.ControlReversed = !isToggleOff;
    }

    public void Off()
    {
        on.gameObject.SetActive(false);
        off.gameObject.SetActive(true);

        if (!isToggleOff)
        {
            isToggleOff = true;

            ButtonSwap();
        }

        GameManager.instance.ControlReversed = !isToggleOff;
    }

    private void ButtonSwap()
    {
        Vector3 tempRect = jumpButton.anchoredPosition;
        jumpButton.anchoredPosition = joystick.anchoredPosition;
        joystick.anchoredPosition = tempRect;
    }
}
