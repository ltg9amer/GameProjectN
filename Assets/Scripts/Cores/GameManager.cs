using MoreMountains.CorgiEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public struct SettingData
{
    [SerializeField]
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
    [SerializeField]
    private float backgroundMusicVolume;
    public float BackgroundMusicVolume
    {
        get
        {
            return backgroundMusicVolume;
        }

        set
        {
            backgroundMusicVolume = value;
        }
    }
    [SerializeField]
    private float soundEffectsVolume;
    public float SoundEffectsVolume
    {
        get
        {
            return soundEffectsVolume;
        }

        set
        {
            soundEffectsVolume = value;
        }
    }

    public SettingData(bool controlReversed = false, float backgroundMusicVolume = 1f, float soundEffectsVolume = 1f)
    {
        this.controlReversed = controlReversed;
        this.backgroundMusicVolume = backgroundMusicVolume;
        this.soundEffectsVolume = soundEffectsVolume;
    }
}

[Serializable]
public struct UserData
{
    public SettingData settingData;
    [SerializeField]
    private int checkpointCount;
    public int CheckpointCount
    {
        get
        {
            return checkpointCount;
        }

        set
        {
            checkpointCount = value;
        }
    }
    [SerializeField]
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
    [SerializeField]
    private int jumpCount;
    public int JumpCount
    {
        get
        {
            return jumpCount;
        }

        set
        {
            jumpCount = value;
        }
    }
    [SerializeField]
    private float playTime;
    public float PlayTime
    {
        get
        {
            return playTime;
        }

        set
        {
            playTime = value;
        }
    }
    [SerializeField]
    private string userName;
    public string UserName
    {
        get
        {
            return userName;
        }

        set
        {
            userName = value;
        }
    }

    public UserData(string userName, int checkpointCount = 1, int deathCount = 0, int jumpCount = 0, float playTime = 0f)
    {
        this.userName = userName;
        settingData = new SettingData(false, 1f, 1f);
        this.checkpointCount = checkpointCount;
        this.deathCount = deathCount;
        this.jumpCount = jumpCount;
        this.playTime = playTime;
    }
}

[Serializable]
public class RankingSaveList
{
    public List<UserData> ranking;

    public RankingSaveList()
    {
        ranking = new List<UserData>();
    }

    public void RankingSort()
    {
        ranking = ranking.OrderBy(user => user.DeathCount).OrderBy(user => user.PlayTime).ToList();
    }
}

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    [SerializeField]
    private List<TextMeshProUGUI> rankingDataTexts = new List<TextMeshProUGUI>();
    [SerializeField]
    private Slider backgroundMusicSlider;
    [SerializeField]
    private Slider soundEffectsSlider;
    [SerializeField]
    private TextMeshProUGUI checkpointText;
    [SerializeField]
    private TextMeshProUGUI statisticsText;
    [SerializeField]
    private Switch controlToggleSwitch;
    public UserData currentUserData;
    public RankingSaveList rankingSaveList;
    private Character corgiCharacter;
    public Character CorgiCharacter
    {
        get
        {
            return corgiCharacter;
        }

        set
        {
            corgiCharacter = value;
        }
    }
    private CorgiController controller;
    public CorgiController Controller => controller;
    private CharacterJump characterJump;
    public CharacterJump CharacterJump => characterJump;
    private CharacterHorizontalMovement horizontalMovement;
    public CharacterHorizontalMovement HorizontalMovement => horizontalMovement;
    private bool isPlay;
    public bool IsPlay => isPlay;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        isPlay = SceneManager.GetActiveScene().name == "PlayScene";
        currentUserData = JsonUtility.FromJson<UserData>(PlayerPrefs.GetString("CurrentUser", JsonUtility.ToJson(new UserData("-", 1, 0, 0, 0f))));
        rankingSaveList = JsonUtility.FromJson<RankingSaveList>(PlayerPrefs.GetString("Ranking", JsonUtility.ToJson(new RankingSaveList())));
        backgroundMusicSlider.value = currentUserData.settingData.BackgroundMusicVolume;
        soundEffectsSlider.value = currentUserData.settingData.SoundEffectsVolume;

        if (isPlay)
        {
            corgiCharacter = FindObjectOfType<Character>();
            controller = corgiCharacter.GetComponent<CorgiController>();
            characterJump = corgiCharacter.FindAbility<CharacterJump>();
            horizontalMovement = corgiCharacter.FindAbility<CharacterHorizontalMovement>();
            characterJump.jumpCount = currentUserData.JumpCount;
            CheckPoint[] checkpoints = FindObjectsOfType<CheckPoint>();
            
            foreach (var checkpoint in checkpoints)
            {
                if (checkpoint.CheckPointOrder < currentUserData.CheckpointCount)
                {
                    checkpoint.OnTriggerEnter2D(CorgiCharacter.GetComponent<Collider2D>());
                }
                else if (checkpoint.CheckPointOrder == currentUserData.CheckpointCount)
                {
                    LevelManager.Instance.DebugSpawn = checkpoint;
                }
            }
        }
        else
        {
            for (int i = 0; i < 3; ++i)
            {
                if (rankingSaveList.ranking.Count < i + 1)
                {
                    rankingDataTexts[i].text = "-";
                    rankingDataTexts[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "-회 사망\n--:--";
                }
                else
                {
                    rankingDataTexts[i].text = rankingSaveList.ranking[i].UserName;
                    rankingDataTexts[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{rankingSaveList.ranking[i].DeathCount}회 사망\n{(int)rankingSaveList.ranking[i].PlayTime / 60:D2}:{(int)rankingSaveList.ranking[i].PlayTime % 60:D2}";
                }
            }
        }
    }

    private void Start()
    {
        if (currentUserData.settingData.ControlReversed)
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
            currentUserData.PlayTime += Time.deltaTime;
            checkpointText.text = statisticsText.text = $"체크 포인트: {currentUserData.CheckpointCount} <alpha=#66> | <alpha=#FF> {currentUserData.DeathCount}회 사망 <alpha=#66> | <alpha=#FF> {(int)currentUserData.PlayTime / 60:D2}:{(int)currentUserData.PlayTime % 60:D2}";
        }
        else
        {
            checkpointText.text = $"체크 포인트: {currentUserData.CheckpointCount}";
        }
    }

    public void OnChangeScene()
    {
        isPlay = SceneManager.GetActiveScene().name == "PlayScene";
        currentUserData.settingData.BackgroundMusicVolume = backgroundMusicSlider.value;
        currentUserData.settingData.SoundEffectsVolume = soundEffectsSlider.value;

        if (isPlay)
        {
            currentUserData.JumpCount = characterJump.jumpCount;
        }

        PlayerPrefs.SetString("CurrentUser", JsonUtility.ToJson(currentUserData));
        PlayerPrefs.Save();
    }

    public void OnDieHandle()
    {
        currentUserData.DeathCount++;
    }

    [ContextMenu("Reset Ranking(Debug)")]
    public void ResetRanking()
    {
        PlayerPrefs.DeleteKey("Ranking");

        for (int i = 0; i < 3; ++i)
        {
            rankingDataTexts[i].text = "Corgi";
            rankingDataTexts[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "0회 사망\n00:00";
        }
    }

    [ContextMenu("Reset User Data")]
    public void ResetUserData()
    {
        PlayerPrefs.DeleteKey("CurrentUser");

        currentUserData = new UserData("userName", 1, 0, 0, 0f);
        backgroundMusicSlider.value = currentUserData.settingData.BackgroundMusicVolume;
        soundEffectsSlider.value = currentUserData.settingData.SoundEffectsVolume;

        if (currentUserData.settingData.ControlReversed)
        {
            controlToggleSwitch.On();
        }
        else
        {
            controlToggleSwitch.Off();
        }

        checkpointText.text = "체크 포인트: 1";

        GameObject.Find("SettingPopup").GetComponent<PauseMenu>()?.Continue();
    }
}
