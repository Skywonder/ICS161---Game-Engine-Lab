using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour {
    public GameObject background;
    public GameObject background2;
    public GameObject endpoint;
    public GameObject startpoint;
    public float speed = 0.1f;
    public GameObject boss = null;
    public GameObject[] borders;
    // Use this for initialization

    void Awake() {
        borders = GameObject.FindGameObjectsWithTag("Border");
	
        for (int i = 0; i < borders.Length; i++)
        {
            Physics2D.IgnoreCollision(background.GetComponent<BoxCollider2D>(), borders[i].GetComponent<BoxCollider2D>(), true);
            Physics2D.IgnoreCollision(background2.GetComponent<BoxCollider2D>(), borders[i].GetComponent<BoxCollider2D>(), true);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update() {

        Physics2D.IgnoreCollision(background.GetComponent<BoxCollider2D>(), background2.GetComponent<BoxCollider2D>(), true);

        if (boss == null) {
            boss = GameObject.Find("Boss(Clone)");
        }
        if (boss != null) {
            for (int i = 0; i < borders.Length; i++)
            {
                Physics2D.IgnoreCollision(boss.GetComponent<CompositeCollider2D>(), borders[i].GetComponent<BoxCollider2D>(), true);
            }
        }


        if (this.transform.position.x <= endpoint.transform.position.x) {
            this.transform.position = startpoint.transform.position;
        }


	}

    void FixedUpdate() {

        this.gameObject.transform.Translate(new Vector3(-5, 0, 0) * speed * Time.deltaTime);


    }


}
