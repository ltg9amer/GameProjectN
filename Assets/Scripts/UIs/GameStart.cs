using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    public UnityEvent loadSceneEvent;
    public PauseMenu pauseMenu;

    private void Update()
    {
        if ((Input.anyKeyDown && !IsPointerOverUIObject()) || (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && !IsTouchOverUIObject()))
        {
            loadSceneEvent?.Invoke();
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        Button dumpButton;

        return results[0].gameObject.TryGetComponent(out dumpButton) | pauseMenu.PausePanel.activeInHierarchy;
    }

    private bool IsTouchOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.GetTouch(0).position.x, Input.touches[0].position.y);
        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        Button dumpButton;

        return results[0].gameObject.TryGetComponent(out dumpButton) | pauseMenu.PausePanel.activeInHierarchy;
    }
}
