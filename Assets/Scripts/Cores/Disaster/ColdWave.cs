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
    private int touchCount;
    private float coldWaveTimer;

    private void Update()
    {
        if (!frozen)
        {
            return;
        }

        coldWaveTimer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began))
        {
            touchCount++;
        }

        if (touchCount >= requireTouchCount)
        {
            touchCount = 0;

            StopDisaster();
        }
        else if (coldWaveTimer <= 0f)
        {
            StopDisaster();
            GameManager.instance.CorgiCharacter.CharacterHealth.Kill();
            GameManager.instance.CorgiCharacter._animator.SetTrigger("Death");
        }
    }

    public override IEnumerator PlayDisaster()
    {
        if (!horizontalMovement)
        {
            horizontalMovement = GameManager.instance.CorgiCharacter.FindAbility<CharacterHorizontalMovement>();
        }

        horizontalMovement.PermitAbility(false);
        GameManager.instance.CharacterJump.PermitAbility(false);

        GameManager.instance.CorgiCharacter._animator.enabled = false;
        frozen = true;
        touchCount = 0;
        coldWaveTimer = coldWaveLimitTime;

        yield return null;
    }

    public override void StopDisaster()
    {
        horizontalMovement.PermitAbility(true);
        GameManager.instance.CharacterJump.PermitAbility(true);

        GameManager.instance.CorgiCharacter._animator.enabled = true;
        frozen = false;
    }
}
