using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdWave : Disaster
{
    [SerializeField]
    private int requireTouchCount = 30;
    [SerializeField]
    private float coldWaveLimitTime = 3f;
    private CharacterHorizontalMovement horizontalMovement;
    private bool frozen = false;
    private bool isSurvived = false;
    private int touchCount;
    private float coldWaveTimer;

    private void Update()
    {
        if (!frozen)
        {
            return;
        }

        coldWaveTimer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            touchCount++;
        }

        if (touchCount >= requireTouchCount)
        {
            isSurvived = true;
            touchCount = 0;
        }

        if (isSurvived)
        {
            StopDisaster();
        }
        else if (coldWaveTimer <= 0f)
        {
            StopDisaster();
            GameManager.instance.CorgiCharacter.CharacterHealth.Kill();
            GameManager.instance.CorgiCharacter._animator.SetTrigger("Death");
        }
    }

    public override void PlayDisaster()
    {
        if (!horizontalMovement)
        {
            horizontalMovement = GameManager.instance.CorgiCharacter.FindAbility<CharacterHorizontalMovement>();
        }

        horizontalMovement.PermitAbility(false);
        GameManager.instance.CharacterJump.PermitAbility(false);

        GameManager.instance.CorgiCharacter._animator.enabled = false;
        frozen = true;
        isSurvived = false;
        touchCount = 0;
        coldWaveTimer = coldWaveLimitTime;
    }

    public override void SetWarningPanelRectangle()
    {

    }

    public override void StopDisaster()
    {
        horizontalMovement.PermitAbility(true);
        GameManager.instance.CharacterJump.PermitAbility(true);

        GameManager.instance.CorgiCharacter._animator.enabled = true;
        frozen = false;
    }
}
