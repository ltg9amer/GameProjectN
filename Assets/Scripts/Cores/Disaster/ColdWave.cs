using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdWave : Disaster
{
    [SerializeField]
    private float coldWaveLimitTime = 3f;
    private Animator animator;
    private CharacterHorizontalMovement horizontalMovement;
    private CharacterJump characterJump;
    private bool frozen = false;
    private float coldWaveTimer;

    private void Update()
    {
        if (!frozen)
        {
            return;
        }

        coldWaveTimer -= Time.deltaTime;

        if (coldWaveTimer <= 0f)
        {
            //ÄÚ±â Á×ÀÌ±â
            StopDisaster();
        }
    }

    public override void PlayDisaster()
    {
        if (!animator)
        {
            animator = GameManager.instance.CorgiCharacter.GetComponent<Animator>();
        }

        if (!horizontalMovement)
        {
            horizontalMovement = GameManager.instance.CorgiCharacter.GetComponent<CharacterHorizontalMovement>();
        }

        if (!characterJump)
        {
            characterJump = GameManager.instance.CorgiCharacter.GetComponent<CharacterJump>();
        }

        horizontalMovement.PermitAbility(false);
        characterJump.PermitAbility(false);

        animator.enabled = false;
        frozen = true;
        coldWaveTimer = coldWaveLimitTime;
    }

    public override void StopDisaster()
    {
        horizontalMovement.PermitAbility(true);
        characterJump.PermitAbility(true);

        animator.enabled = true;
        frozen = false;
    }
}
