using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour {

    private GameObject jogador;
    public int atkBase = 1;
    public AudioClip damageSound;
    public AudioSource soundSource;

    void OnTriggerEnter(Collider col)
    {
       
        jogador.GetComponent<SistemaStatus>().VidaAtual -= atkBase;
        soundSource.Play();
        //GetComponent<AudioSource>().PlayOneShot(damageSound);


    }

    void Start()
    {
        jogador = GameObject.FindWithTag("Player");
        soundSource.clip = damageSound;
    }
}
