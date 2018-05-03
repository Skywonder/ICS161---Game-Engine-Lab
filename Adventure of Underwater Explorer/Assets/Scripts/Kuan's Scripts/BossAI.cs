using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class BossAI : MonoBehaviour {
	public GameObject game_manager;
    public GameObject[] move_pts;
    public int random_pt;
    public float speed;
    private bool routineActive = false;
    private bool moving = false;
    public int health = 100; //hp 

    // Use this for initialization
    void Start () {
		game_manager = GameObject.FindGameObjectWithTag ("gamemanager");
        move_pts = GameObject.FindGameObjectsWithTag("boss_pt");
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Boss hp: " + health);
        if (!moving) {
            moveToPoint();
        }

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, move_pts[random_pt].transform.position, step);

        if (transform.position == move_pts[random_pt].transform.position) {
            moving = false;
        }
    }

    void LateUpdate()
    {
        if (health <= 0) {
            health = 0;
			game_manager.GetComponent<GameManagerScript> ().score += 1000501;
            Destroy(this.gameObject);
        }
    }

    void moveToPoint() {
        moving = true;
        Debug.Log("moving");
        
        for (int i = 0; i < 100; i++)
        {
            random_pt = Random.Range(0, move_pts.Length);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter");
        if (other.tag == "Player1" || other.tag == "Player2") {
            Debug.Log("Player1 Enter Oct");
            Destroy(other.gameObject);
        }
        
        if (other.tag == "Bullet") {
			GameObject.Find ("BackgroundAudio").GetComponents<AudioSource> () [4].Play ();
            Destroy(other.gameObject);
            Debug.Log("take hit");
            health -= 1;
        }
        if (other.tag == "Bullet Enhanced") {
			GameObject.Find ("BackgroundAudio").GetComponents<AudioSource> () [4].Play ();
            Destroy(other.gameObject);
            Debug.Log("take hit enhanced");
            health -= 3;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exit");
        if (other.tag == "Player1" || other.tag == "Player2")
        {
            Debug.Log("Player1 Exit Oct");
            Destroy(other.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Stay");
        if (other.tag == "Player1" || other.tag == "Player2")
        {
            Debug.Log("Player1 Stay Oct");
            Destroy(other.gameObject);
        }
    }


}
