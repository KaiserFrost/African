using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SistemaStatus : MonoBehaviour {

    private CharacterController controlador;
    public Animator anim;
    private GameObject Jogador;
    private float UltimaPosicaoEmY, DistanciaDeQueda;
    [Range(1, 15)]
    public float AlturaQueda = 4, DanoPorMetro = 1;
    public Image BarraVida, BarraFome, BarraSede;
    [Range(20, 500)]
    public float VidaCheia = 200, EstaminaCheia = 100, FomeCheia = 2000, SedeCheia = 1500, velocidadeEstamina = 250;
    [HideInInspector]
    public float VidaAtual, EstaminaAtual, FomeAtual, SedeAtual;
    private bool semEstamina = false;
    private float cronometroFome, cronometroSede, velocidadeCaminhando, velocidadeCorrendo;

    void Start()
    {
        controlador = GetComponent<CharacterController>();
        VidaAtual = VidaCheia;
        EstaminaAtual = EstaminaCheia;
        FomeAtual = FomeCheia;
        SedeAtual = SedeCheia;
        Jogador = GameObject.FindWithTag("Player");
       
    }
    void Update(){
        SistemaDeQueda();
        SistemaDeVida();
        SistemaDeFome();
        SistemaDeSede();
        AplicarBarras();
    }
    void SistemaDeQueda(){
        if (UltimaPosicaoEmY > Jogador.transform.position.y && controlador.velocity.y < 0)
        {
            DistanciaDeQueda += UltimaPosicaoEmY - Jogador.transform.position.y;
        }
        UltimaPosicaoEmY = Jogador.transform.position.y;
        if (DistanciaDeQueda >= AlturaQueda && controlador.isGrounded)
        {
            VidaAtual = VidaAtual - DanoPorMetro * DistanciaDeQueda;
            DistanciaDeQueda = 0;
            UltimaPosicaoEmY = 0;
        }
        if (DistanciaDeQueda < AlturaQueda && controlador.isGrounded)
        {
            DistanciaDeQueda = 0;
            UltimaPosicaoEmY = 0;
        }
    }
    void SistemaDeFome(){
        FomeAtual -= Time.deltaTime;
        if (FomeAtual >= FomeCheia)
        {
            FomeAtual = FomeCheia;
        }
        if (FomeAtual <= 0)
        {
            FomeAtual = 0;
            cronometroFome += Time.deltaTime;
            if (cronometroFome >= 3)
            {
                VidaAtual -= (VidaCheia * 0.005f);//0.005f
                //EstaminaAtual -= (EstaminaCheia * 0.1f);
                cronometroFome = 0;
            }
        }
        else
        {
            cronometroFome = 0;
        }
    }
    void SistemaDeSede() {
        SedeAtual -= Time.deltaTime;
        if (SedeAtual >= SedeCheia)
        {
            SedeAtual = SedeCheia;
        }
        if (SedeAtual <= 0)
        {
            SedeAtual = 0;
            cronometroSede += Time.deltaTime;
            if (cronometroSede >= 3)
            {
                //EstaminaAtual -= (EstaminaCheia * 0.1f);
                cronometroSede = 0;
            }
        }
        else
        {
            cronometroSede = 0;
        }
    }

    void SistemaDeVida() {
        if (VidaAtual >= VidaCheia)
        {
            VidaAtual = VidaCheia;
        }
        else if (VidaAtual <= 0)
        {
            VidaAtual = 0;
            Morreu();
        }
    }
    void AplicarBarras(){
        BarraVida.fillAmount = ((1 / VidaCheia) * VidaAtual);
        BarraFome.fillAmount = ((1 / FomeCheia) * FomeAtual);
        BarraSede.fillAmount = ((1 / SedeCheia) * SedeAtual);
    }
    void Morreu()
    {
        anim.SetBool("isDied", true);
        Debug.Log("E morreu...");
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Enemy")
        {
            VidaAtual = VidaAtual - 5; 
            Debug.Log("Faleceu");
            //SceneManager.LoadScene(menu);
        }
    }
}
