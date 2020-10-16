using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    public Rigidbody pacman;
    private static PlayerController instance;
    public int currentScore;
    private bool inPowerUp;
    private int pacmanSpeed = 5;

    // Start is called before the first frame update

    public event EventHandler OnDied;
    public static PlayerController getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        inPowerUp = false;
    }
    void Start()
    {
        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        pacman.velocity = new Vector3(horizontalInput*pacmanSpeed, pacman.velocity.y, pacman.velocity.z);
        pacman.velocity = new Vector3(pacman.velocity.x, pacman.velocity.y, verticalInput* pacmanSpeed);
    }

    public int getCurrentScore()
    {
        return currentScore;
    }
    void OnCollisionEnter(Collision other)
    {
        Rigidbody ghost;
        //If pacman collides with a ghost
        if(other.gameObject.layer == 9)
        {

            //Pacman dies
            if (!inPowerUp) { 
                if (OnDied != null) { OnDied(this, EventArgs.Empty); }

                //Rigid body of pacman
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }


            //Ghost Dies
            if (inPowerUp)
            {
                //Destroy pacman
                Destroy(other.gameObject);
            }

        }
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        //If collided with a coin
        if (other.gameObject.layer == 10)
        {
            Destroy(other.gameObject);
            currentScore++;
        }

        //If collided with a coin
        if (other.gameObject.layer == 11)
        {
            Destroy(other.gameObject);
            powerUpMode(true);
            yield return new WaitForSeconds(5);
            powerUpMode(false);
        }
    }


    private void powerUpMode(bool control)
    {
        Debug.Log("POWERUP");
        LevelScript level = LevelScript.getInstance();
        //When this is true pacman can eat ghosts and becomes faster
        if (control)
        {
            pacmanSpeed = 8;
            level.makeGhostEdible(true);
            inPowerUp = true;
        }
        else
        {
            pacmanSpeed = 5;
            level.makeGhostEdible(false);
            inPowerUp = false;
        }
    }
}
