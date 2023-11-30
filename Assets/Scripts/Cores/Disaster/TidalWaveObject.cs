using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidalWaveObject : MonoBehaviour, IDisaster
{
    [SerializeField]
    private GameObject warningPanel;
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private float sweepSpeed;
    private SpriteRenderer spriteRenderer;
    private bool isSweep;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void Update()
    {
        if (!isSweep)
        {
            return;
        }

        GameManager.instance.HorizontalMovement.MovementSpeed = Mathf.Min(GameManager.instance.HorizontalMovement.MovementSpeed, GameManager.instance.HorizontalMovement.WalkSpeed * 0.5f);
        transform.position += Vector3.left * sweepSpeed * Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character))
        {
            character.transform.position += Vector3.left * sweepSpeed * Time.deltaTime;
        }
    }

    public IEnumerator BlinkWarningPanel()
    {
        // ±ôºýÀÌ´Â °æ°í Ç¥½Ã
        /*for (int i = 0; i < 3; ++i)
        {
            warningPanel.SetActive(true);

            yield return new WaitForSeconds(0.75f);

            warningPanel.SetActive(false);

            yield return new WaitForSeconds(0.75f);
        }*/

        // ÂªÀº °æ°í Ç¥½Ã
        warningPanel.SetActive(true);

        yield return new WaitForSeconds(0.75f);

        warningPanel.SetActive(false);
    }

    public IEnumerator PlayDisaster()
    {
        yield return StartCoroutine(BlinkWarningPanel());

        spriteRenderer.enabled = isSweep = true;

        yield return new WaitForSeconds(lifeTime);

        GameManager.instance.Controller.GravityActive(true);

        GameManager.instance.CharacterJump.AbilityPermitted = true;
        GameManager.instance.HorizontalMovement.MovementSpeed = GameManager.instance.HorizontalMovement.WalkSpeed;
        spriteRenderer.enabled = isSweep = false;

        gameObject.SetActive(false);
    }
}
