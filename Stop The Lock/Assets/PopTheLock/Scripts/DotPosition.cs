﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class DotPosition : MonoBehaviourHelper 
{
	[SerializeField] private Transform dotTransform;

	[SerializeField] private RectTransform dotRectTransform;

	public float GetDotSize()
	{
//		return dotRectTransform.sizeDelta.x * gameManager.canvasScale;
//		float size = dotRectTransform.GetComponent<Image>().sprite.bounds.size.x;
//		float canvasScale = gameManager.canvasScale;
//		print("size = " + size + " canvasScale = " + canvasScale + " sizeRect = " + sizeRect);
//		return  size * gameManager.canvasScale;

		float sizeRect = dotRectTransform.sizeDelta.x;
		return sizeRect;
	}

	public Transform GetDotTransform()
	{
		return dotTransform;
	}



	public float GetRotation()
	{
		return transform.eulerAngles.z;
	}


	public void DoPosition()
	{
		float minRotDecal = 30;

		if (UnityEngine.Random.Range (0, 2) == 0)
			minRotDecal *= -1;

		float randLeft = UnityEngine.Random.Range (-90.00f, -30.00f);

		float randRight = UnityEngine.Random.Range (30.00f, 90.00f);

		float rotationTemp = randLeft;

		if (UnityEngine.Random.Range (0, 2) == 0)
			rotationTemp = randRight;

		float rotation = player.GetRotation () + rotationTemp;

	

		Vector3 rot = Vector3.forward * rotation;

		transform.eulerAngles = rot;

		dotTransform.localScale = Vector2.zero;
		DoScale (0, 1, () => {
		});


	}

	public Vector2 GetPosition()
	{
		return dotTransform.position;
	}

	public bool isLeftOfScreen()
	{
		bool isLeft = transform.eulerAngles.z >= 0 && transform.eulerAngles.z < 180;
		return isLeft;
	}

	void DoScale(float s0, float s1, Action callback)
	{
		StartCoroutine (_DoScale (s0, s1, callback));
	}

	IEnumerator _DoScale(float s0, float s1, Action callback)
	{
		dotTransform.localScale = Vector2.one * s0;

		float time = 0.2f;

		float timer = 0f;

		while (timer <= time) 
		{
			timer += Time.deltaTime;

			float f = Mathf.Lerp (s0, s1, timer / time);

			dotTransform.localScale = Vector2.one * f;

			yield return 0;
		}

		if (callback != null)
			callback ();
	}
}
