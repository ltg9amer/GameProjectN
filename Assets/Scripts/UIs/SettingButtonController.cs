using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingButtonController : MonoBehaviour
{
    public void LoadSettingScene()
    {
        SceneManager.LoadScene("SettingScene"); // SettingScene���� �Ѿ�ϴ�.
    }
}
