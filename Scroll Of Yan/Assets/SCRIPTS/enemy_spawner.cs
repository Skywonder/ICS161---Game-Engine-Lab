using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawner : MonoBehaviour {

    public GameObject prefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spawning() {

        float randomX = Random.Range(-GetComponent<SphereCollider>().radius, GetComponent<SphereCollider>().radius);
        float randomY = Random.Range(-GetComponent<SphereCollider>().radius, GetComponent<SphereCollider>().radius);
        

        //GameObject enemy = Instantiate(prefab, new Vector3(randomX, randomY, 0), Quaternion.identity) as GameObject;
        GameObject enemy = Instantiate(prefab, transform.position + new Vector3(randomX, randomY, 0), Quaternion.identity) as GameObject;
        enemy.SetActive(true);
        //enemy.AddComponent<Rigidbody>().useGravity = false;
        //enemy.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;


    }

}
