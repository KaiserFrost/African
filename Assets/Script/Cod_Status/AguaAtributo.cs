using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AguaAtributo : MonoBehaviour {
    public enum TIPO
    {
        Comida,
        Bebida,
        Vida,
        Energia
    }
    public TIPO TipoDoItem;
    [Range(1, 500)]
    public float QuantoRepor = 20;
    private GameObject Jogador;
    public GameObject Aua;

    public AudioClip drinkSound;
    //public AudioClip eatSound;
    

    public AudioSource soundSource;

   

    private Vector3 NivelAua = new Vector3(0.0f,0.001f,0.0f);

    void Start()
    {
        Jogador = GameObject.FindWithTag("Player");
        soundSource.clip = drinkSound;
       // soundSource.clip = eatSound;
        
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown("e") && other.gameObject == Jogador.gameObject)
        {
            switch (TipoDoItem)
            {
                case TIPO.Comida:
                    
                    Jogador.GetComponent<SistemaStatus>().FomeAtual += QuantoRepor;
                    
                    break;
                case TIPO.Bebida:
                    soundSource.Play();
                    Jogador.GetComponent<SistemaStatus>().SedeAtual += QuantoRepor;
                    Aua.transform.position -= NivelAua;


                    break;
                case TIPO.Vida:
                    
                    Jogador.GetComponent<SistemaStatus>().VidaAtual += QuantoRepor;
                   
                    break;

            }
        }
    }
}
