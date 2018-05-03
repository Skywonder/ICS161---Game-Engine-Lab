using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    public Vector2 direction = Vector2.zero;
    public float speed = 1f;
    public float speed_up = 1.5f;
    public Vector2 playerposboundarymin;
    public Vector2 playerposboundarymax;
    public GameObject sw_prefab;
    public float cd;
    public float cdtimer;
    public int swordsummon = 0;
    public int swordcount = 3;
    public int takedmg = 5;
    public int scount;
    public Animator animator;
    //public float dmgcd = 0 ;
    //public float dmgtimer = 0;
    public bool invincible = false;
    public bool timeout = false;
    public bool hit = false;
    public int health = 100; //health
    public GameObject Trail1;
    public GameObject Trail2;
    public GameObject[] sword_list;
    public GameObject enemy;
    public AudioSource source1;
    public AudioSource source2;
    public float distance;
    

    //public GameObject[] swords;
    //public List<GameObject> sword_list = new List<GameObject>(); 
    public int face_offset = 10; 
	// Use this for initialization
	void Awake () {
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        sword_list = null;

        if (invincible) {
            if (timeout) {
                StartCoroutine(BeginTimeOut());
            }
        }

        //update the sword count
        if (Time.timeScale == 1)
        {

            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            //direction = Vector2.zero;
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");


            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                GetComponentInChildren<SpriteRenderer>().flipX = false;
                Trail1.SetActive(true);
                Trail2.SetActive(false);
                face_offset = 10;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                GetComponentInChildren<SpriteRenderer>().flipX = true;
                Trail2.SetActive(true);
                Trail1.SetActive(false);
                face_offset = -10;

            }


            if ((Input.GetKey(KeyCode.LeftShift)|| Input.GetKey(KeyCode.RightShift))&& Input.anyKey)
            {
                float nspeed = speed * speed_up;
                direction = new Vector2(inputX * nspeed, inputY * nspeed);
            }
            else
            {

                direction = new Vector2(inputX * speed, inputY * speed);
            }

            CreateSword(face_offset);
            UseSword(face_offset);

        }
    }

    GameObject SummonSword(int face_offset){
        Vector3 offset = new Vector3(face_offset, 0, 0);
        GameObject sword = Instantiate(sw_prefab, transform.position + offset, Quaternion.identity) as GameObject;
        sword.AddComponent<BoxCollider>();
        sword.AddComponent<Rigidbody>().useGravity = false;
        sword.GetComponent<Transform>().position = new Vector3(sword.GetComponent<Transform>().position.x, sword.GetComponent<Transform>().position.y, 0);
        sword.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ|RigidbodyConstraints.FreezeRotationX;
        //sword.transform.parent = this.transform;
        return sword;
    }

    void CreateSword(int face_offset) {
        sword_list = GameObject.FindGameObjectsWithTag("Sword");
        swordsummon = sword_list.Length;
        if (Input.GetKey(KeyCode.F) && cdtimer <= Time.time && swordsummon < swordcount)
        {
            source1.Play();
            GameObject sword = SummonSword(face_offset);
            cdtimer = Time.time + cd;
        }
    }


    void UseSword(int face_offset) {
        sword_list = GameObject.FindGameObjectsWithTag("Sword");
        //take the sword from the top and fire it
        if (Input.GetKey(KeyCode.Space) && cdtimer <= Time.time && sword_list.Length > 0)
        { //if there are swords...fire it out then remove it
            // Play Animation
            //animator.SetTrigger("Attack");
            //
            
            Debug.Log("Sword count: " + sword_list.Length);

            sword_list[0].transform.position = new Vector3(transform.position.x + face_offset, transform.position.y, 0);//ready sword
            sword_list[0].GetComponent<SwordBehaviour>().not_used = false;//fire it

            //sword_list.Remove(sword_list[0]);
            //swordsummon--;
            cdtimer = Time.time + cd;
            // Play Animation
            animator.SetTrigger("Attack");
            source2.Play();

            //
        }
        swordsummon = sword_list.Length;
    }

    void FixedUpdate() {


        GetComponent<Rigidbody>().velocity = direction;
        //GetComponent<Rigidbody>().AddForce(direction * speed);
        

    }

    void LateUpdate() {
       
    }


    void OnCollisionEnter(Collision other) {

        if (other.transform.tag == "Enemy") {
            enemy = other.gameObject;
            timeout = true;
            hit = true;
            health -= enemy.GetComponent<EnemyAI>().damage;
            invincible = true;
            animator.SetTrigger("Damaged");
            Color tmp = GetComponentInChildren<SpriteRenderer>().color;
            tmp.a = 0.1f;
            GetComponentInChildren<SpriteRenderer>().color = tmp;
            Physics.IgnoreCollision(GetComponent<BoxCollider>(), other.transform.GetComponent<BoxCollider>(), true);
        }    
    }

    IEnumerator BeginTimeOut() { 
       
        yield return new WaitForSeconds(1f);
        Debug.Log("Get here");
        invincible = false;
        timeout = false;
        Color tmp = GetComponentInChildren<SpriteRenderer>().color;
        tmp.a = 1f;
        GetComponentInChildren<SpriteRenderer>().color = tmp;
        if(enemy != null) { 
        Physics.IgnoreCollision(GetComponent<BoxCollider>(), enemy.transform.GetComponent<BoxCollider>(), false);
            //GetComponent<Rigidbody>().isKinematic = false;
        }
    }





}
