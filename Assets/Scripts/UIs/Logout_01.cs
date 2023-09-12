using UnityEngine;
using UnityEngine.UI;

public class Logout_01 : MonoBehaviour
{
    // "Button_Exit" ��ư�� Inspector���� �������־�� �մϴ�.
    public Button exitButton;

    private void Start()
    {
        // "Button_Exit" ��ư�� Ŭ�� �̺�Ʈ �����ʸ� �߰��մϴ�.
        exitButton.onClick.AddListener(ExitGame);
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �����Ϳ��� ���� ���̸� �÷��� ��带 �����մϴ�.
#else
        Application.Quit(); // ����� ���ӿ����� ������ �����մϴ�.
#endif
    }
}
