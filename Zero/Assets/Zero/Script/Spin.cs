using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {
	public float speed = 251f;
	private float direction = 1; //1: rotate clk, 0: rotate cclk
	
	// Update is called once per frame
	void FixedUpdate () {
		//only rotate when in playing mode
		if (GameManager.CurrentState == GameManager.GameState.Playing)
			transform.Rotate (Vector3.forward, direction * speed * Time.fixedDeltaTime);
	}

	//called by Picker.cs
	public void ChangeDirection(){
		direction *= -1;
	}
}
