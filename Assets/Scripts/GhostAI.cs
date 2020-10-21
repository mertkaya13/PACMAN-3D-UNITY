// MoveTo.cs
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{

    private NavMeshAgent agent;
    public Transform ghost;
    public Transform target;
    public Transform[] patrolPoints;
    public int destinationIndex = 0;
    public Transform spawnPoint;
    public Material defaultColor;

    //Checks if it is going 0->9 or 9->0 ?
    private bool isOnCycle = false;

    //Is ghost chasing Pacman?
    private bool isOnChase = false;

    //Should ghost wait onSpawn?
    public bool isOnWayToSpawn = false;

    private static GhostAI instance;

    public static GhostAI getInstance()
    {
        return instance;
    }

    void Start()
    {
        NavMesh.avoidancePredictionTime = 0.5f;
        agent = GetComponent<NavMeshAgent>();
        goToNextPatrolPoint();
    }


    //Is distance close enough to start chase?
    private void chaseControl()
    {

        //Will calculate distance to Pacman using the offmesh links too.
        //If distance is too far does not chase and continues to patrol
        //NavMeshPath calculatedPathToPacman = new NavMeshPath();
        //NavMesh.CalculatePath(ghost.position, target.position, NavMesh.AllAreas, calculatedPathToPacman);
        //NavMeshPath temp = agent.path;
        //agent.path = calculatedPathToPacman;


        if (Vector3.Distance(ghost.position, target.position) < 13)
        {
            //Debug.Log(Vector3.Distance(ghost.position, target.position));
            isOnChase = true;
            agent.destination = target.position;
        }
        else
        {
            isOnChase = false;
        }
    }

    public void waitOnSpawnPoint()
    {
        agent.destination = spawnPoint.position;
        isOnWayToSpawn = true;
        //this.gameObject.layer = 12;
    }

    private void Awake()
    {
        instance = this;

        //Setting default color
        defaultColor = this.gameObject.GetComponent<MeshRenderer>().material;
    }
    private void goToNextPatrolPoint()
    {

        //Waits 3 seconds in spawn;
        if (isOnWayToSpawn)
        {
            if (agent.remainingDistance < 2)
            {
                isOnWayToSpawn = false;
                this.gameObject.layer = 9;
                this.gameObject.GetComponent<MeshRenderer>().material = defaultColor;
            }
                return;
        }

        //Start Chase
        if (isOnChase)
        {
            agent.destination = target.position;
            return;
        }


        //Cycling thorugh patrol points
        if (destinationIndex == patrolPoints.Length - 1)
        {
            //end point of patrol
            isOnCycle = true;
        }else if (destinationIndex == 0)
        {
            //start point of patrol
            isOnCycle = false;
        }


        if(agent.remainingDistance < 2)
        {
            if (isOnCycle)
            {
                destinationIndex--;
            }
            else
            {
                destinationIndex++;
            }
        }


        //Setting the target point
        agent.destination = patrolPoints[destinationIndex].position;
    }

    private void Update()
    {
        
        if (isOnWayToSpawn)
        {
            goToNextPatrolPoint();
            this.gameObject.GetComponent<MeshRenderer>().material = MaterialHandler.getInstance().DeadColor;
            return;
        }


        chaseControl();
        if (isOnChase) { 
            agent.destination = target.position;
            return;
        }

        //If the path point is not achieved do not change it.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            goToNextPatrolPoint();
        
    }
}