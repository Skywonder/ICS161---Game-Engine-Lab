using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormHole : MonoBehaviour {

    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider obj) {
        if (obj.tag == "Player") {
            //shoot player out and after 1 sec go to end screen
            Debug.Log("into the wormhole");
            obj.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, speed), ForceMode.Impulse);
        }

    }
}
