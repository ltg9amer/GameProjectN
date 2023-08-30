using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
	public GameObject canvasMenu; // CanvasMenu 게임 오브젝트를 Inspector에서 연결해주세요.

	public void ContinueGame()
	{
		canvasMenu.SetActive(false); // CanvasMenu를 비활성화합니다.
	}
}
