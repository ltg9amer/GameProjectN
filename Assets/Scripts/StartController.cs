using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtonController : MonoBehaviour
{
	public void LoadStartScene()
	{
		SceneManager.LoadScene("StartScene"); // StartScene으로 이동합니다.
	}
}