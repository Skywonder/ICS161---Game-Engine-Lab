using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour {

	//public Text title;

	// Use this for initialization
	void Start () {
	


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ReturnToStart(){
		//remember to change
		Debug.Log("Change to start scene");
		SceneManager.LoadScene ("StartSceneTest");
	
	}


	public void ControlMenu(){
	
		SceneManager.LoadScene ("ControlSceneTest");
	
	}


	public void StartGame(){
		//remember to change
		SceneManager.LoadScene ("KuanScene");
	
	}
		
	public void EndScene(){
		//SceneManager.LoadScene ("EndSceneTest");
		SceneManager.LoadScene ("EndCredits");
	}

	public void QuitGame(){
	
		Application.Quit ();
	}



}
