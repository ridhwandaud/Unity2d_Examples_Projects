using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public static MainMenu instance;

	public Image soundImg;
	public Sprite soundOn;
	public Sprite soundOff;

	public string storeLink = "your store link";
	public string facebookLink = "your facebook link";

	public Text countDownTxt;
	public Text bestUI;
	public Text bestGO;
	public Text TimeUI;
	public Text TimeGO;

	public GameObject UI;
	public GameObject GameOver;

	private Animator countDownAnim;

	void Awake(){
		instance = this;
		countDownAnim = countDownTxt.gameObject.GetComponent<Animator> ();
		UI.SetActive (true);
		GameOver.SetActive (false);
//		PlayerPrefs.DeleteAll ();
	}

	void Start(){
		if (PlayerPrefs.HasKey ("best")) {	//check if best exist, if not this is the first time you play game
			if (GameManager.Best == 0)		//if player reached to zero
				bestUI.text = "ZERO";
			else
				bestUI.text = "BEST: " + GameManager.Best;
			TimeUI.text = "in " + GameManager.TimeAtBest + " seconds";
		}
		//init sound image state
		if (GlobalValue.isSound) 
			soundImg.sprite = soundOn;
		else
			soundImg.sprite = soundOff;
	}

	public void CorrectPick(Color color){
		countDownTxt.text = GameManager.CountDown + "";
		countDownTxt.color = color;
		countDownAnim.SetTrigger ("Correct");
	}
	
	public void Play(){
		GameManager.instance.Play ();
		UI.SetActive (false);
	}

	public void Restart(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void ShowGameOver(){
		GameOver.SetActive (true);
		if (GameManager.Best == 0)
			bestGO.text = "ZERO";
		else
			bestGO.text = "BEST: " + GameManager.Best;
		TimeGO.text = "in " + GameManager.TimeAtBest + " seconds";
	}

	public void Sound(){
		GlobalValue.isSound = !GlobalValue.isSound;
		if (GlobalValue.isSound) {
			soundImg.sprite = soundOn;
			SoundManager.SoundVolume = 1f;
			SoundManager.PlaySfx ("Click");
		} else {
			soundImg.sprite = soundOff;
			SoundManager.SoundVolume = 0f;
		}
	}

	public void OpenFacebook(){
		Application.OpenURL (facebookLink);
	}

	public void OpenStoreLink(){
		Application.OpenURL (storeLink);
	}
}
