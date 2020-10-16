// MoveTo.cs
using System;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{

    private NavMeshAgent agent;
    public Transform ghost;
    public Transform target;
    public Transform[] patrolPoints;
    public int destinationIndex = 0;

    //Checks if it is going 0->9 or 9->0 ?
    private bool isOnCycle = false;

    //Is ghost chasing Pacman?
    private bool isOnChase = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        goToNextPatrolPoint();
    }


    //Is distance close enough to start chase?
    private void chaseControl()
    {
        if (Vector3.Distance(ghost.position, target.position) < 8)
        {
            Debug.Log(Vector3.Distance(ghost.position, target.position));
            isOnChase = true;
        }
        else
        {
            isOnChase = false;
        }
    }

    private void goToNextPatrolPoint()
    {

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


        if (isOnCycle)
        {
            destinationIndex--;
        }
        else
        {
            destinationIndex++;
        }

        //Setting the target point
        agent.destination = patrolPoints[destinationIndex].position;
    }

    private void Update()
    {
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