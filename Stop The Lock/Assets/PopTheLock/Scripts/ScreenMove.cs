using UnityEngine;
using System.Collections;

public class ScreenMove : MonoBehaviourHelper
{
	public static bool isMoving = false;

	public static IEnumerator Move(RectTransform t, bool startAnim)
	{
		isMoving = true;

		float p0 = 0;

		float p1 = -Screen.width;

		if (startAnim) 
		{
			p0 = Screen.width;

			p1 = 0;
		}

		t.anchoredPosition = new Vector2(p0,t.anchoredPosition.y);

		float timer = 0;

		float time = 0.3f;

		while (timer <= time) 
		{
			timer += Time.deltaTime;

			float f = Mathf.Lerp (p0, p1, timer / time);
		
			t.anchoredPosition = new Vector2(f,t.anchoredPosition.y);

			yield return 0;
		}

		yield return new WaitForSeconds (0.1f);

		isMoving = false;
	}
}
