using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavySnow : Disaster, IDisaster
{
    [SerializeField]
    private GameObject warningPanel;
    public GameObject WarningPanel => warningPanel;
    private bool isFall;

    private void Update()
    {
        if (!isFall)
        {
            return;
        }

        GameManager.instance.HorizontalMovement.MovementSpeed = Mathf.Min(GameManager.instance.HorizontalMovement.MovementSpeed, GameManager.instance.HorizontalMovement.WalkSpeed * 0.5f);
    }

    public override void StopDisaster()
    {
        isFall = false;
        GameManager.instance.HorizontalMovement.MovementSpeed = GameManager.instance.HorizontalMovement.WalkSpeed;
    }

    public override IEnumerator PlayDisaster()
    {
        yield return StartCoroutine((this as IDisaster).BlinkWarningPanel());

        isFall = true;

        yield return new WaitForSeconds(7.5f);

        StopDisaster();
    }
}
