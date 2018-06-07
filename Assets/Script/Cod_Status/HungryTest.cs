using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungryTest : MonoBehaviour {

    public GameObject player;

    public static int health = 100;
    public Slider healthBar;

    public static int hungry = 100;
    public Slider hungryBar;
    public Slider thirstBar;
    public static int thirst = 100;



    // Use this for initialization
    void Start () {
        InvokeRepeating("ReduceHungry", 1, 1);
        InvokeRepeating("ReduceThirst", 1, 1);
        

    }
	
	// Update is called once per frame
	void Update () {

        if (health == 0) 
            Debug.Log("E morreu...");
    }

    void ReduceHealth()
    {     
        health--;
        healthBar.value = health; 
        
    }
    void ReduceHungry()
    {
        hungry = hungry - 20;
        hungryBar.value = hungry;
        if (hungry <= 0)
        {
            health = health - 1;
            healthBar.value = health;
        }
    }
    //Aumenta a sede
    void ReduceThirst()
    {
        thirst = thirst - 20;
        thirstBar.value = thirst;
        if (thirst <= 0)
        {
            health = health - 1;
            healthBar.value = health;
            //Aqui tem de mexer a barra
        }
    }
}
