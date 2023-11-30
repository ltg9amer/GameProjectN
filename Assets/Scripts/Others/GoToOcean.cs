using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoToOcean : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onTriggeredEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character))
        {
            onTriggeredEvent?.Invoke();
        }
    }
}
