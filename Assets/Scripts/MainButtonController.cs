using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtonController : MonoBehaviour
{
    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene"); // StartScene���� �Ѿ�ϴ�.
    }
}
