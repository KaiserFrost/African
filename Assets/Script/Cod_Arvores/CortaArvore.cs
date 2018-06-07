using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CortaArvore : MonoBehaviour {

    public int TempoDeQueda = 8;
    public int VIDA;
    private Rigidbody corpoRigido;
    private float cronometro;
    private bool comecarContagem;
    public GameObject Madeiras;
    public GameObject[] localMadeiras;
    public int minMadeiras = 1, maxMadeiras = 5;
    void Start()
    {
        corpoRigido = GetComponent<Rigidbody>();
        corpoRigido.useGravity = true;
        corpoRigido.isKinematic = true;
        VIDA = 100;
        cronometro = 0;
        comecarContagem = false;
        corpoRigido.mass = 250;
    }
    void Update()
    {
        if (VIDA <= 0)
        {
            corpoRigido.isKinematic = false;
            corpoRigido.AddForce(Random.Range(-20, 20) * 75, 0, Random.Range(-20, 20) * 75);
            comecarContagem = true;
        }
        if (comecarContagem == true)
        {
            cronometro += Time.deltaTime;
        }
        if (cronometro >= TempoDeQueda)
        {
            cronometro = 0;
            int quantidade = Random.Range(minMadeiras, maxMadeiras);
            if (quantidade > localMadeiras.Length)
            {
                quantidade = localMadeiras.Length;
            }
            for (int x = 0; x < quantidade; x++)
            {
                Instantiate(Madeiras, localMadeiras[x].transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}

