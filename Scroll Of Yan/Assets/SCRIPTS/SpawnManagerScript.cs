using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour {

    public GameObject[] spawners;
    public int spawn_location;
	public int spawn_number;// = 10;
    public int spawned = 0;
    public bool finish_spawning = false;
	public int cState;
	public bool end_spawning = false;
    public string wave;

	public enum State{
		Wave1,
		Wave2,
		Wave3,
		Wave4,
		Wave5

	}


	// Use this for initialization
	void Start () {

        spawners = GameObject.FindGameObjectsWithTag("Spawner");
		cState = (int)State.Wave1;
	}

    // Update is called once per frame
    void Update()
    {
		switch (cState){
		case 0:
			    SpawnWave1 ();
                wave = "Wave 1";
			    break;
		case 1:
		    	SpawnWave2 ();
                wave = "Wave 2";
		    	break;
		case 2:
			    SpawnWave3 ();
                wave = "Final Wave";
		    	break;
		/*
		case 3:
			SpawnWave4 ();
			break;
		case 4:
			SpawnWave5 ();
			break;
		*/
		}
        
    }

	void SpawnSystem(int spawn_number){
		spawn_location = Random.Range(Random.Range(0, spawners.Length), Random.Range(0, spawners.Length));
		if (spawned < spawn_number && !finish_spawning)
		{
			spawners[spawn_location].GetComponent<enemy_spawner>().Spawning();
			spawned++;
			if (spawned == spawn_number) 
			{
				finish_spawning = true;
			}
		}
		if (cState == 2) {
			end_spawning = true;
		}
	}


	void SpawnWave1(){
		SpawnSystem(10);
	}
	void SpawnWave2(){
		SpawnSystem(15);
	}
	void SpawnWave3(){
		SpawnSystem(20);
	}
	/*
	void SpawnWave4(){
		SpawnSystem(15);
	}
	void SpawnWave5(){
		SpawnSystem(20);
	}
	*/
}
