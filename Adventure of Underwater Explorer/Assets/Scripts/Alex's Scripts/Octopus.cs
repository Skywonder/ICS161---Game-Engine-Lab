using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : MonoBehaviour {

	public GameObject game_manager;
    public int health;
    public float speed;
    public int scoreValue = 200;
    Vector2 change;
    public Color color1;
    public Color color2;
    private Rigidbody2D m_rigidbody;
    public GameObject Tentacle;
    private float current_time;
    public float next_shot;
    public float shoot_delay = 2;
    // Use this for initialization
    void Awake()
    {
        color1 = GetComponent<SpriteRenderer>().color;
        color2 = GetComponent<SpriteRenderer>().color;
        color1.a = 0.5f;
        color2.a = 1.0f;
    }
    void Start () {
		game_manager = GameObject.FindGameObjectWithTag ("gamemanager");
        m_rigidbody = GetComponent<Rigidbody2D>();
        int start_direction = Random.Range(0, 2);
        if (start_direction == 0)
            change = Vector2.up;
        else
            change = Vector2.down;
        current_time = Time.time;
        GetComponent<SpriteRenderer>().color = color2;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > current_time + 7f)
        {
            change = Vector2.left;
            current_time = Time.time;
        }   
        if(change == Vector2.left && Time.time > current_time + 1f)
        {
            int start_direction = Random.Range(0, 2);
            if (start_direction == 0)
                change = Vector2.up;
            else
                change = Vector2.down;
        }
        else if (m_rigidbody.transform.position.y >= 18.5)
            change = Vector2.down;
        else if (m_rigidbody.transform.position.y <= -20)
            change = Vector2.up;
        Vector2 movement = change * speed * Time.deltaTime;
        m_rigidbody.MovePosition(m_rigidbody.position + movement);
        if(next_shot - 2 <= Time.time)
            GetComponent<SpriteRenderer>().color = color1;
        if (next_shot <= Time.time)
        {
            Instantiate(Tentacle, transform.position, Quaternion.identity);
            next_shot = shoot_delay + Time.time;
            GetComponent<SpriteRenderer>().color = color2;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            if (--health <= 0)
            {
				game_manager.GetComponent<GameManagerScript> ().score += 1501;
                Destroy(this.gameObject);
            }
        }

        if (other.gameObject.tag == "Bullet Enhanced")
        {
            Destroy(other.gameObject);
            if ((health = health - 3) <= 0)
            {
				game_manager.GetComponent<GameManagerScript> ().score += 1501;
                Destroy(this.gameObject);
            }
        }

        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    
}
