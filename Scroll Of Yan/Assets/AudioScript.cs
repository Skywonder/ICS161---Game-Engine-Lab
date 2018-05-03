using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioScript : MonoBehaviour {

    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    
    public AudioSource source;
    public static GameObject instance;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this);
        if (instance == null) {
            instance = this.gameObject;
        }
        else {
            DestroyObject(this.gameObject);
            return;
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().name == "StartScene" && source.clip.name != clip1.name) {
            source.clip = clip1;
            source.Play();
        }
        if (SceneManager.GetActiveScene().name == "MainGame" && source.clip.name != clip2.name) {
            source.clip = clip2;
            source.Play();
        }
        if (SceneManager.GetActiveScene().name == "EndScene" && source.clip.name != clip3.name) {
            source.clip = clip3;
            source.Play();
        }
	}
}
