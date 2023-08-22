using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // sceneName으로 넘어갑니다.
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중이면 플레이 모드를 종료합니다.
#else
        Application.Quit(); // 빌드된 게임에서는 게임을 종료합니다.
#endif
    }
}
