using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public bool wondering = true;
    public bool chasing = false;
    public bool move_zone = false;
    public float cd = 40f;
    public float rcd = 0.1f;
    public float timestamp = 0f;
    public float rtimestamp = 0f;
    public float chtimer = 0f;
    public float chcd = 3f;
    public GameObject[] Pointholder;
    public GameObject point;
    public float speed = 5;
    public float step;
    public bool chase = false;
    public Vector3 target_position = Vector3.zero;
    public Vector3 player_pos = Vector3.zero;
    public float previous_pos_x;//x
    public float current_pos_x;//x
    public int damage =5;
    public Animator animator;
    //establish move range

    //establish boundary within range


    // Use this for initialization
    void Awake () {
        Pointholder = GameObject.FindGameObjectsWithTag("PointHolder");
        animator = GetComponentInChildren<Animator>();
    }

    void Start() {
        

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        
        step = speed * Time.deltaTime;
        Debug.Log("TimeStamp: " + timestamp);
       
        if (!chase)
        {
            if (wondering)
            {
                //this is for moving to different area
                if (timestamp <= Time.time)
                {
                    move_zone = true;
                    point = Pointholder[Random.Range(Random.Range(0, Pointholder.Length), Pointholder.Length)];
                    target_position = point.transform.position;
                    timestamp = cd + Time.time;

                }
                //establish random position inside the sphere range
                //and a random direction
                //and set a range for the direction
                if (rtimestamp <= Time.time)
                {
                    float Xrange_movement = Random.Range(-point.GetComponent<SphereCollider>().radius, point.GetComponent<SphereCollider>().radius);
                    float Yrange_movement = Random.Range(-point.GetComponent<SphereCollider>().radius, point.GetComponent<SphereCollider>().radius);
                    target_position = new Vector3(Xrange_movement, Yrange_movement, 0);
                    //this is for random flying within boundary
                    if (transform.position.x - target_position.x < 0) {
                        GetComponentInChildren<SpriteRenderer>().flipX = true;
                    }
                    if (transform.position.x - target_position.x > 0) {
                        GetComponentInChildren<SpriteRenderer>().flipX = false;
                    }
                    
                    rtimestamp = rcd + Time.time;
                }
                transform.position = Vector3.MoveTowards(transform.position, target_position, step);
            }
            if (move_zone && !chase)
            {
                MoveToNextZone(step, target_position);
            }
        }

        //chase
        if (chase && chtimer <= Time.time ) {
            
            animator.SetTrigger("Attack");
            Debug.Log("Chase");
            //current_pos_x = transform.position.x;
            if (transform.position.x - player_pos.x < 0) {
                GetComponentInChildren<SpriteRenderer>().flipX = true;
            }
            if (transform.position.x - player_pos.x > 0) {
                GetComponentInChildren<SpriteRenderer>().flipX = false;
            }


            transform.position = Vector3.MoveTowards(transform.position, player_pos, step * 1.5f);
            //player takes dmg on contact
            //player collider checks for contact
        }

	}


    void MoveToNextZone(float step, Vector3 target_position) {
        //check for next position that can move to on the map
        Debug.Log("Move to next");
        wondering = false;
        if (transform.position.x - target_position.x < 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        if (transform.position.x - target_position.x > 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        transform.position = Vector3.MoveTowards(transform.position, target_position, step);
        //current_pos_x = transform.position.x;
        if (transform.position == target_position) {
            move_zone = false;
            wondering = true;
        }

    }


    void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "Sword") {
          
            Destroy(this.gameObject);
        }
        if (other.transform.tag == "Player") {
            chase = false;
            target_position = transform.position;
            //try to pause it in air for a few second
            chtimer = Time.time + chcd;
            //StartCoroutine(waitforinvincible());
        }
    }


    void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            chase = true;
            if (other.GetComponent<PlayerController>().invincible) {
                chase = false;
            }
            PlayScream();
            player_pos = other.transform.position;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            chase = false;
        }

    }

    void OnCollisionExit(Collision other) {
        if (other.transform.tag == "Player") {
            chase = false;
         
        }
    }


    void PlayScream() {
        GetComponent<AudioSource>().Play();
    }

    IEnumerator waitforinvincible() {
        yield return new WaitForSeconds(10);
    }

}
