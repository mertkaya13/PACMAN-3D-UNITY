using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    public Rigidbody pacman;
    private static PlayerController instance;
    public int currentScore = 0;
    private float pacmanSpeed = 3.2f;

    private bool isUpKeyPressed = false;
    private bool isDownKeyPressed = false;
    private bool isRightKeyPressed = false;
    private bool isLeftKeyPressed = false;

    // Start is called before the first frame update

    public event EventHandler OnDied;
    public static PlayerController getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentScore = 0;
        SoundManager.getInstance().playMoveSound(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            isUpKeyPressed = true;
            isDownKeyPressed = false;
            isRightKeyPressed = false;
            isLeftKeyPressed = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            isRightKeyPressed = true;
            isDownKeyPressed = false;
            isUpKeyPressed = false;
            isLeftKeyPressed = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            isLeftKeyPressed = true;
            isDownKeyPressed = false;
            isUpKeyPressed = false;
            isRightKeyPressed = false;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            isDownKeyPressed = true;
            isUpKeyPressed = false;
            isRightKeyPressed = false;
            isLeftKeyPressed = false;
        }
    }

    private void FixedUpdate()
    {
        if (isUpKeyPressed)
        {
            pacman.position += Vector3.forward * pacmanSpeed * Time.deltaTime;
        }
        else if (isDownKeyPressed)
        {
            pacman.position += Vector3.back * pacmanSpeed * Time.deltaTime;
        }
        else if (isRightKeyPressed) 
        {
            pacman.position += Vector3.right * pacmanSpeed * Time.deltaTime;
        }
        else if (isLeftKeyPressed)
        {
            pacman.position += Vector3.left * pacmanSpeed * Time.deltaTime;

        }
    }

    public int getCurrentScore()
    {
        return currentScore;
    }
    void OnCollisionEnter(Collision other)
    {

    }

    private IEnumerator OnTriggerEnter(Collider other)
    {


        //If pacman collides with a ghost
        if (other.gameObject.layer == 9)
        {
            SoundManager.getInstance().playPacmanDiesSound(true);
            pacman.gameObject.SetActive(false);
            Score.TrySetNewHighscore(currentScore);
            if (OnDied != null) { OnDied(this, EventArgs.Empty); }

            //Rigid body of pacman
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }


        if(other.gameObject.layer == 13) 
        {


            SoundManager.getInstance().playPacmanEatsGhost(true);

            //Respawn ghost
            NavMeshAgent agentForDeadGhost = other.gameObject.GetComponent<NavMeshAgent>();
             GhostAI ghostAi = agentForDeadGhost.GetComponent<GhostAI>();

             //other.gameObject.transform.position = ghostAi.spawnPoint.position;
             other.gameObject.GetComponent<MeshRenderer>().material = MaterialHandler.getInstance().DeadColor;
             ghostAi.isOnWayToSpawn = true;
             ghostAi.waitOnSpawnPoint();
             currentScore += 100;
             other.gameObject.layer = 12;
             //StartCoroutine(ghostAi.waitOnSpawnPoint());

            
        }


        //If collided with a coin
        if (other.gameObject.layer == 10)
        {
            Destroy(other.gameObject);
            currentScore++;
        }

        //If collided with a powerup
        if (other.gameObject.layer == 11)
        {
            SoundManager.getInstance().playPacmanPowerUpSound(true);
            Destroy(other.gameObject);
            powerUpMode(true);
            yield return new WaitForSeconds(5);
            powerUpMode(false);
        }
    }


    private void powerUpMode(bool control)
    {
        LevelScript level = LevelScript.getInstance();
        //When this is true pacman can eat ghosts
        if (control)
        {
            level.makeGhostEdible(true);
        }
        else
        {
            level.makeGhostEdible(false);
        }
    }
}
