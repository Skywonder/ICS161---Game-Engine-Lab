using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contact_detection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player1" || other.transform.tag == "Player2")
        {
            Debug.Log("Found player");
            Destroy(other.gameObject);
        }

    }
    
}
