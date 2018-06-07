using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour {

    
    public GameObject player;
    public Image healthBar;
    public int maxHealth = 100;
    public static int health;
    public Slider hungryBar;
    public static int maxHungry;
    public static int hungry = 100;
    public Slider thirstBar;
    public int maxThirst = 100;
    public static int thirst;


    // Use this for initialization
    void Start () {
        healthBar = GetComponent<Image>();
        health = maxHealth;
        InvokeRepeating("ReduceThirst", 1, 1);
        InvokeRepeating("ReduceHungry", 1, 1);
    }
	
	// Update is called once per frame
	void Update () {
        //healthBar.fillAmount = health / maxHealth;

        //if (Input.GetMouseButtonDown(0))
        //{
          //  TakeDamage();
        //}
        
	}

    void ReduceThirst()
    {
        thirst--;
        thirstBar.value = thirst;
        if(thirst <= 0)
        {
            health = health - 5; 
            //Aqui tem de mexer a barra
        }
    }
    void ReduceHungry()
    {
        hungry--;
        hungryBar.value = hungry;

        if (hungry <= 0)
        {
            health = health - 3;
            //Aqui tem de mexer a barra
        }
    }

    void TakeDamage()
    {
        health -= 5;
        transform.localPosition = new Vector3((health / maxHealth), 1, 1);
    }
}
