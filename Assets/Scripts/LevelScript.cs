using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelScript : MonoBehaviour
{
    public Rigidbody ghost1;
    public Rigidbody ghost2;
    public Rigidbody ghost3;
    public Rigidbody ghost4;
    public Rigidbody pacman;
    private static LevelScript instance;

    public Material defaultGhost1Color;
    public Material defaultGhost2Color;
    public Material defaultGhost3Color;
    public Material defaultGhost4Color;
    public Material edibleGhostColor;
    // Start is called before the first frame update

    public static LevelScript getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;

    }

    void Start()
    {
        PlayerController.getInstance().OnDied += gameEnd;
    }

    private void gameEnd(object sender, EventArgs e)
    {
        ghost1.constraints = RigidbodyConstraints.FreezeAll;
        ghost2.constraints = RigidbodyConstraints.FreezeAll;
        pacman.constraints = RigidbodyConstraints.FreezeAll;

    }

    // Update is called once per frame
    void Update()
    {


        MaterialHandler matHandler = MaterialHandler.getInstance();

        if (ghost1.gameObject.layer == 13)
            ghost1.gameObject.GetComponent<MeshRenderer>().material = matHandler.PowerUpColor;

        if (ghost2.gameObject.layer == 13)
            ghost2.gameObject.GetComponent<MeshRenderer>().material = matHandler.PowerUpColor;
                                                                      
        if (ghost3.gameObject.layer == 13)                            
            ghost3.gameObject.GetComponent<MeshRenderer>().material = matHandler.PowerUpColor;
                                                                      
        if (ghost4.gameObject.layer == 13)                            
            ghost4.gameObject.GetComponent<MeshRenderer>().material = matHandler.PowerUpColor;

        if (ghost1.gameObject.layer == 12)
            ghost1.gameObject.GetComponent<MeshRenderer>().material = matHandler.DeadColor;

        if (ghost2.gameObject.layer == 12)
            ghost2.gameObject.GetComponent<MeshRenderer>().material = matHandler.DeadColor;

        if (ghost3.gameObject.layer == 12)
            ghost3.gameObject.GetComponent<MeshRenderer>().material = matHandler.DeadColor;

        if (ghost4.gameObject.layer == 12)
            ghost4.gameObject.GetComponent<MeshRenderer>().material = matHandler.DeadColor;

        if (ghost1.gameObject.layer == 9)
        {
            ghost1.gameObject.GetComponent<MeshRenderer>().material = defaultGhost1Color;
        }
        if (ghost2.gameObject.layer == 9)
        {
            ghost2.gameObject.GetComponent<MeshRenderer>().material = defaultGhost2Color;
        }
        if (ghost3.gameObject.layer == 9)
        {
            ghost3.gameObject.GetComponent<MeshRenderer>().material = defaultGhost3Color;
        }
        if (ghost4.gameObject.layer == 9)
        {
            ghost4.gameObject.GetComponent<MeshRenderer>().material = defaultGhost4Color;
        }
    }

    internal void makeGhostEdible(bool control)
    {
        if (control) 
        { 

            ghost1.gameObject.layer = 13;
            ghost2.gameObject.layer = 13;
            ghost3.gameObject.layer = 13;
            ghost4.gameObject.layer = 13;

            ghost1.gameObject.GetComponent<MeshRenderer>().material = edibleGhostColor;
            ghost2.gameObject.GetComponent<MeshRenderer>().material = edibleGhostColor;
            ghost3.gameObject.GetComponent<MeshRenderer>().material = edibleGhostColor;
            ghost4.gameObject.GetComponent<MeshRenderer>().material = edibleGhostColor;

        }
        else
        {
            if (ghost1.gameObject.layer != 12)
            {
                ghost1.gameObject.layer = 9;
                ghost1.gameObject.GetComponent<MeshRenderer>().material = defaultGhost1Color;
            }
            if (ghost2.gameObject.layer != 12)
            {
                ghost2.gameObject.layer = 9;
                ghost2.gameObject.GetComponent<MeshRenderer>().material = defaultGhost2Color;
            }
            if (ghost3.gameObject.layer != 12)
            {
                ghost3.gameObject.layer = 9;

                ghost3.gameObject.GetComponent<MeshRenderer>().material = defaultGhost3Color;
            }
            if (ghost4.gameObject.layer != 12)
            {
                ghost4.gameObject.layer = 9;
                ghost4.gameObject.GetComponent<MeshRenderer>().material = defaultGhost4Color;
            }
            
        }
    }
}
