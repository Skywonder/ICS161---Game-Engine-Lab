using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public float y_direction = 0f;
    public float x_direction = -15f;
    private Rigidbody2D m_rigidbody;
    public GameObject m_bullet;
	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_rigidbody.MoveRotation(90f);
        m_rigidbody.velocity = new Vector2(x_direction, y_direction);
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {
			GameObject.Find ("BackgroundAudio").GetComponents<AudioSource> () [3].Play ();
            Destroy(other.gameObject);
            Destroy(this.gameObject);            
        }
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
