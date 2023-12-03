using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
	public void LoadScene(string sceneName)
	{
        Time.timeScale = 1f;

		SceneManager.LoadScene(sceneName);
	}

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
