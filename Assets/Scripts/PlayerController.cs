using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Holds a reference to the UI Text GameObject.
    public Text countText;
    //Tells the compiler how many seconds the timer will count down.
    public float timeStart = 60;
    //holds a reference to the textbox game object.
    public Text textBox;
    public AudioSource collectSound, Gunfire, Injured;
    public float timeEnd = 60;
    public GameObject bulletfps;
    Camera eyes;
    //Needed to create bullet.
    public float bulletUp= .8f, bulletFwd = 1f, bulletRht = .1f, bulletSp = 100f;
    public Vector3 posInit = new Vector3(13f, 1.542f, -36f), gunInit = new Vector3(-.03f, 0.24f, 0.62f);
    public int bulletcount = 0, enemyCount, hitcount;
    //Needed to create and animate enemy.
    public GameObject Enemy;
    public float enemyMoveTime = 4f, enemyMoveTimeLeft, enemyMoveSpeed;
    Animator enemy1Animator;
    public bool walk, rest, attack, dead, getHit;
    float timeLeft;
    public GameObject[] collectables;
    public Slider MyHealthBar;
    public float MaxHealth = 100;
   // public GameObject nb;
   // bool isPlayed = false;


    private int count;
    
    // Start is called before the first frame update
    void Start()
    {
        collectables = GameObject.FindGameObjectsWithTag("Pick Up");
        count = 0;
        //Updates the text object countText everytime that an object is collected.
        SetCountText();
        //converts variable timeStart to a string type, so that
        // it can be added to textBox
        textBox.text = timeStart.ToString();
        eyes = Camera.main;
        timeLeft = 3.0f;
        hitcount = bulletcount = enemyCount = 0;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            bulletcount++;
            if (bulletcount % 4 == 0)
            {
                GameObject nb = (GameObject)Instantiate(bulletfps, transform.position + bulletFwd * transform.forward + bulletUp * transform.up, transform.rotation);
                nb.GetComponent<Rigidbody>().velocity = eyes.transform.forward * bulletSp;
                Gunfire.Play();
            }
        }

        //Conditional if-else statement to check if the timer has reached 0 seconds.
        if (timeStart >= 0)
        {
            timeStart -= Time.deltaTime;
            //Updates textBox and rounds value using mathf.Round
            textBox.text = "TIME LEFT: " + Mathf.Round(timeStart).ToString();
        }
        else
        {
            SceneManager.LoadScene(3);
        }
        
    }

    //Called when the player touches other trigger collider.
    //gameobject and all its components will be deactivated.
    void OnTriggerEnter(Collider other)
    {
        //if the player collides with gameobject with tag Pick up it will deactivate it.
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            //Adding +1 each time an object is collected.
            count += 1;
            SetCountText();
            collectSound.Play();
        }  
        if (other.gameObject.CompareTag("Enemy"))
        {
            MyHealthBar.value -= 20;
            Injured.Play();
            if (MyHealthBar.value <= 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void SetCountText()
    {
        countText.text = "SCORE: " + count.ToString() + "/" + collectables.Length; 
        if (count >= collectables.Length)
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);             
        }
    }
}
