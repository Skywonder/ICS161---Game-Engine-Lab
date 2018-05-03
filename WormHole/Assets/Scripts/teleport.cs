using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour {

    public GameObject target;
	// Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider obj) {
        if (obj.tag == "Player") {

            obj.transform.position = target.transform.position;

        }
    }

}
