using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
	public GameObject canvasMenu; // CanvasMenu ���� ������Ʈ�� Inspector���� �������ּ���.

	public void ContinueGame()
	{
		canvasMenu.SetActive(false); // CanvasMenu�� ��Ȱ��ȭ�մϴ�.
	}
}
