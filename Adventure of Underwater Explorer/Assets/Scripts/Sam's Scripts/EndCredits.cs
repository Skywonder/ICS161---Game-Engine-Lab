using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCredits : MonoBehaviour {


    private void Update() {
	
	}

	public void GameFinish(){

		SceneManager.LoadScene ("StartSceneTest");

	}
}
