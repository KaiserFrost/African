using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAtck : MonoBehaviour {

    private GameObject jogador;
    public int atkBase = 1;

    void OnTriggerEnter(Collider col)
    {
        jogador.GetComponent<SistemaStatus>().VidaAtual -= atkBase;
    }

    void Start()
    {
        jogador = GameObject.FindWithTag("Player");
    }
}
