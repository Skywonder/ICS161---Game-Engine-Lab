using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject player;
    public Vector2 minimumBoundary;
    public Vector2 maximumBoundary;
    public float distance_away = -60f;

	// Use this for initialization
	void Awake () {

        player = GameObject.FindGameObjectWithTag("Player");

	}



    // Update is called once per frame
    void LateUpdate() {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, distance_away);

        transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, minimumBoundary.x, maximumBoundary.x),
        Mathf.Clamp(transform.position.y, minimumBoundary.y, maximumBoundary.y),
        transform.position.z);
    }
    
}
