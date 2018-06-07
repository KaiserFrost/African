using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteBoomer : MonoBehaviour {

    public GameObject boomer;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {

            GameObject clone;
            clone = Instantiate(boomer, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation) as GameObject;
            //if (clone.transform.position) ;
        }
    }
}
