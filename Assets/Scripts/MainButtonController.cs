using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtonController : MonoBehaviour
{
    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene"); // StartScene으로 넘어갑니다.
    }
}
