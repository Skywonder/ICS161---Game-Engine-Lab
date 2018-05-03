using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Functions : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (SceneManager.GetActiveScene().name == "Prototype")
        {
            this.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void ClickRestart() {

        SceneManager.LoadScene("Prototype");
    }

    public void ClickQuit() {
        SceneManager.LoadScene("StartScene");

    }

}
