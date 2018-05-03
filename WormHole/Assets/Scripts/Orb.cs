using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour {

    public Material matoObj;
    public Color color_off;
    public Color color_on;
    public bool isActive = false;

	// Use this for initialization
	void Start () {
        matoObj.color = color_off;
    }
	
	// Update is called once per frame
	void Update () {
        if (isActive == true) {
            matoObj.color = color_on;
        }
        if (isActive == false) {
            matoObj.color = color_off;
        }
	}
}
