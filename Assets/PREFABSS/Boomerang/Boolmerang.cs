using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boolmerang : MonoBehaviour {

    bool go;

    GameObject player;
    GameObject Boomerang;

    Transform itemToRotate;

    Vector3 posFrentePlayer;
    

    // Use this for initialization
    void Start()
    {
        go = false; 

        //Fiz com tags pa ser mais facil
        player = GameObject.FindGameObjectWithTag("Player");
        Boomerang = GameObject.FindGameObjectWithTag("Boom");

        Boomerang.GetComponent<MeshRenderer>().enabled = false; 
        itemToRotate = gameObject.transform.GetChild(0);


        Ray ray = Camera.main.ScreenPointToRay(Camera.main.transform.position);
        RaycastHit hit;


        //Se for preciso controlar a altura ou até mesmo a posiçao em si, mas acho q ta bom assim
        posFrentePlayer = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z) + player.transform.forward * 15f;
        
        StartCoroutine(Boom());
    }

    IEnumerator Boom()
    {
        go = true;
        yield return new WaitForSeconds(1.5f);
        go = false;
    }


    // Update is called once per frame
    void Update()
    {
        
            itemToRotate.transform.Rotate(0, Time.deltaTime * 600, 0); //Roda o boomerang
         // haver se ele manda apenas um...
            if (go)
            {
                //transform.position = Vector3.MoveTowards(transform.position, posFrentePlayer, Time.deltaTime * 40); //muda a poosiçao pa frente do player           

            transform.position = transform.position + Camera.main.transform.forward * 1.5f;

             }

            if (!go)
            {
                
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z), Time.deltaTime * 40); //Volta po dono
            }

            if (!go && Vector3.Distance(player.transform.position, transform.position) < 1.5)
            {
                Boomerang.GetComponent<MeshRenderer>().enabled = true;
                Destroy(this.gameObject);
            }
        
        
    }
}
