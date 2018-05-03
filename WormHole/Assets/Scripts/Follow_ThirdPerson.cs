using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_ThirdPerson : MonoBehaviour {

    public float rotateSpeed;

    public GameObject player;
    public Vector3 pposition;
    public float distanceAway;
    public float distanceUp;
    public float smooth;
    public Vector3 targetPosition;

    
	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
	}

    void Start() {
;
    }
	// Update is called once per frame
	void LateUpdate () {

        targetPosition = player.transform.position + player.transform.up * distanceUp - player.transform.forward* distanceAway;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
        transform.LookAt(player.transform);

	}
}
