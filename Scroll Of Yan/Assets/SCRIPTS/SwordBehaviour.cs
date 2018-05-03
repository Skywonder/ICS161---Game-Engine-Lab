using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MonoBehaviour {

    public GameObject player;
    public PlayerController pc;
    public float rotatespd = 5f;
    public float rotatespd_up = 1.5f;
    public float temp_speed = 0;
    public float old_speed =0;
    public float TargetDistance;
    public float delay_sec = 1f;
    public bool not_used = true;
    public int offset;
    public GameObject tail1;
    public GameObject tail2;
    

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        old_speed = rotatespd;
    }

    void Update() {
        offset = player.GetComponent<PlayerController>().face_offset;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if (not_used)
        {
            float adjustable = 0;
            
            transform.position = player.transform.position + (transform.position - player.transform.position).normalized * TargetDistance;
            if (Input.anyKeyDown)
            {
                adjustable = rotatespd * rotatespd_up;
            }
            else
            {
                adjustable = rotatespd;
            }
            if (offset < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                tail1.SetActive(false);
                tail2.SetActive(true);
                transform.RotateAround(player.transform.position, Vector3.forward, adjustable * Time.deltaTime);
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
                tail1.SetActive(true);
                tail2.SetActive(false);
                transform.RotateAround(player.transform.position, -Vector3.forward, adjustable * Time.deltaTime);

            }


           
            
            
        }
        else {
            //consider moving the sword to the right position ... easier for player to know which one is used...
            if (offset < 0)
            {
               
                GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0) * 10f, ForceMode.Impulse);
    
            }
            if (offset > 0) {
                
                GetComponent<Rigidbody>().AddForce(new Vector3(1, 0) * 10f, ForceMode.Impulse);
   
            }
            WaitBeforeDestroy();
        }
            //Vector3 delta = player.transform.position - transform.position;
            //delta.y = 0; // Keep same Y level
            //transform.position = player.transform.position + delta.normalized * TargetDistance;
        
    }


    public void WaitBeforeDestroy() {
        Destroy(gameObject, delay_sec);
    }


    void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "Enemy") {
            GetComponent<AudioSource>().Play();
            //pc.swordsummon = pc.swordsummon--;
            Physics.IgnoreCollision(GetComponent<BoxCollider>(), other.transform.GetComponent<BoxCollider>(), true);
            Destroy(gameObject, 0.5f);

        }

    }

    void OnCollisionStay(Collision other) {
        if (other.transform.tag == "Sword") {

            other.transform.GetComponent<SwordBehaviour>().rotatespd -= temp_speed;
            rotatespd += temp_speed;
        }
        if (other.transform.tag == "Enemy") {
            Destroy(gameObject);
        }
    }

    void OnCollisionExit(Collision other) {
        if (other.transform.tag == "Sword") {
            rotatespd = old_speed;

        }

    }

}
