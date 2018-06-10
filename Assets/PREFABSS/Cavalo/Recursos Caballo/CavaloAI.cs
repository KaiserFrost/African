using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;


public class CavaloAI : MonoBehaviour {

    public float velocidadeMov = 5.0f;
    public float velocidadeRot = 10.0f;
    public int direcaoMov = 1;
    private int tempo;
    private int tempo2;
    private int tempo3;
    public bool andar;
    public Animator anim;
    public CharacterController controller;
    public Vector3 localScale;

    public float gravidade = 20.0f;
    private Vector3 direcaoMovimento = Vector3.zero;


    // Use this for initialization
    void Start () {

        int random = UnityEngine.Random.Range(1, 20);
        tempo = UnityEngine.Random.Range(0, 1000);
        tempo2 = UnityEngine.Random.Range(0, 1000);//random.Range(0, 1000);
        tempo3 = UnityEngine.Random.Range(0, 1000);
    }
	
	// Update is called once per frame
	void Update ()
    {
		controller = GetComponent<CharacterController>();
        direcaoMovimento.y -= gravidade * Time.deltaTime;
        controller.Move(direcaoMovimento * Time.deltaTime);
        
        if(!Physics.Raycast(transform.position, transform.forward, 5))
            transform.Translate(Vector3.forward * velocidadeMov * Time.smoothDeltaTime);
        else
        {
            if (Physics.Raycast(transform.position, -transform.right, 1))
            {
                direcaoMov = 1;
            }
            else if (Physics.Raycast(transform.position, transform.right, 1))
            {
                direcaoMov = -1;
            }
            transform.Rotate(Vector3.up, 90 * velocidadeRot * Time.smoothDeltaTime * direcaoMov);

        }
        tempo -= (int)Time.deltaTime * 1;
        tempo2 -= (int)Time.deltaTime * 1;
        tempo3 -= (int)Time.deltaTime * 1;

        if (tempo <= 0)
        {
            tempo = UnityEngine.Random.Range(0, 1000);
        }
        if (tempo2 <= 0)
        {
            tempo2 = UnityEngine.Random.Range(0, 1000);
        }

        if (tempo3 <= 0)
        {
            tempo3 = UnityEngine.Random.Range(0, 1000);
        }

        if (tempo > 500)
        {
            andar = true;
            velocidadeMov = 2;
            anim.SetBool("Walk", true);
        }
        if (tempo < 300)
        {
            andar = false;
            velocidadeMov = 0;
            anim.SetBool("Walk", false);
        }
        if (tempo2 < 75 && andar == true)
        {
            transform.Rotate(Vector3.up, 90 * velocidadeRot * Time.smoothDeltaTime * direcaoMov);
        }
        if (tempo3 > 925 && andar == true)
        {
            transform.Rotate(Vector3.up, -90 * velocidadeRot * Time.smoothDeltaTime * direcaoMov);
        }
    }
}
