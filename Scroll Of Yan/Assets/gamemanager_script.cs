using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager_script : MonoBehaviour {

    public GameObject[] enemies;
    public GameObject SpawnManager;
    public GameObject gameover_canvas;
    public bool start = true;

	// Use this for initialization
	void Start () {
        SpawnManager = GameObject.FindGameObjectWithTag("SpawnManager");

        gameover_canvas.SetActive(false);		
	}
	
	// Update is called once per frame
	void Update () {
        if (start == true && SpawnManager.GetComponent<SpawnManagerScript>().spawned < SpawnManager.GetComponent<SpawnManagerScript>().spawn_number)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
        else {
            start = false;
        }

        if (!start) { //check for count
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }


		if (enemies.Length == 0 && !start && SpawnManager.GetComponent<SpawnManagerScript> ().end_spawning == true) {
			Time.timeScale = 0;
			gameover_canvas.SetActive (true);
		} 
		else if(enemies.Length == 0){
			start = true;
			SpawnManager.GetComponent<SpawnManagerScript> ().spawned = 0;
			SpawnManager.GetComponent<SpawnManagerScript>().cState += 1;
			SpawnManager.GetComponent<SpawnManagerScript> ().finish_spawning = false;
		}
	}
}
