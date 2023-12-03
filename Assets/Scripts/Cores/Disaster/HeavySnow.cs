using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavySnow : Disaster, IDisaster
{
    [SerializeField]
    private GameObject warningPanel;
    public GameObject WarningPanel => warningPanel;
    [SerializeField]
    private ParticleSystem heavySnowEffect;
    [SerializeField]
    private Vector3 offset;
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
        heavySnowEffect.Stop();

        isFall = false;
        GameManager.instance.HorizontalMovement.MovementSpeed = GameManager.instance.HorizontalMovement.WalkSpeed;

        StartCoroutine(TidalWaveCoroutine());
    }

    public override IEnumerator PlayDisaster()
    {
        yield return StartCoroutine((this as IDisaster).BlinkWarningPanel());

        isFall = true;
        heavySnowEffect.transform.position = GameManager.instance.CorgiCharacter.transform.position + offset;

        heavySnowEffect.Play();

        yield return new WaitForSeconds(7.5f);

        StopDisaster();
    }

    private IEnumerator TidalWaveCoroutine()
    {
        yield return new WaitForSeconds(DisasterManager.instance.TidalWaveDelay);

        if (Random.value <= 0.5f)
        {
            DisasterManager.instance.DisasterDictionary["TidalWave"]?.onPlay.Invoke();
        }
    }
}
