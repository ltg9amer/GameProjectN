using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameCheck : MonoBehaviour
{
    [SerializeField]
    private AudioSource corgiSoundEffect;
    [SerializeField]
    private AudioSource skipSoundEffect;
    [SerializeField]
    private AudioSource failSoundEffect;
    [SerializeField]
    private TMP_InputField nameInput;
    [SerializeField]
    private MMLoadScene loadScene;

    public void CheckName()
    {
        if (nameInput.text != "")
        {
            if (nameInput.text.ToLower() == "corgi")
            {
                StartCoroutine(CorgiBarkSplit());
            }
            else if (nameInput.text.ToLower() == "skip")
            {
                skipSoundEffect.Play();
            }
            else
            {
                GameManager.instance.currentUserData.UserName = nameInput.text;

                GameManager.instance.rankingSaveList.ranking.Add(GameManager.instance.currentUserData);
                GameManager.instance.rankingSaveList.RankingSort();
                PlayerPrefs.SetString("Ranking", JsonUtility.ToJson(GameManager.instance.rankingSaveList));
                PlayerPrefs.DeleteKey("CurrentUser");
                AudioManager.instance.SetBackgroundMusicVolume(1f);
                AudioManager.instance.SetSoundEffectsVolume(1f);
                PlayerPrefs.Save();
                loadScene.LoadScene();
            }
        }
        else
        {
            failSoundEffect.time = 0.75f;

            failSoundEffect.Play();
        }
    }

    private IEnumerator CorgiBarkSplit()
    {
        corgiSoundEffect.time = 0.75f;

        corgiSoundEffect.Play();

        yield return new WaitForSecondsRealtime(0.5f);

        corgiSoundEffect.Stop();
    }
}
