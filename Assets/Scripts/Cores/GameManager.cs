using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    [SerializeField]
    private Switch controlToggleSwitch;
    [SerializeField]
    private Slider backgroundMusicSlider;
    [SerializeField]
    private Slider soundEffectsSlider;
    [SerializeField]
    private TextMeshProUGUI checkpointText;
    private bool isPlay;
    private bool controlReversed;
    public bool ControlReversed
    {
        get
        {
            return controlReversed;
        }

        set
        {
            controlReversed = value;
        }
    }
    private int checkpointCount;
    private int deathCount;
    public int DeathCount
    {
        get
        {
            return deathCount;
        }

        set
        {
            deathCount = value;
        }
    }
    private int currentStage;
    private float playTime;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        isPlay = SceneManager.GetActiveScene().name == "PlayScene";
        controlReversed = !(PlayerPrefs.GetInt("ControlReverse", 0) == 0);
        checkpointCount = PlayerPrefs.GetInt("Checkpoint", 1);
        deathCount = PlayerPrefs.GetInt("Death", 0);
        backgroundMusicSlider.value = PlayerPrefs.GetFloat("BackgroundMusic", 1f);
        soundEffectsSlider.value = PlayerPrefs.GetFloat("SoundEffects", 1f);
        playTime = PlayerPrefs.GetFloat("Time", 0f);
        currentStage = (checkpointCount - 1) / 3 + 1;

        if (controlReversed)
        {
            controlToggleSwitch.On();
        }
        else
        {
            controlToggleSwitch.Off();
        }
    }

    private void Update()
    {
        if (isPlay)
        {
            playTime += Time.deltaTime;
            checkpointText.text = $"체크 포인트: {currentStage}-{checkpointCount % currentStage + 1} <alpha=#66> | <alpha=#FF> {deathCount}회 사망 <alpha=#66> | <alpha=#FF> {(int)playTime / 60:D2}:{(int)playTime % 60:D2}";
        }
        else
        {
            checkpointText.text = $"체크 포인트: {currentStage}-{checkpointCount % currentStage + 1}";
        }
    }

    public void ChangeScene()
    {
        PlayerPrefs.SetInt("ControlReverse", controlReversed ? 1 : 0);
        PlayerPrefs.SetInt("Checkpoint", checkpointCount);
        PlayerPrefs.SetInt("Death", deathCount);
        PlayerPrefs.SetFloat("BackgroundMusic", backgroundMusicSlider.value);
        PlayerPrefs.SetFloat("SoundEffects", soundEffectsSlider.value);

        if (isPlay)
        {
            PlayerPrefs.SetFloat("Time", playTime);
        }
    }

    public void ReachCheckpoint()
    {
        checkpointCount++;
        currentStage = (checkpointCount - 1) / 3 + 1;
    }

    public void ResetButton()
    {
        PlayerPrefs.DeleteKey("Checkpoint");
        PlayerPrefs.DeleteKey("Death");
        PlayerPrefs.DeleteKey("Time");

        checkpointText.text = "체크 포인트: 1-1";

        GameObject.Find("CanvasMenu").GetComponent<PauseMenu>().Continue();
    }
}
