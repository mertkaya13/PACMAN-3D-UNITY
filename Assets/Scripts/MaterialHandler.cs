using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialHandler : MonoBehaviour
{
    public Material DeadColor;
    public Material PowerUpColor;

    // Start is called before the first frame update


    private static MaterialHandler instance;

    public static MaterialHandler getInstance()
    {
        return instance;
    }
    public enum Colours
    {
        DeadColor,
        PowerUpColor,
    }


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
