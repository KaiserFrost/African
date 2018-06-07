using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSpawn : MonoBehaviour {

    private GameObject Jogador;

    void Start()
    {
        Jogador = GameObject.FindWithTag("Player");
    }
    void OnTriggerStay(Collider other)
    {
       
            Destroy(gameObject);
        
           
    }

   
}
