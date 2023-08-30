using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonController : MonoBehaviour
{
	public void LoadMainScene()
	{
		SceneManager.LoadScene("MainScene"); // MainScene으로 이동합니다.
	}
}
