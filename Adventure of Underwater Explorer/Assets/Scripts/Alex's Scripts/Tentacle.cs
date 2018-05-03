using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour {

    float staying_time;
	// Use this for initialization
	void Start () {
        staying_time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time >= staying_time + 3f)
            Destroy(this.gameObject);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {
            GameObject.Find("BackgroundAudio").GetComponents<AudioSource>()[3].Play();
            Destroy(other.gameObject);
        }
    }
}
