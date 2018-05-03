using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 3;
    public float jspeed = 4;
    public float mjspeed = 10;
    public float aspeed = 0.5f;
    private float timeStamp = 0;
    public float jumpcount = 0;
    public float cdtimer = 0.5f;
    public bool isGrounded = false;
    public bool isAir = false;
    public bool canDoubleJump = false;
    public Vector3 gravity = new Vector3(0,-9.8f,0);
    public Vector3 velocity = Vector3.zero;
    public Vector3 direction;
    public GameObject attractedTo;
    public float strengthOfAttraction = 5f;
    public bool pulled = false;
    public GameObject startposition;
    public AudioSource source;
    public AudioClip clip;


    void Awoke() {
        if (attractedTo == null)
            {
                Debug.Log("no gravity found");
            }
        if (startposition = null)
            {
                startposition = GameObject.FindGameObjectWithTag("Start");
            }
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log("startposition" + startposition);
        startposition = GameObject.FindGameObjectWithTag("Start");
    }

    void Update() {

        
        Debug.Log("startposition" + startposition);

        Mathf.Clamp(velocity.y, -15f, 15f);
        velocity.x = 0;
        velocity.z = 0;
        if (Input.GetKey(KeyCode.W)) {
           velocity.z += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
           velocity.z -= 1;
        }       
        if (Input.GetKey(KeyCode.D)) {
           velocity.x += 1;      
        }       
        if (Input.GetKey(KeyCode.A)){
           velocity.x -= 1;
        }

        if (isAir == true && isGrounded == false)
        {
            //velocity.y = gravity;
            velocity += gravity * Time.deltaTime;
        }


        if (transform.position.y < -20)
        {
            //Time.timeScale = 0;
            Debug.Log("startposition" + startposition);
            //respawn automatically at start position;
            velocity = Vector3.zero;
            this.transform.position = startposition.transform.position;

        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        
        transform.position += velocity * speed * Time.deltaTime;
        
        jump();

        
    }

    void AirJump() {
        
            velocity.y = 0;
            velocity = ( Vector3.up * jspeed);
            GetComponent<Rigidbody>().AddForce(velocity, ForceMode.Impulse);
    }

    void jump() {
        Vector3 up = Vector3.zero;
       
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            source.PlayOneShot(clip);
            velocity = Vector3.up * jspeed;
            GetComponent<Rigidbody>().AddForce(velocity, ForceMode.Impulse);
            timeStamp = Time.time + cdtimer;
            jumpcount++;
        }
        else if (canDoubleJump && isAir && timeStamp <= Time.time && jumpcount < 2)
        {
            if (Input.GetKey(KeyCode.Space) && timeStamp <= Time.time)
            {
                source.PlayOneShot(clip);
                jumpcount++;
                AirJump();
                timeStamp = Time.time + cdtimer;

            }
        }   
    }
    

    void OnCollisionEnter(Collision obj){
        if (obj.transform.tag == "Ground") {
            velocity.y = 0;
            isGrounded = true;
            isAir = false;
            canDoubleJump = false;
            jumpcount = 0;
        }
    }

    void OnCollisionExit(Collision obj) {
        if (obj.transform.tag == "Ground")
        {
            isGrounded = false;
            isAir = true;
            canDoubleJump = true;
        }

    }
}
