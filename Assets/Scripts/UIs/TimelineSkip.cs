using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TimelineSkip : MonoBehaviour
{
    [SerializeField]
    private List<PlayableDirector> director;
    [SerializeField]
    private List<float> skipTimeFrame;
    private Image skipButton;

    private void Awake()
    {
        skipButton = transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        if (!skipButton.enabled && (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)))
        {
            for (int i = 0; i < director.Count; ++i)
            {
                if (director[i].state == PlayState.Playing)
                {
                    skipButton.enabled = true;

                    skipButton.DOFade(1f, 1f)
                        .OnComplete(() => StartCoroutine(SkipButtonLife()));
                }
            }
        }
    }

    public async void Skip()
    {
        for (int i = 0; i < director.Count; ++i)
        {
            await Task.Delay(1);

            if (director[i].state == PlayState.Playing)
            {
                director[i].time = skipTimeFrame[i] / 60f;
            }
        }
    }

    private IEnumerator SkipButtonLife()
    {
        yield return new WaitForSeconds(5f);

        skipButton.DOFade(0f, 1f)
            .OnComplete(() => skipButton.enabled = false);
    }
}
