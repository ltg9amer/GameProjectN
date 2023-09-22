using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
	[SerializeField] private RawImage _Img;
	[SerializeField] private float _x, _y;

	private void Update()
	{
		_Img.uvRect = new Rect(_Img.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _Img.uvRect.size);
	}
}

