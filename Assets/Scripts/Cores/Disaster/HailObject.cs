using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HailObject : MonoBehaviour, IDisaster
{
    [SerializeField]
    private GameObject warningPanel;
    public GameObject WarningPanel => warningPanel;
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private float fallSpeed;
    private bool isFall;

    private void Update()
    {
        if (!isFall)
        {
            return;
        }

        transform.position += (transform.position + new Vector3(-0.5f, -1f) - transform.position) * fallSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character))
        {
            character.CharacterHealth.Kill();
            GameManager.instance.CorgiCharacter._animator.SetTrigger("Death");
        }
    }

    public IEnumerator PlayDisaster()
    {
        yield return StartCoroutine((this as IDisaster).BlinkWarningPanel());

        isFall = true;

        yield return new WaitForSeconds(lifeTime);

        isFall = false;

        gameObject.SetActive(false);
    }
}
