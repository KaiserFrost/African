using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColLancaEnemy : MonoBehaviour {

    private GameObject enemy;
    public int atkBase = 30;

    void OnTriggerEnter(Collider col)
    {
        
        enemy.GetComponent<EnemyAI>().VidaAtual -= atkBase;
    }

    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy");
    }
}
