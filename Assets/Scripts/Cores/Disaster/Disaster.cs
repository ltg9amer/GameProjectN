using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Disaster : MonoBehaviour
{
    public Action onPlay;
    public List<Vector2> warningPanelRectangles = new List<Vector2>();
    public GameObject warningPanel;
    private List<GameObject> warningPanels = new List<GameObject>();

    public virtual void Awake()
    {
        onPlay += PlayDisaster;

        for (int i = 0; i < warningPanelRectangles.Count; i++)
        {
            warningPanels.Add(Instantiate(warningPanel));
            warningPanels[warningPanels.Count - 1].SetActive(false);
        }
    }

    public abstract void PlayDisaster();

    public abstract void SetWarningPanelRectangle();

    public abstract void StopDisaster();
}
