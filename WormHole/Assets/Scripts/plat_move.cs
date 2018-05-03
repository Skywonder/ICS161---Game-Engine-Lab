using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plat_move : MonoBehaviour {
    public bool isActivate = false;
    public Transform farEnd;
    private Vector3 frometh;
    private Vector3 untoeth;
    private float secondsForOneLength = 10f;

    void Start()
    {
        frometh = transform.position;
        untoeth = farEnd.position;
    }

    void Update()
    {
        if (isActivate)
        {
            transform.position = Vector3.Lerp(frometh, untoeth, Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time / secondsForOneLength, 1f)));
        }
    }

    void OnCollisionEnter(Collision ob) {
        if (ob.transform.tag == "Player") {
            ob.transform.SetParent(this.gameObject.transform);

        }

    }

    void OnCollisionExit(Collision ob) {
        if (ob.transform.tag == "Player") {
            ob.transform.SetParent(null);
        }


    }


}
