using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestinyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider obj) {
        if (obj.tag == "Player") {
            Debug.Log("End");
            SceneManager.LoadScene("EndScene");

        }
    }

}
