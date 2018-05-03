using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletModifier : MonoBehaviour {
    public float damage = 1.0f;
    public float speed = 10.0f;
    public float duration = 6.0f;
    public float rate = 1.0f;
    public string parent;

    public GameObject bulletPlus;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if (this.gameObject.tag == "Bullet" && other.gameObject.tag == "Bullet"){
            string parent_other = other.gameObject.GetComponent<BulletModifier>().parent;
            if (parent != parent_other) {
                Destroy(this.gameObject);

                if (GetInstanceID() < other.gameObject.GetInstanceID()) {
                    var bulletPlus_obj = Instantiate(bulletPlus, transform.position, Quaternion.Euler(new Vector2(0, 0)));
                    var bulletPlus_spd = bulletPlus_obj.GetComponent<BulletModifier>().speed;
                    var bulletPlus_dur = bulletPlus_obj.GetComponent<BulletModifier>().duration;
                    bulletPlus_obj.GetComponent<Rigidbody2D>().velocity = bulletPlus_obj.transform.right * bulletPlus_spd;
                    Destroy(bulletPlus_obj, bulletPlus_dur);
                }
            }
        }
    }		
}
