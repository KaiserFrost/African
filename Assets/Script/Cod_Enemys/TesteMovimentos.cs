using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteMovimentos : MonoBehaviour {

    public Animator anim;
    public AnimatorClipInfo[] clip;
    public bool noChao = true;

    public bool ataque1;
    public bool ataque2;
    public bool defesa;
    public bool rola;

    public float velocidade;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ataque();
        Defesa();
        Esquivar();

        UpdateParametrosAnimator();
	}

    void UpdateParametrosAnimator()
    {
        anim.SetBool("z_noChao", noChao);
        
        
        anim.SetBool("Hit1", ataque1);
        anim.SetBool("Hit2", ataque2);
        anim.SetBool("isBlock", defesa);
        anim.SetBool("isCambalhota", rola);

        anim.SetFloat("z_velocidade", velocidade);
    }

    void Ataque()
    {
        ataque1 = false;
        clip = anim.GetCurrentAnimatorClipInfo(0);
        if (ataque1)
        ataque2 = false;

        

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (noChao)
            {
                ataque1 = true;
                ataque2 = false;
                defesa = false;
                
                clip = anim.GetCurrentAnimatorClipInfo(0);
                if(clip[0].clip.name == "hit1")
                {
                    Debug.Log(ataque2);
                    Debug.Log(ataque1);
                    ataque1 = false;
                    ataque2 = true;
                    defesa = false;
                }
               
                
            }
        }
    }

    void Defesa()
    {
        defesa = false;
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (noChao)
            {
                defesa = true;
            }
        }
    }

    void Esquivar()
    {
        rola = false;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (noChao)
            {
                rola = true;
            }
        }
    }
}
