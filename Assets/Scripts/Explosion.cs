using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int hitcount;
    public bool gotHit;
    public GameObject explosionPrefab;
    public AudioSource hurt, pain;
    // Start is called before the first frame update
    void Start()
    {
        hitcount = 0;
        gotHit = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet2Tag")
        {
            hitcount++;
            
            gotHit = true;
            if (hitcount >= 3)
            {
                hurt.Play(); Invoke("Explode", 0.1f);
            }
            else pain.Play();
            Invoke("offHit", 0.1f);
        }
    }
    void offHit()
    {
        gotHit = false;
    }
    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
