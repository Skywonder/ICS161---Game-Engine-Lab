using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class rotate : MonoBehaviour {

    public GameObject target;
    public AudioSource source;
    public AudioClip clip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 45 * Time.deltaTime, 0));
	}

    void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            if (Input.GetKey(KeyCode.F) && source.isPlaying == false) {
                target.GetComponent<Orb>().isActive = true;
                source.PlayOneShot(clip); 
                //change target color and call it active
            }
        }
    }
}
