using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanism : MonoBehaviour {

    public GameObject[] activated;
    public GameObject target;
    public int length;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        //check to see if all mech are activated
        if (length < activated.Length)
        {
            for (int i = 0; i < activated.Length; i++)
            {
                if (activated[i].GetComponent<Orb>().isActive == true)
                {
                    length++;
                }
            }
            if (length < activated.Length)
            {
                length = 0;
            }
        }
        
        

        if (length >= activated.Length) {
            length = activated.Length;
            target.GetComponent<plat_move>().isActivate = true;

        }
	}


}
