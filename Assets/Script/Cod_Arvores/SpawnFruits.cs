using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFruits : MonoBehaviour {

    public GameObject fruta;
    int spawnNum = 3;//cria 8 frutas

    void spawn()
    {
        for(int i = 0; i <spawnNum; i++)
        {
            //Manipular as posiçoes conforme o bush
            Vector3 frutaPos = new Vector3(this.transform.position.x + Random.Range(-1.0f, 1.0f),
                                            this.transform.position.y + Random.Range(0.0f, 1.0f),
                                            this.transform.position.z + Random.Range(2.0f, 4.0f));
            //Instancia uma nova fruta
            Instantiate(fruta, frutaPos, Quaternion.identity);
        }
    }

	// Use this for initialization
	void Start () {
        spawn();
	}
	
	
}
