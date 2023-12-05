using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidalWaveObject : MonoBehaviour, IDisaster
{
    [SerializeField]
    private AudioSource alertSound;
    public AudioSource AlertSound => alertSound;
    [SerializeField]
    private GameObject warningPanel;
    public GameObject WarningPanel => warningPanel;
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

        transform.position += Vector3.left * sweepSpeed * Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character))
        {
            GameManager.instance.HorizontalMovement.MovementSpeed = Mathf.Min(GameManager.instance.HorizontalMovement.MovementSpeed, GameManager.instance.HorizontalMovement.WalkSpeed * 0.5f);
            character.transform.position += Vector3.left * sweepSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character))
        {
            GameManager.instance.HorizontalMovement.MovementSpeed = GameManager.instance.HorizontalMovement.WalkSpeed;
        }
    }

    public IEnumerator PlayDisaster()
    {
        yield return StartCoroutine((this as IDisaster).BlinkWarningPanel());

        spriteRenderer.enabled = isSweep = true;

        yield return new WaitForSeconds(lifeTime);

        GameManager.instance.Controller.GravityActive(true);

        GameManager.instance.CharacterJump.AbilityPermitted = true;
        GameManager.instance.HorizontalMovement.MovementSpeed = GameManager.instance.HorizontalMovement.WalkSpeed;
        spriteRenderer.enabled = isSweep = false;

        gameObject.SetActive(false);
    }
}
