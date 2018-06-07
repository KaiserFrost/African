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
    public float QuantoRepor = 50;
    private GameObject Jogador;
    public GameObject Aua;

    private Vector3 NivelAua = new Vector3(0.0f,0.1f,0.0f);

    void Start()
    {
        Jogador = GameObject.FindWithTag("Player");
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
