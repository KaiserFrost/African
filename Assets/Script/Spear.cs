using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour {

    public float tmpVida = 100;
    float tmpCurrent;

    public float velocidade;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(0, 0, velocidade * Time.deltaTime);

        tmpVida -= Time.deltaTime;
        if(tmpCurrent >= tmpVida)
        {
            Destroy(gameObject);
        }
	}
    public void OnTriggerEnter()
    {
        tmpVida -= 2;
        if(tmpVida <= 0)
             Destroy(gameObject);
    }
}
