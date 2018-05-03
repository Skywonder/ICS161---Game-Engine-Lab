using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float timeStamp;
    public float cdtimer = 0.5f;
    public GameObject canvas;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                canvas.SetActive(true);
                Time.timeScale = 0;
            }
            else {
                canvas.SetActive(false);
                Time.timeScale = 1;
            }

        }
	}


}
