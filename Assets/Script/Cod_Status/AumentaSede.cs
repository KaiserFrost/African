using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AumentaSede : MonoBehaviour {

    public enum TIPO
    {
        Comida,
        Bebida,
        Vida,
        Energia
    }
    public TIPO TipoDoItem;
    [Range(1, 500)]
    public float QuantoRepor = 50;
    private GameObject Jogador;

    void Start(){
        Jogador = GameObject.FindWithTag("Player");
    }

    void OnTriggerStay(Collider other){
        if (Input.GetKeyDown("e") && other.gameObject == Jogador.gameObject){
            switch (TipoDoItem)
            {
                case TIPO.Comida:
                    Jogador.GetComponent<SistemaStatus>().FomeAtual += QuantoRepor;
                    Destroy(gameObject);
                    break;
                case TIPO.Bebida:
                    Jogador.GetComponent<SistemaStatus>().SedeAtual += QuantoRepor;
                    Destroy(gameObject);
                    break;
                case TIPO.Vida:
                    Jogador.GetComponent<SistemaStatus>().VidaAtual += QuantoRepor;
                    Destroy(gameObject);
                    break;
                
            }
        }
    }
}


