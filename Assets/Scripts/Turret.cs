using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform player;
    public GameObject bulletfps;
    public float bulletUp = .8f, bulletFwd = 1f, bulletRht = .1f, bulletSp = 100f;
    public AudioSource cannon;


    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("Launch", 2.0f, 0.7f);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "projectile")
        {
            Destroy(gameObject);
        }
    }

    void Launch()
    {
        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        if (Vector3.Distance(player.position, this.transform.position) < 10 && angle < 60)
        {
            direction.y = 0;
            //Uses the distance between the enemy and the player and rotate to the player's position.
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                      Quaternion.LookRotation(direction), 0.8f);
            cannon.Play();
            GameObject nb = (GameObject)Instantiate(bulletfps, transform.position + bulletFwd * transform.forward + bulletUp * transform.up, transform.rotation);
            nb.GetComponent<Rigidbody>().velocity = transform.forward * bulletSp;
            
        }

    }
}
