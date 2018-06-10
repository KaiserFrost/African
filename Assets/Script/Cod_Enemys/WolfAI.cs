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

    public float VidaCheia = 100;
    public float VidaAtual;


    public GameObject Objeto;
    public GameObject[] Local;

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
            if (Vector3.Distance(transform.position, jogador.transform.position) < 15f)
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
                if (Vector3.Distance(transform.position, jogador.transform.position) < 10f)
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

                if (Vector3.Distance(transform.position, jogador.transform.position) < 3f)
                {

                    isAlert = false;
                    clip = animatorAnimal.GetCurrentAnimatorClipInfo(0);
                    if (isAlert == false)
                    {
                        isWallking = false;
                        isRunning = false;
                        isAttack = true;
                    }
                    //navMesh.destination = jogador.transform.position;
                    Debug.Log("ATACAR!!!");
                    Attack();
                }

            }

        }

    }


    void SistemaDeVida()
    {
        if (VidaAtual >= VidaCheia)
        {
            VidaAtual = VidaCheia;
        }
        else if (VidaAtual <= 0)
        {
            VidaAtual = 0;
     
            Dropar();
            Destroy(gameObject);
        }
    }

    void Dropar()
    {
        for (int i = 0; i < 3; i++)
            Instantiate(Objeto, Local[i].transform.position, Local[i].transform.rotation);
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

    void OnTriggerEnter(Collider col)
    {
        if (GameObject.FindWithTag("Boom"))
        {
            this.GetComponent<WolfAI>().VidaAtual -= atkBase;
        }
        //GetComponent<AudioSource>().PlayOneShot(damageSound);


    }


    //IEnumerator AttackTime()
    //{
    //    isAttack = false;
    //    yield return new WaitForSeconds(5);
    //    isAttack = true;
    //}
}
