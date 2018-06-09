using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour {

    public int respawnTime = 1000;

    private int food = 5;
    private GameObject player; 
    
    void OnTriggerStay(Collider other)
    {
        player.GetComponent<SistemaStatus>().FomeAtual += food;
        //pa por a fruta "invisivel"
        this.GetComponent<SphereCollider>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;
        

        Invoke("Respawn", 5000);//Chamar a funçao de respawn, num determinado tempo
    }

    void Respawn()//mete visible
    {
        this.GetComponent<SphereCollider>().enabled = true;
        this.GetComponent<MeshRenderer>().enabled = true;
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
}
