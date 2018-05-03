using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_UI_Script : MonoBehaviour {

	public GameObject pause_canvas;
	public GameObject control_canvas;
    public GameObject game_manager;
	public GameObject option_button;//open
	public GameObject option_button2;//close
	public Text score;
    public Text life_remaining1;
	public Text life_remaining2;
    public Text zone;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
        life_remaining1.text = "Player 1 Lives Remaining: " + game_manager.GetComponent<GameManagerScript>().player_life1.ToString();
		life_remaining2.text = "Player 2 Lives Remaining: " + game_manager.GetComponent<GameManagerScript>().player_life2.ToString();
        zone.text = "Zone " + game_manager.GetComponent<GameManagerScript>().zone;
		score.text = "Score: " + game_manager.GetComponent<GameManagerScript> ().score;
    }

	public void QuitToStart(){
		SceneManager.LoadScene ("StartSceneTest");
	
	}

	public void Restart(){

		SceneManager.LoadScene ("KuanScene");
	}


	public void OpenPause(){
		option_button.SetActive (false);
		option_button2.SetActive (true);
		pause_canvas.SetActive (true);
		Time.timeScale = 0;
	
	}

	public void ClosePause(){
		option_button2.SetActive (false);
		option_button.SetActive (true);
		pause_canvas.SetActive (false);
		Time.timeScale = 1;
	
	}

	public void EndScene(){
		//SceneManager.LoadScene ("EndSceneTest");
		SceneManager.LoadScene ("EndCredits");
	}

	public void ShowControl(){

		pause_canvas.SetActive (false);
		control_canvas.SetActive (true);
		//just open the canvas

	}
	public void CloseControl(){

		pause_canvas.SetActive (true);
		control_canvas.SetActive (false);
		//just close the canvas
	}


}
