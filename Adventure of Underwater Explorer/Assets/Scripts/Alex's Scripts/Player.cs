using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float speed = 10f;
    public int health = 5;
    Vector2 movement;
    private Rigidbody2D m_rigidbody;
    public GameObject Bullet;
    public float ShootDelay = 1.25f;
    private float nextShot = 0.0f;
    private ScoreChecker scoreChecker;
    // Use this for initialization
    void Start () {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Shoot();
        Move();
    }

    private void FixedUpdate()
    {
    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "LeftWall")
        {
            scoreChecker.GameOver = true;
            health = 0;
            Destroy(this.gameObject);
            scoreChecker.DisplayHealth(0);
        }

        else if (other.gameObject.tag == "RightWall")
        {
            scoreChecker.GameOver = true;
            health = 0;
            Destroy(this.gameObject);
            scoreChecker.DisplayHealth(0);
        }

        else if (other.gameObject.tag == "Enemy Bullet")
        {
            Destroy(other.gameObject);
            if (--health <= 0)
            {
                scoreChecker.GameOver = true;
                Destroy(this.gameObject);
            }
            scoreChecker.DisplayHealth(health);
        }
        else if(other.gameObject.tag == "Enemy")
        {
            scoreChecker.GameOver = true;
            health = 0;
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            scoreChecker.DisplayHealth(0);
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && nextShot <= Time.time)
        {
            nextShot = ShootDelay + Time.time;
            Instantiate(Bullet, transform.position, Quaternion.identity);
        }

    }
    private void Move()
    {
        Vector2 input = Vector2.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            input = Vector2.left;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            input = Vector2.right;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            input = Vector2.up;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            input = Vector2.down;
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("AlexScene");
        }

        input.Normalize();

        movement = input * speed * Time.deltaTime;
        m_rigidbody.MovePosition(m_rigidbody.position + movement);
    }
}
