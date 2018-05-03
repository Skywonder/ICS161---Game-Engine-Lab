using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour {

    public GameObject target;
    public float speed;
    public bool option_up = false;
    public bool option_down = false;

    void Awake() {
 
    }

    // Use this for initialization
	void Start () {
        
    }

    void Update() {
        if (option_up == true)
        {
            transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);
        }
        if (option_down == true) {

            transform.RotateAround(target.transform.position, Vector3.down, speed * Time.deltaTime);
        }
    }


    void OnTriggerEnter(Collider obj)
    {
        //increase gravity
        if (obj.tag == "Player") {
            obj.GetComponent<PlayerController>().gravity.y *= 2;
            Debug.Log("enter gravity " + obj.GetComponent<PlayerController>().gravity.y);
        }
    }

    void OnTriggerStay(Collider obj) {
        //pull player in

    }

    void OnTriggerExit(Collider obj) {
        //remove gravity 
        if (obj.tag == "Player") {
            obj.GetComponent<PlayerController>().gravity.y /= 2;
            Debug.Log("exit gravity " + obj.GetComponent<PlayerController>().gravity.y);
        }
    }


}
