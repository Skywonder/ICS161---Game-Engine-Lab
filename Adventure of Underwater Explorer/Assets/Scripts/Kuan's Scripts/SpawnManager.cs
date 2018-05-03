using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public GameObject[] spawners;
    public int spawn_location;
    public GameObject boss_spawn_location;
    public int spawn_number;// = 10;
    public int spawned = 0;
    public bool finish_spawning = false;
    public bool end_spawning = false;
    public GameObject BossSpawn;
    //public string wave;

    // Use this for initialization
    void Start () {
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
        //cState = (int)State.Wave1;
    }

    // Update is called once per frame
    void Update () {
        //SpawnSystem(spawn_number);
	}

    public void SpawnSystem(int spawn_number, GameObject typeUnit)
	{
        if (typeUnit.name != "Boss")
        {
            spawn_location = Random.Range(Random.Range(0, spawners.Length), Random.Range(0, spawners.Length));
            if (spawned < spawn_number && !finish_spawning)
            {
                spawners[spawn_location].GetComponent<SpawnerScript>().Spawning(typeUnit);
                spawned++;
                if (spawned == spawn_number)
                {
                    finish_spawning = true;
                }
            }
        }
        else {//For BOSS
            Debug.Log("Boss found");
            if (spawned < spawn_number && !finish_spawning)
            {
                spawners[3].GetComponent<SpawnerScript>().Spawning(typeUnit);
                spawned++;
                if (spawned == spawn_number)
                {
                    finish_spawning = true;
                }
            }

        }
    }
}
