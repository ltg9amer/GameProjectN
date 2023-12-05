using DG.Tweening;
using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyphoonObject : MonoBehaviour, IDisaster
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
    private float blowSpeed;
    private SpriteRenderer spriteRenderer;
    private Sequence sequence;
    private bool isBlowing;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void Update()
    {
        if (!isBlowing)
        {
            return;
        }

        Vector3 destination = GameManager.instance.CorgiCharacter.transform.position + Vector3.down * (transform.localScale.y - transform.localScale.y * 0.2f - 1f);
        destination.y = Mathf.Clamp(destination.y, 0f, float.MaxValue);
        transform.position += (destination - transform.position) * blowSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isBlowing)
        {
            return;
        }

        if (collision.TryGetComponent(out Character character))
        {
            GameManager.instance.Controller.GravityActive(false);

            GameManager.instance.CharacterJump.AbilityPermitted = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isBlowing)
        {
            return;
        }

        if (collision.TryGetComponent(out Character character))
        {
            GameManager.instance.HorizontalMovement.MovementSpeed = Mathf.Min(GameManager.instance.HorizontalMovement.MovementSpeed, GameManager.instance.HorizontalMovement.WalkSpeed * 0.1f);
            
            if (sequence == null || sequence.IsComplete())
            {
                sequence = DOTween.Sequence()
                    .SetAutoKill(false)
                    .Append(character.transform.DOJump(transform.position + Vector3.up * (transform.localScale.y - transform.localScale.y * 0.2f), 2f, 1, 2f))
                    .OnComplete(() =>
                    {
                        character.transform.position = new Vector3(character.transform.position.x, transform.position.y + (transform.localScale.y - transform.localScale.y * 0.2f));
                    });
            }
        }
    }

    public IEnumerator PlayDisaster()
    {
        yield return StartCoroutine((this as IDisaster).BlinkWarningPanel());

        spriteRenderer.enabled = isBlowing = true;

        yield return new WaitForSeconds(lifeTime);

        sequence.Complete();
        GameManager.instance.Controller.GravityActive(true);

        GameManager.instance.CharacterJump.AbilityPermitted = true;
        GameManager.instance.HorizontalMovement.MovementSpeed = GameManager.instance.HorizontalMovement.WalkSpeed;
        spriteRenderer.enabled = isBlowing = false;

        gameObject.SetActive(false);
    }
}
