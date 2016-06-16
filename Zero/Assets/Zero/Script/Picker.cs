using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Picker : MonoBehaviour {
	[Tooltip("distance of raycast detect the color")]
	public float distance = 2f;	
	[Tooltip("Sprite Renderer of picker image. Use this to set color")]
	public SpriteRenderer spritePicker;

	[Tooltip("The color of the item ID 0")]
	public Color Color_ID_0;
	[Tooltip("The color of the item ID 1")]
	public Color Color_ID_1;
	[Tooltip("The color of the item ID 2")]
	public Color Color_ID_2;
	[Tooltip("The color of the item ID 3")]
	public Color Color_ID_3;

	[Tooltip("Play this sound when player pick correct color")]
	public AudioClip soundCorrect;
	[Tooltip("Play this sound if player choose wrong")]
	public AudioClip soundFail;

	private Spin spin;
	private int choosenID;
	private int currentID;
	private int previousID;
	private bool trueArea = false;	//if the picker point to the right item, set this true
	private bool allowCheck = true;

	private List<Color> colorList;
	private Animator anim;


	// Use this for initialization
	void Start () {
		spin = FindObjectOfType<Spin> ();
		anim = GetComponent<Animator> ();

		//add color to list
		colorList = new List<Color> ();
		colorList.Add (Color_ID_0);
		colorList.Add (Color_ID_1);
		colorList.Add (Color_ID_2);
		colorList.Add (Color_ID_3);

		//random first color
		ChooseRandomID ();
	}
	
	// Update is called once per frame
	void Update () {
		//only work if press any key and the game in playing mode
		if (Input.anyKeyDown && GameManager.CurrentState == GameManager.GameState.Playing) {
			if (currentID == choosenID) {		//if player pick the right color
				allowCheck = false;		//stop checking color in FixedUpdate temporary
				GameManager.CountDown--;	//count down number by 1
				if (GameManager.CountDown == 0){
					GameManager.instance.GameSuccess ();	//if the counter equal zero then Success
					anim.SetTrigger ("Wrong");	//hide the picker
					MainMenu.instance.CorrectPick (colorList [choosenID]);		//call function in MAin menu, aim to countdownnumber text
					return;
				}
				anim.SetTrigger ("Correct");	//animation effect for picker
				ChooseRandomID ();		//choose another color to choose
				spin.ChangeDirection ();	//send action to change the direction of the spin
				SoundManager.PlaySfx (soundCorrect);	//play correct sound
			} else {
				anim.SetTrigger ("Wrong");		//animation effet wrong
				GameManager.instance.GameOver ();	//call Game Over
				SoundManager.PlaySfx (soundFail);	//play fail sound
			}
		}
	}


	public void ChooseRandomID(){
		while (choosenID == currentID) {	//do not allow create next ID the same with the old ID
			choosenID = Random.Range (0, colorList.Count);
		}
		trueArea = false;	//reset
		allowCheck = true;	//allow checking the color again
		spritePicker.color = colorList [choosenID];		//change the picker's color to the color choosen
		MainMenu.instance.CorrectPick (colorList [choosenID]);		//call function in MAin menu, aim to countdownnumber text
	}

	void FixedUpdate(){
		if (allowCheck) {	
			RaycastHit2D ray = Physics2D.Raycast (transform.position, new Vector2 (0, 1), distance);
			if (ray) {		//if collider with any objects
				currentID = ray.collider.gameObject.GetComponent<PickerID> ().ID;		//get ID of the item by PickerID script

				if (currentID == choosenID)
					trueArea = true;	//tell that the picker are pointing to the right color
				else {
					if (trueArea) {		//if the picker leave out of the right color, then GameOver
						allowCheck = false;		//stop checking
						anim.SetTrigger ("Hide");	//
						GameManager.instance.GameOver ();		//call Game Over
						SoundManager.PlaySfx (soundFail);
					}
				}

			}
		}
	}

	//draw the distance line in Editor
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Vector3 direction = transform.TransformDirection(Vector3.up) * distance;
		Gizmos.DrawRay(transform.position, direction);
	}
}
