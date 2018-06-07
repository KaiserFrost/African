using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]//a cena do terreno
public class EnemyAI : MonoBehaviour {

    public Animator animatorAnimal;
    public AnimatorClipInfo[] clip;

    private GameObject jogador;
    private GameObject inimigo;
    private NavMeshAgent navMesh;

    public float VidaCheia = 100;
    public float VidaAtual;

    private bool isAlert;
    private bool isRunning;
    private bool isWallking;
    private bool isAttack;
    

    public int atkBase = 1;//provisorio
    private Vector3 speed;

    //public BoxCollider atkCollider;

    public GameObject Objeto;
    public GameObject[] Local;

    //public GameObject RinoMorto;

    // Use this for initialization
    void Start () {

        VidaAtual = VidaCheia;
        jogador = GameObject.FindWithTag("Player");
        inimigo = GameObject.FindWithTag("Enemy");
        navMesh = GetComponent<NavMeshAgent>();

        //atkCollider = GetComponent<BoxCollider>();
        //atkCollider.isTrigger = false;

        speed = new Vector3(0, 0, 0);
        
	}
	
	// Update is called once per frame
	void Update () {


        SistemaDeVida();
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
        if (Vector3.Distance(transform.position, jogador.transform.position) < 20f)
        {
            isAlert = true;
            clip = animatorAnimal.GetCurrentAnimatorClipInfo(0);
            if(isAlert)
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
                    isAttack = false;
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
                        isAttack = false;
                    }

                    //diminuir a velocidade!!!!!
                    navMesh.destination = jogador.transform.position;
                    Debug.Log("CORRER");
                }

                if (Vector3.Distance(transform.position, jogador.transform.position) < 2.5f)
                {
                   
                    isAlert = false;
                    clip = animatorAnimal.GetCurrentAnimatorClipInfo(0);
                    if (isAlert == false)
                    {
                        isWallking = false;
                        isRunning = false;
                        isAttack = true;
                    }
                    
                       //Attack();

                    //isAlert = true;
                    //clip = animatorAnimal.GetCurrentAnimatorClipInfo(0);
                    //if (isAlert)
                    //{
                    //    isWallking = false;
                    //    isRunning = false;
                    //}

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
            Morreu();
            Dropar();
            Destroy(gameObject);
        }
    }

    void Morreu()
    {
        animatorAnimal.SetBool("Dead", true);
        Debug.Log("E morreu...");
        
        //Instantiate(RinoMorto, transform.position, transform.rotation);
        //Destroy(gameObject);
    }
    ////alterar a forma de ataque, sem usar coroutine
    //void Attack()//Ta muito Basico... 
    //{

    //    print("Atingiu!!!!");
    //    //por pa ele so colidir com o cilindro do corpo do gajo
    //    if(atkCollider.isTrigger == true)
    //    {
    //        //StartCoroutine("AttackTime");
            
    //        jogador.GetComponent<SistemaStatus>().VidaAtual -= atkBase;

    //    }
        
    //}
   
    void Dropar()
    {
        for(int i=0; i < 3; i++)
        Instantiate(Objeto,Local[i].transform.position,Local[i].transform.rotation);
    }
    //IEnumerator AttackTime()
    //{
    //    isAttack = false;
    //    yield return new WaitForSeconds(5);
    //    isAttack = true;
    //}
}
