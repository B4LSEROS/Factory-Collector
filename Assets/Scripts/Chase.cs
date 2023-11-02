using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public Transform player;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Direction from the player to the enemy.
        Vector3 direction = player.position - this.transform.position;

        //Tests between the distance between the player and the enemy.
        //If the distance is less than ten, the enemy will follow around the player.
        if (Vector3.Distance(player.position, this.transform.position) < 10)
        {
            direction.y = 0;

            //Uses the distance between the enemy and the player and rotate to the player's position.
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                      Quaternion.LookRotation(direction), 0.5f);

            //When player gets close to enemy isIdle is set to false.
            anim.SetBool("isIdle", false);

            //Makes the enemy move forward
            //if the magnitude of the vector is greater than 5, the enemy will be translated
            if (direction.magnitude > 3)
            {
                this.transform.Translate(0, 0, 0.05f);
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);
            }
            //If player is closer than 5, the enemy will attack.
            else
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
            }
        }
        //If the two if-statements above are set to false, the enemy will set to idle.
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
        }
    }

}
