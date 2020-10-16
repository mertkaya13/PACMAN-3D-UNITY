using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    public Rigidbody pacman;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        pacman.velocity = new Vector3(horizontalInput*30, pacman.velocity.y, pacman.velocity.z);
        pacman.velocity = new Vector3(pacman.velocity.x, pacman.velocity.y, verticalInput*30);
    }
}
