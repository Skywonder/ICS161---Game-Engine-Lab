using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Enemy : MonoBehaviour
{
	public GameObject game_manager;
    public int health;
    public float speed;
    public int scoreValue = 200;
    Vector2 change;
    private Rigidbody2D m_rigidbody;
    public GameObject Bullet;
    public float ShootDelay = 0.0f;
    private float nextShot = 0.0f;
    private float player1_position;
    private float player2_position;
    private float player1_position_x;
    private float player2_position_x;
    private float enemy_position;
    private float enemy_position_x;
    private float wait_time;
    float current_time;
    // Use this for initialization
    void Start()
    {
		game_manager = GameObject.FindGameObjectWithTag ("gamemanager");
        m_rigidbody = GetComponent<Rigidbody2D>();
        int start_direction = Random.Range(0, 2);
        if (start_direction == 0)
            change = Vector2.up;
        else
            change = Vector2.down;
        current_time = Time.time;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Time.time > current_time + 5f)
        {
            current_time = Time.time;
            change = Vector2.left; 
        }
        if (change == Vector2.left && Time.time > current_time + 1f)
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
        enemy_position = m_rigidbody.transform.position.y;
        enemy_position_x = m_rigidbody.transform.position.x;
        if (GameObject.FindGameObjectWithTag("Player1"))
        {
            player1_position = GameObject.FindGameObjectWithTag("Player1").transform.position.y;
            player1_position_x = GameObject.FindGameObjectWithTag("Player1").transform.position.x;
        }
        if (GameObject.FindGameObjectWithTag("Player2"))
        {
            player2_position = GameObject.FindGameObjectWithTag("Player2").transform.position.y;
            player2_position_x = GameObject.FindGameObjectWithTag("Player2").transform.position.x;
        }
        float shoot1 = player1_position - enemy_position;
        float shoot2 = player2_position - enemy_position;
        float shoot3 = player1_position_x - enemy_position_x;
        float shoot4 = player2_position_x - enemy_position_x;
        if (nextShot <= Time.time && ((shoot1 < 0 && shoot1 > -0.5) || (shoot1 > 0 && shoot1 < 0.5)) && shoot3 < 0)
        {
            Instantiate(Bullet, transform.position, Quaternion.identity);
            nextShot = ShootDelay + Time.time;
        }

        if (nextShot <= Time.time && ((shoot2 < 0 && shoot2 > -0.5) || (shoot2 > 0 && shoot2 < 0.5)) && shoot4 < 0)
        {
            Instantiate(Bullet, transform.position, Quaternion.identity);
            nextShot = ShootDelay + Time.time;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bullet")
        {
			GameObject.Find ("BackgroundAudio").GetComponents<AudioSource> () [4].Play ();
            Destroy(other.gameObject);
            if (--health <= 0)
            {
				game_manager.GetComponent<GameManagerScript> ().score += 501;
                Destroy(this.gameObject);
            }
        }

		if (other.gameObject.tag == "Bullet Enhanced") 
		{
			GameObject.Find ("BackgroundAudio").GetComponents<AudioSource> () [4].Play ();
			Destroy (other.gameObject);
			if ((health = health - 3) <= 0) 
			{
				game_manager.GetComponent<GameManagerScript> ().score += 501;
				Destroy (this.gameObject);
			}
		}

        if(other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {
			GameObject.Find ("BackgroundAudio").GetComponents<AudioSource> () [3].Play ();
            Destroy(other.gameObject);
            Destroy(this.gameObject);

        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
