using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piranha : MonoBehaviour {
	public GameObject game_manager;
    public int health;
    public float speed;
    public float scoreValue = 200;
    private float current_time;
    private Rigidbody2D m_rigidbody;
    private GameObject player1;
    private GameObject player2;
    private Vector2 delta1;
    private Vector2 delta2;
    // Use this for initialization
    void Start () {
		game_manager = GameObject.FindGameObjectWithTag ("gamemanager");
        m_rigidbody = GetComponent<Rigidbody2D>();
        current_time = Time.time;
        if(GameObject.FindGameObjectWithTag("Player1"))
            player1 = GameObject.FindGameObjectWithTag("Player1");
        if(GameObject.FindGameObjectWithTag("Player2"))
            player2 = GameObject.FindGameObjectWithTag("Player2");
        delta1 = Vector2.left;
        delta2 = Vector2.left;
    }
	
	// Update is called once per frame
	void Update () {
        if (current_time + 7f <= Time.time)
        {
            Vector2 movement = Vector2.left * speed * Time.deltaTime;
            m_rigidbody.MovePosition(m_rigidbody.position + movement);
        }
        else
        {
            Vector2 movement;
            if (GameObject.FindGameObjectWithTag("Player1"))
            {
                player1 = GameObject.FindGameObjectWithTag("Player1");
                delta1 = player1.transform.position - transform.position;
            }
            else
                delta1 = delta2;
            if (GameObject.FindGameObjectWithTag("Player2"))
            {
                player2 = GameObject.FindGameObjectWithTag("Player2");
                delta2 = player2.transform.position - transform.position;
            }
            else
                delta2 = delta1;
            if (delta1.x >= delta2.x && delta1.y >= delta2.y && GameObject.FindGameObjectWithTag("Player1"))
                movement = delta1.normalized * speed * Time.deltaTime;
            else if (GameObject.FindGameObjectWithTag("Player2"))
                movement = delta2.normalized * speed * Time.deltaTime;
            else
                movement = Vector2.left;
            m_rigidbody.MovePosition(m_rigidbody.position + movement);
        }
        
	}

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
			GameObject.Find ("BackgroundAudio").GetComponents<AudioSource> () [4].Play ();

            Destroy(other.gameObject);
            if (--health <= 0)
            {
				game_manager.GetComponent<GameManagerScript> ().score += 801;
                //scoreChecker.IncreaseScore(scoreValue);
                Destroy(this.gameObject);
            }
        }

        if (other.gameObject.tag == "Bullet Enhanced")
        {
			GameObject.Find ("BackgroundAudio").GetComponents<AudioSource> () [4].Play ();

            Destroy(other.gameObject);
            if ((health = health - 3) <= 0)
            {
				game_manager.GetComponent<GameManagerScript> ().score += 801;
                Destroy(this.gameObject);
            }
        }

        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {
			GameObject.Find ("BackgroundAudio").GetComponents<AudioSource> () [3].Play ();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
