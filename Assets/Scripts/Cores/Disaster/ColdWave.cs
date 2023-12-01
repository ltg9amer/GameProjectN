using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdWave : Disaster, IDisaster
{
    [SerializeField]
    private GameObject warningPanel;
    public GameObject WarningPanel => warningPanel;
    [SerializeField]
    private int requireTouchCount = 30;
    [SerializeField]
    private float coldWaveLimitTime = 3f;
    private bool frozen;
    private int touchCount;

    private void Update()
    {
        if (!frozen)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began))
        {
            if (++touchCount >= requireTouchCount)
            {
                StopDisaster();
            }
        }
    }

    public override void StopDisaster()
    {
        GameManager.instance.HorizontalMovement.PermitAbility(true);
        GameManager.instance.CharacterJump.PermitAbility(true);

        GameManager.instance.CorgiCharacter._animator.enabled = true;
        frozen = false;
        touchCount = 0;
    }

    public override IEnumerator PlayDisaster()
    {
        yield return StartCoroutine((this as IDisaster).BlinkWarningPanel());

        GameManager.instance.HorizontalMovement.PermitAbility(false);
        GameManager.instance.CharacterJump.PermitAbility(false);

        GameManager.instance.CorgiCharacter._animator.enabled = false;
        frozen = true;

        yield return new WaitForSeconds(coldWaveLimitTime);

        if (frozen)
        {
            StopDisaster();
            GameManager.instance.CorgiCharacter.CharacterHealth.Kill();
            GameManager.instance.CorgiCharacter._animator.SetTrigger("Death");
        }
    }
}
