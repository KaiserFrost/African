using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



[RequireComponent(typeof(NavMeshAgent))]//a cena do terreno
public class WolfAI : MonoBehaviour {


    public Animator animatorAnimal;
    public AnimatorClipInfo[] clip;

    private GameObject jogador;
    private GameObject inimigo;
    private NavMeshAgent navMesh;

    private bool isAlert;
    private bool isRunning;
    private bool isWallking;
    private bool isAttack;

    public int atkBase;//provisorio
    private Vector3 speed;

    public Collider atkCollider;


    // Use this for initialization
    void Start () {
        jogador = GameObject.FindWithTag("Player");
        inimigo = GameObject.FindWithTag("Enemy");
        navMesh = GetComponent<NavMeshAgent>();

        atkCollider = GetComponent<Collider>();
        atkCollider.isTrigger = false;

        speed = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        Following();//Começa a seguir o nego  

        UpdateParemetrosAnimator();
    }

    void UpdateParemetrosAnimator()
    {
        animatorAnimal.SetBool("Shout", isAlert);
        animatorAnimal.SetBool("Walk", isWallking);

        animatorAnimal.SetBool("Run", isRunning);
        animatorAnimal.SetBool("ATK", isAttack);
    }

    void Following()
    {
        if (Vector3.Distance(transform.position, jogador.transform.position) < 25f)
        {
            isAlert = true;
            clip = animatorAnimal.GetCurrentAnimatorClipInfo(0);
            if (isAlert)
            {
                isWallking = false;
                isRunning = false;
            }


            Debug.Log("ATENÇÃO!!!");
            if (Vector3.Distance(transform.position, jogador.transform.position) < 20f)
            {

                isAlert = false;
                clip = animatorAnimal.GetCurrentAnimatorClipInfo(0);
                if (isAlert == false)
                {
                    isWallking = true;
                    isRunning = false;
                }

                //diminuir a velocidade!!!!!
                navMesh.destination = jogador.transform.position;


                Debug.Log("ANDAR");
                if (Vector3.Distance(transform.position, jogador.transform.position) < 15f)
                {
                    isAlert = false;
                    clip = animatorAnimal.GetCurrentAnimatorClipInfo(0);
                    if (isAlert == false)
                    {
                        isWallking = false;
                        isRunning = true;
                    }

                    //diminuir a velocidade!!!!!
                    navMesh.destination = jogador.transform.position;
                    Debug.Log("CORRER");
                }

                if (Vector3.Distance(transform.position, jogador.transform.position) < 2f)
                {

                    isAlert = false;
                    clip = animatorAnimal.GetCurrentAnimatorClipInfo(0);
                    if (isAlert == false)
                    {
                        isWallking = false;
                        isRunning = false;
                        isAttack = true;
                    }
                    navMesh.destination = jogador.transform.position;
                    Debug.Log("ATACAR!!!");
                    Attack();
                }

            }

        }

    }


    //alterar a forma de ataque, sem usar coroutine
    void Attack()//Ta muito Basico... 
    {
        if (atkCollider.isTrigger == true)
        {
            //StartCoroutine("AttackTime");

            jogador.GetComponent<SistemaStatus>().VidaAtual -= atkBase;
        }
    }



    //IEnumerator AttackTime()
    //{
    //    isAttack = false;
    //    yield return new WaitForSeconds(5);
    //    isAttack = true;
    //}
}
