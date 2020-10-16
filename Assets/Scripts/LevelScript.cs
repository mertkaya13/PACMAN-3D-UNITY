using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public Rigidbody ghost1;
    public Rigidbody ghost2;
    public Rigidbody pacman;
    private static LevelScript instance;

    public Material defaultGhost1Color;
    public Material defaultGhost2Color;
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

        Destroy(pacman.gameObject);
        Destroy(ghost1.gameObject);
        Destroy(ghost2.gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ghost1 == null && ghost2 == null)
        {
            gameEnd(this, EventArgs.Empty);
        }
        if(PlayerController.getInstance().getCurrentScore() == 150)
        {
            gameEnd(this,EventArgs.Empty);
        }
    }

    internal void makeGhostEdible(bool control)
    {
        if (control) { 
             ghost1.gameObject.GetComponent<MeshRenderer>().material = edibleGhostColor;
             ghost2.gameObject.GetComponent<MeshRenderer>().material = edibleGhostColor;
        }
        else
        {
            ghost1.gameObject.GetComponent<MeshRenderer>().material = defaultGhost1Color;
            ghost2.gameObject.GetComponent<MeshRenderer>().material = defaultGhost2Color;
        }
    }
}
