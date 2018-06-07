using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCortArvores : MonoBehaviour {

    public Camera cameraPrincipal;
    public float DistanciaMinima = 6, TempoPorAtaque = 1;
    public int DanoCausado = 30;
   // public AudioClip somMadeira;
    private float contador;
    private bool podeAtacar;
   
    void Start()
    {

        //cameraPrincipal = Camera.main;
        Cursor.visible = true;//false
        contador = 0;
        podeAtacar = true;
    }
    void Update()
    {
        RaycastHit colisor;
        //ver melhor isto
        Ray CentroDaTela = cameraPrincipal.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 4f, 0));
        bool colisao = Physics.Raycast(CentroDaTela, out colisor);
        if (colisao)
        {
            /*//if(Input.GetKeyDown("e") && Vector3.Distance(transform.position, colisor.transform.position) < DistanciaMinima && podeAtacar == true)
            //{
            //    print("Arvore");
            //    if (colisor.collider)
            //    {
            //        CortaArvore ScriptArvore = colisor.transform.GetComponent<CortaArvore>() as CortaArvore;
            //        ScriptArvore.VIDA -= DanoCausado;
            //        print("Hit");
            //    }
            */
                

            }
            if (Input.GetButtonUp("Fire1") && Vector3.Distance(transform.position, colisor.transform.position) < DistanciaMinima && podeAtacar == true)
            {
                
                podeAtacar = false;
                if (colisor.transform.gameObject.tag == "ARVORE")
                {
                    //GetComponent<AudioSource>().PlayOneShot(somMadeira);
                    CortaArvore ScriptArvore = colisor.transform.GetComponent<CortaArvore>() as CortaArvore;
                    ScriptArvore.VIDA -= DanoCausado;
                }

                if (colisor.transform.gameObject.tag == "PEDRA")
                {
                    //GetComponent<AudioSource>().PlayOneShot(somPedra);
                    CortaArvore ScriptArvore = colisor.transform.GetComponent<CortaArvore>() as CortaArvore;
                    ScriptArvore.VIDA -= DanoCausado;
                print("Hit");
            }
            }
              
        if (podeAtacar == false)
        {
            contador += Time.deltaTime;
        }
        if (contador >= TempoPorAtaque)
        {
            contador = 0;
            podeAtacar = true;
        }
    }
  
}
