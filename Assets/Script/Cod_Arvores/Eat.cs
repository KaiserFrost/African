using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eat : MonoBehaviour {

    public AudioClip eatSound;

    public void eat()
    {
        if (System.Int32.Parse(this.transform.Find("Text").GetComponent<Text>().text) > 1)
        {
            GetComponent<AudioSource>().PlayOneShot(eatSound);
            int tCount = System.Int32.Parse(this.transform.Find("Text").GetComponent<Text>().text) - 1;
            this.transform.Find("Text").GetComponent<Text>().text = "" + tCount;

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

	
}
