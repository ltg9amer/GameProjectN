using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // sceneName���� �Ѿ�ϴ�.
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �����Ϳ��� ���� ���̸� �÷��� ��带 �����մϴ�.
#else
        Application.Quit(); // ����� ���ӿ����� ������ �����մϴ�.
#endif
    }
}
