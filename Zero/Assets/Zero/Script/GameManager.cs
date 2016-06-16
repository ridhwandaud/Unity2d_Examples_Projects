using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public enum GameState {Menu, Playing, Success, Fail};
	public GameState state;
	public int countDownFrom = 99;
	public AudioClip soundSuccess;


	private float timeStart;		//get the time when the game begin
	private float timeEnd;		//get the time when the game fail or complete


	public static GameManager instance;

	public static GameState CurrentState {
		set{ instance.state = value; }
		get{ return instance.state; }
	}

	public static int CountDown {
		set{ instance.countDownFrom = value; }
		get{ return instance.countDownFrom; }
	}

	public static int Best {
		set{ PlayerPrefs.SetInt ("best", value); }
		get{ return PlayerPrefs.GetInt ("best", 99); }
	}

	public static int TimeAtBest {
		set{ PlayerPrefs.SetInt ("time", value); }
		get{ return PlayerPrefs.GetInt ("time", 0); }
	}

	void Awake(){
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		//Exit game when not in playing mode
		if (Input.GetKeyDown (KeyCode.Escape) && state != GameState.Playing)
			Application.Quit ();
	}

	public void GameOver(){
		state = GameState.Fail;		//set state is fail to stop gameplay

		CheckRecord ();

		MainMenu.instance.ShowGameOver ();		//call MainMenu function
	}

	public void Play(){
		state = GameState.Playing;		//allow spin rotating,
		timeStart = Time.time;
	}

	public void GameSuccess(){
		state = GameState.Success;
		CheckRecord ();
		MainMenu.instance.ShowGameOver ();		//call MainMenu function
		SoundManager.PlaySfx (soundSuccess);
	}

	private void CheckRecord(){

		timeEnd = Time.time;	//get the time
		int totalTimePlaying = (int)(timeEnd - timeStart);

		//check and save new best
		if (countDownFrom < Best) {		// lower is better
			Best = countDownFrom;
			TimeAtBest = totalTimePlaying;		//save new time record with new best

		} else if (countDownFrom == Best) {		//check time record when current number equal with the best
			//check time record at new best: lower is better
			if (PlayerPrefs.HasKey ("time")) {		//check if it exist
				if (totalTimePlaying < TimeAtBest)		//check record
					TimeAtBest = totalTimePlaying;		//save new time record
			} else {
				TimeAtBest = totalTimePlaying;		//save new time if this is first time playing
			}
		}
	}
}
