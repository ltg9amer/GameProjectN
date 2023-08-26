using UnityEngine;
using UnityEngine.UI;

public class Logout_01 : MonoBehaviour
{
    // "Button_Exit" 버튼을 Inspector에서 연결해주어야 합니다.
    public Button exitButton;

    private void Start()
    {
        // "Button_Exit" 버튼에 클릭 이벤트 리스너를 추가합니다.
        exitButton.onClick.AddListener(ExitGame);
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중이면 플레이 모드를 종료합니다.
#else
        Application.Quit(); // 빌드된 게임에서는 게임을 종료합니다.
#endif
    }
}
