using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    //public KeyCode key;
	public string key;
    private float damage = 1.0f;
	private float speed = 10.0f;
    private float duration = 6.0f;
    private float rate = 1.0f;

	private int shotAngle = 0;

    private bool canShoot;
    private float countdown;

	public GameObject bullet;
	public Transform bullet_spawn;
	public AudioSource shoot_sfx;

	void Start () {        
        // Support different bullet types by getting data from BulletModifier
        // Hidden default values are included just incase
		shotAngle = this.gameObject.GetComponent<PlayerController>().m_Switch;
        if (bullet.gameObject.GetComponent<BulletModifier>()){
            damage = 	bullet.gameObject.GetComponent<BulletModifier>().damage;
            speed = 	bullet.gameObject.GetComponent<BulletModifier>().speed;
            duration = 	bullet.gameObject.GetComponent<BulletModifier>().duration;
            rate = 		bullet.gameObject.GetComponent<BulletModifier>().rate;
        }
        canShoot = true;
		//shoot_sfx = GetComponent<AudioSource> ();
    }

    void Update () {
		shotAngle = this.gameObject.GetComponent<PlayerController>().m_Switch;
        
        if (Input.GetButtonDown (key) && canShoot) {
			UpdateBullet();
			ShootBullet();
            ShootDelay();
		}
        if (countdown > 0) { 
            countdown -= Time.deltaTime;
            if( countdown <= 0) {
                canShoot = true;
            }
        }
	}

	private void ShootBullet(){
		var bullet_obj = Instantiate(bullet, bullet_spawn.position, bullet_spawn.rotation);
        bullet_obj.GetComponent<BulletModifier>().parent = this.gameObject.tag;
		shoot_sfx.Play ();

		switch (shotAngle) {
            case 1:
    			bullet_obj.transform.Rotate (0, 0, -30);
	    		break;
		    case 2:
                bullet_obj.transform.Rotate (0, 0, 30);
			    break;
		    default:
                bullet_obj.transform.Rotate (0, 0, 0);
			    break;
		}
       
        bullet_obj.GetComponent<Rigidbody2D>().velocity = bullet_obj.transform.right * speed;
        Destroy(bullet_obj, duration);
	}
    
	private void ShootDelay() {
        canShoot = false;
        countdown = 1 / rate;
    }

	private void UpdateBullet(){
		if (bullet.gameObject.GetComponent<BulletModifier>()){
			damage = 	bullet.gameObject.GetComponent<BulletModifier>().damage;
			speed = 	bullet.gameObject.GetComponent<BulletModifier>().speed;
			duration = 	bullet.gameObject.GetComponent<BulletModifier>().duration;
			rate = 		bullet.gameObject.GetComponent<BulletModifier>().rate;
		}
	}
}