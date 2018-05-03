using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider obj) {
        if (obj.tag == "Player") {
            obj.GetComponent<PlayerController>().startposition = this.gameObject;

        }
    }

    void OnTriggerStay(Collider obj)
    {
        if (obj.tag == "Player")
        {
            obj.GetComponent<PlayerController>().startposition = this.gameObject;

        }
    }

}
