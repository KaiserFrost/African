using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeraAnimais : MonoBehaviour {

    public Transform animal;
    public int max_Animals;
    private int animals;
    private float timer;
    public int tempoPaSpawnar;


	
	// Update is called once per frame
	void Update () {
        animals = transform.childCount;
        if(animals<max_Animals && timer < 30)
        {
            timer += Time.deltaTime;
            if (timer > tempoPaSpawnar)
            {
                var clon = Instantiate(animal, transform.position, Quaternion.identity);
                clon.transform.parent = gameObject.transform;
                timer = 0;
            }
        }
	}
}
